import { observe } from "mobx";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import { RootStore } from "./rootStore";

export class PreloadStore {
	preloadedStores: IBasePreloadStore[];
	rootStore: RootStore;

	constructor(preloadedStores: IBasePreloadStore[], rootStore: RootStore) {
		this.preloadedStores = preloadedStores;
		this.rootStore = rootStore;
		observe(rootStore.apiClient, (change) => {
			if (change.name === "isAutheticated") {
				if (this.rootStore.apiClient.getAuthToken() !== null) {
          console.log("Preloading stores " );
					this.preloadStores();
				}
			}
		});
	}

	async preloadStores() {
		for (const store of this.preloadedStores) {
			await store.onAuthenticated(this.rootStore.apiClient.getAuthToken()?.accessToken!);
		}
	}
}
