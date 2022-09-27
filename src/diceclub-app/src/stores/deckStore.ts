import { RootStore } from "./rootStore";
import { action, makeAutoObservable, observable } from "mobx";
import { ApiClientStore } from "./apiClientStore";
import { DeckMasterDto, DeckMasterDtoPaginatedRestResultObject } from "../schemas/dice-club";
import { apiRoutes } from "../api_client/api.routes";

export class DeckStore  {
	rootStore: RootStore; 
  private apiClientStore: ApiClientStore;

  @observable
  decks: DeckMasterDto[] = [];

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

}