import { RootStore } from "./rootStore";
import { action, makeAutoObservable, observable } from "mobx";
import { ApiClientStore } from "./apiClientStore";
import {
	DeckDetailDto,
	DeckDetailDtoListRestResultObject,
	DeckMasterDto,
	DeckMasterDtoPaginatedRestResultObject,
	DeckMultipleDeckRequest,
} from "../schemas/dice-club";
import { apiRoutes } from "../api_client/api.routes";

export class DeckStore {
	rootStore: RootStore;
	private apiClientStore: ApiClientStore;

	@observable
	decks: DeckMasterDto[] = [];
	@observable
	selectedDeckMaster: DeckMasterDto | null = null;
	@observable
	deckDetails: DeckDetailDto[] = [];
	@observable
	viewListSelection: boolean = false;

	@observable
	multipleDeckQuery: DeckMultipleDeckRequest = {
		count: 10,
		sideBoardTotalCards: 12,
		totalCards: 60,
	};

	constructor(rootStore: RootStore) {
		this.rootStore = rootStore;
		this.apiClientStore = this.rootStore.apiClient;
		makeAutoObservable(this);
	}

	@action
	setMultipleDeckQuery(query: DeckMultipleDeckRequest) {
		this.multipleDeckQuery = { ...this.multipleDeckQuery, ...query };
	}

	@action
	toggleViewListSelection(value: boolean) {
		this.viewListSelection = value;
	}

	@action
	async selectDeckMaster(deckId: string) {
		if (this.decks.length === 0) {
			await this.getDecksMaster();
		}
		this.selectedDeckMaster = this.decks.find((d) => d.id === deckId)!;
	}

	@action
	async getDecksMaster() {
		this.decks = [];
		const result =
			await this.apiClientStore.get<DeckMasterDtoPaginatedRestResultObject>(
				apiRoutes.DECK.DECKS
			);
		this.decks = result.result!;
	}
	@action
	async getDeckDetails(deckId: string) {
		const result =
			await this.apiClientStore.get<DeckDetailDtoListRestResultObject>(
				apiRoutes.DECK.DECKS + "/master/" + deckId
			);
		this.deckDetails = result.result!;
	}
	async createMultipleDecks() {
		await this.apiClientStore.post(
			apiRoutes.DECK.MULTIPLE_DECK,
			this.multipleDeckQuery
		);
	}
}
