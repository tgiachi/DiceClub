import { action, makeAutoObservable, observable } from "mobx";
import { apiRoutes } from "../api_client/api.routes";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import {
	MtgCardColorDto,
	MtgCardDto,
	MtgCardDtoPaginatedRestResultObject,
	MtgCardLanguageDto,
	MtgCardLegalityDto,
	MtgCardLegalityTypeDto,
	MtgCardRarityDto,
	MtgCardSetDto,
	MtgCardTypeDto,
	SearchCardRequest,
	SearchCardRequestOrderBy,
} from "../schemas/dice-club";
import { ApiClientStore } from "./apiClientStore";
import { RootStore } from "./rootStore";

export class CardsStore implements IBasePreloadStore {
	rootStore: RootStore;
	apiClientStore: ApiClientStore;

	@observable sets?: MtgCardSetDto[] = [];
	@observable types?: MtgCardTypeDto[] = [];
	@observable colors?: MtgCardColorDto[] = [];
	@observable rarities: MtgCardRarityDto[] = [];
	@observable languages: MtgCardLanguageDto[] = [];
	@observable legalities: MtgCardLegalityDto[] = [];
	@observable legalityTypes: MtgCardLegalityTypeDto[] = [];
	@observable searchOrderBy: string[] = [];

	@observable ownedCardSearchQuery: SearchCardRequest = {};
	@observable ownedCardSearchCurrentPage = 1;
	@observable ownedCardSearchTotalPages = 1;
	@observable ownedCardResult: MtgCardDto[] = [];

	constructor(rootStore: RootStore) {
		this.rootStore = rootStore;
		this.apiClientStore = rootStore.apiClient;
		makeAutoObservable(this);
	}

	async onAuthenticated(auth: string): Promise<void> {
		Promise.all([
			this.loadSets(),
			this.loadColors(),
			this.loadTypes(),
			this.loadRarities(),
			this.loadLanguages(),
			this.loadLegalities(),
			this.loadLegalityTypes(),
			this.loadSearchOrderBy(),
		]);
		return Promise.resolve();
	}

	@action
	async setOwnedCardsResult(result: MtgCardDto[]) {
		this.ownedCardResult = result;
	}

	@action
	async loadSearchOrderBy() {
		this.searchOrderBy = Object.values(SearchCardRequestOrderBy);
	}

	@action
	async loadSets() {
		this.sets = await this.apiClientStore.getPaginatedFull<MtgCardSetDto>(
			apiRoutes.CARDS.SETS
		);
	}

	@action
	async loadLegalities() {
		this.legalities =
			await this.apiClientStore.getPaginatedFull<MtgCardLegalityDto>(
				apiRoutes.CARDS.LEGALITIES
			);
	}

	@action
	async loadLegalityTypes() {
		this.legalityTypes =
			await this.apiClientStore.getPaginatedFull<MtgCardLegalityTypeDto>(
				apiRoutes.CARDS.LEGALITY_TYPES
			);
	}

	@action
	async loadColors() {
		this.colors = await this.apiClientStore.getPaginatedFull<MtgCardColorDto>(
			apiRoutes.CARDS.COLORS
		);
	}
	@action
	async loadTypes() {
		this.types = await this.apiClientStore.getPaginatedFull<MtgCardTypeDto>(
			apiRoutes.CARDS.TYPES
		);
	}
	@action
	async loadRarities() {
		this.rarities =
			await this.apiClientStore.getPaginatedFull<MtgCardRarityDto>(
				apiRoutes.CARDS.RARITIES
			);
	}

	@action
	async loadLanguages() {
		this.languages =
			await this.apiClientStore.getPaginatedFull<MtgCardLanguageDto>(
				apiRoutes.CARDS.LANGUAGES
			);
	}

	@action
	async buildSearchQuery(searchQuery: SearchCardRequest) {
		this.ownedCardSearchQuery = { ...searchQuery };
	}

	@action
	async incrementOwnedCardSearchPage() {
		if (this.ownedCardSearchCurrentPage < this.ownedCardSearchTotalPages) {
			this.ownedCardSearchCurrentPage++;
			await this.searchOwnedCards();
		}
	}

	@action
	async decrementOwnedCardSearchPage() {
		if (this.ownedCardSearchCurrentPage > 1) {
			this.ownedCardSearchCurrentPage--;
			await this.searchOwnedCards();
		}
	}

  @action
  async goToPageOwnedCardSearch(page: number) { 
    if (page > 0 && page <= this.ownedCardSearchTotalPages) {
      this.ownedCardSearchCurrentPage = page;
      await this.searchOwnedCards();
    }
  }

	@action
	async searchOwnedCards() {
		const result =
			await this.apiClientStore.post<MtgCardDtoPaginatedRestResultObject>(
				apiRoutes.PAGINATION.buildPaginationQuery(
					apiRoutes.CARDS.SEARCH,
					this.ownedCardSearchCurrentPage,
					apiRoutes.PAGINATION.PAGE_SIZE
				),
				this.ownedCardSearchQuery
			);
		this.ownedCardSearchTotalPages = result.pageCount!;
		this.setOwnedCardsResult(result.result!);
	}
}
