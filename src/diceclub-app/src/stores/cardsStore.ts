import { action, makeAutoObservable, observable } from "mobx";
import { apiRoutes } from "../api_client/api.routes";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import {
  MtgCardColorDto,
  MtgCardColorDtoPaginatedRestResultObject,
  MtgCardSetDto,
  MtgCardSetDtoPaginatedRestResultObject,
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
  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    this.apiClientStore = rootStore.apiClient;
    makeAutoObservable(this);
  }

  async onAuthenticated(auth: string): Promise<void> {
    Promise.all([this.loadSets(), this.loadColors(), this.loadTypes()]);
    return Promise.resolve();
  }

  @action
  async loadSets() {
    let page = 1;
    let pageCount = 0;
    let result =
      await this.apiClientStore.getPaginated<MtgCardSetDtoPaginatedRestResultObject>(
        apiRoutes.CARDS.SETS,
        page
      );
    this.sets = result.result!;
    pageCount = result.pageCount!;

    while (page < pageCount) {
      page++;
      result =
        await this.apiClientStore.getPaginated<MtgCardSetDtoPaginatedRestResultObject>(
          apiRoutes.CARDS.SETS,
          page
        );
      this.sets = this.sets!.concat(result.result!);
    }
  }

  @action
  async loadColors() {
    let page = 1;
    let pageCount = 0;
    let result =
      await this.apiClientStore.getPaginated<MtgCardColorDtoPaginatedRestResultObject>(
        apiRoutes.CARDS.COLORS,
        page
      );
    this.colors = result.result!;
    pageCount = result.pageCount!;

    while (page < pageCount) {
      page++;
      result =
        await this.apiClientStore.getPaginated<MtgCardColorDtoPaginatedRestResultObject>(
          apiRoutes.CARDS.COLORS,
          page
        );
      this.sets = this.colors!.concat(result.result!);
    }
  }
  @action
  async loadTypes() {
    let page = 1;
    let pageCount = 0;
    let result =
      await this.apiClientStore.getPaginated<MtgCardSetDtoPaginatedRestResultObject>(
        apiRoutes.CARDS.TYPES,
        page
      );
    this.types = result.result!;
    pageCount = result.pageCount!;

    while (page < pageCount) {
      page++;
      result =
        await this.apiClientStore.getPaginated<MtgCardSetDtoPaginatedRestResultObject>(
          apiRoutes.CARDS.TYPES,
          page
        );
      this.types = this.colors!.concat(result.result!);
    }
  }
}
