import { RootStore } from "./root.store";
import { makeAutoObservable } from "mobx";
import { CardDtoPaginationObject, CardQueryObject } from "../schemas/dice-club";
import { ApiClientStore } from "./api.store";
export class CardStore {
	rootStore: RootStore;
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
	set setQuery(searchQuery: CardQueryObject) {
		this.searchQuery = searchQuery;
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
			url: "/api/v1/card/search?pageNum=" + page + "&pageSize=" + size,
			payload: query
		});

		if (result.isError) {
			this.rootStore.errorsStore.addError("Error during search cards!", "error");
		}
		this.cardSearchResult = result.data;
		this.totalPages = result.data?.totalPages || 0;
	}
}
