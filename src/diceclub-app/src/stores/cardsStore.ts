import { action, makeAutoObservable, observable } from "mobx";
import { apiRoutes } from "../api_client/api.routes";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import {
  MtgCardColorDto,
  MtgCardLanguageDto,
  MtgCardRarityDto,
  MtgCardSetDto,
  MtgCardTypeDto,
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
    ]);
    return Promise.resolve();
  }

  @action
  async loadSets() {
    this.sets = await this.apiClientStore.getPaginatedFull<MtgCardSetDto>(
      apiRoutes.CARDS.SETS
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
}
