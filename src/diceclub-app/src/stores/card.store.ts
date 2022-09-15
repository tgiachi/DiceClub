import { RootStore } from "./root.store";
import { makeAutoObservable } from "mobx";
import {
	CardDtoPaginationObject,
	CardLegalityDto,
	CardLegalityTypeDto,
	CardQueryObject,
	CardSetDto,
	CardSetDtoPaginationObject,
	CardTypeDto
} from "../schemas/dice-club";
import { ApiClientStore } from "./api.store";
import { apiRoutes } from "../api_client/api.routes";
import { apiConfig } from "../api_client/api.config";
import { ICardColor } from "../interfaces/cards/cardcolor.interfaces";
export class CardStore {
	rootStore: RootStore;
	cardTypes: CardTypeDto[] = [];
	cardLegalities: CardLegalityDto[] = [];
	cardLegailityTypes: CardLegalityTypeDto[] = [];
	cardSets: CardSetDto[] = [];
	cardColors: ICardColor[] = [];
	cardSearchResult?: CardDtoPaginationObject;
	searchQuery: CardQueryObject = {};
	totalPages: number = 0;
	currentPage: number = 1;
	pageSize: number = 30;
	cardTableView: number = 5;
	apiClient: ApiClientStore;

	constructor(rootStore: RootStore) {
		makeAutoObservable(this);
		this.rootStore = rootStore;
		this.apiClient = rootStore.apiStore;
	}

	get getSearchedCards() {
		return this.cardSearchResult?.result?.map((s) => s);
	}

	get getCardTableView() {
		return this.cardTableView;
	}
	set setCardTableView(view: number) {
		this.cardTableView = view;
	}
	get getSets() {
		return this.cardSets;
	}

	set addSets(sets: CardSetDto[]) {
		this.cardSets = [...this.cardSets, ...sets];
	}

	get getQuery() {
		return this.searchQuery;
	}
	async nextPage() {
		if (this.currentPage < this.totalPages) {
			this.currentPage++;
			await this.searchCardsWithQuery(this.searchQuery!, this.currentPage, this.pageSize);
		}
	}
	async prevPage() {
		if (this.currentPage > 1) {
			this.currentPage--;
			await this.searchCardsWithQuery(this.searchQuery!, this.currentPage, this.pageSize);
		}
	}
	async goToPage(page: number) {
		this.currentPage = page;
		await this.searchCardsWithQuery(this.searchQuery!, this.currentPage, this.pageSize);
	}
	set setQuery(searchQuery: CardQueryObject) {
		this.searchQuery = searchQuery;
	}
	async getAllCardSets() {
		let page = 1;
		let allSetUrl = `${apiRoutes.CARDS.ALL_SETS}?page=${page}&pageSize=${apiConfig.defaultPageSize}`;

		let result = await this.apiClient.request<CardSetDtoPaginationObject>({
			method: "get",
			url: allSetUrl
		});
		this.addSets = result.data?.result!;
		while (page < result.data?.totalPages!) {
			page++;
			allSetUrl = `${apiRoutes.CARDS.ALL_SETS}?page=${page}&pageSize=${apiConfig.defaultPageSize}`;
			result = await this.apiClient.request<CardSetDtoPaginationObject>({
				method: "get",
				url: allSetUrl
			});
			this.addSets = result.data?.result!;
		}
	}
	async getAllLegalities() {
		const legalities = await this.apiClient.request<CardLegalityDto[]>({
			method: "get",
			url: apiRoutes.CARDS.ALL_LEGALITIES
		});
		const legalityType = await this.apiClient.request<CardLegalityTypeDto[]>({
			method: "get",
			url: apiRoutes.CARDS.ALL_LEGALITIES_TYPES
		});

		this.cardLegalities = legalities.data!;
		this.cardLegailityTypes = legalityType.data!;
	}
	async getAllTypes() {
		const types = await this.apiClient.request<CardTypeDto[]>({
			method: "get",
			url: apiRoutes.CARDS.ALL_TYPES
		});

		this.cardTypes = types.data!;
	}

	getAllCardColors() {
		this.cardColors = [
			{
				key: "R",
				text: "Red",
				value: "R"
			},
			{
				key: "G",
				text: "Green",
				value: "G"
			},
			{
				key: "B",
				text: "Blue",
				value: "B"
			},
			{
				key: "W",
				text: "White",
				value: "W"
			},
			{
				key: "U",
				text: "Black",
				value: "U"
			}
		];
	}

	async searchCards() {
		this.searchCardsWithQuery(this.searchQuery, this.currentPage, this.pageSize);
	}

	async searchCardsWithQuery(query: CardQueryObject, page?: number, size?: number) {
		if (!page) {
			page = this.currentPage;
		}
		if (!size) {
			size = this.pageSize;
		}
		this.searchQuery = query;

		const result = await this.apiClient.request<CardDtoPaginationObject>({
			method: "post",
			url: `${apiRoutes.CARDS.SEARCH}?pageNum=${page}&pageSize=${size}`,
			payload: query
		});

		if (result.isError) {
			this.rootStore.errorsStore.addError("Error during search cards!", "error");
		}
		this.cardSearchResult = result.data;
		this.totalPages = result.data?.totalPages || 0;
	}
}
