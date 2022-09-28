import { RootStore } from "./rootStore";
import { action, makeAutoObservable, observable } from "mobx";
import { ApiClientStore } from "./apiClientStore";
import { DeckDetailDto, DeckDetailDtoListRestResultObject, DeckMasterDto, DeckMasterDtoPaginatedRestResultObject } from "../schemas/dice-club";
import { apiRoutes } from "../api_client/api.routes";

export class DeckStore  {
	rootStore: RootStore; 
  private apiClientStore: ApiClientStore;

  @observable
  decks: DeckMasterDto[] = [];
  deckDetails: DeckDetailDto[] =[];

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    this.apiClientStore = this.rootStore.apiClient;
    makeAutoObservable(this);
  }

  @action
  async getDecksMaster() {
    this.decks = [];
    const result = await this.apiClientStore.get<DeckMasterDtoPaginatedRestResultObject>(apiRoutes.DECK.DECKS);
    this.decks = result.result!;
  }
  @action
  async getDeckDetails(deckId: string) {
    const result = await this.apiClientStore.get<DeckDetailDtoListRestResultObject>(apiRoutes.DECK.DECKS +"/master/"+ deckId);
    this.deckDetails = result.result!;
  }
}