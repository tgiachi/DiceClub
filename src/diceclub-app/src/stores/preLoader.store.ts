import { RootStore } from "./root.store";
import { observe } from "mobx";
export class PreloaderStore {
	rootStore: RootStore;
	constructor(rootStore: RootStore) {
		this.rootStore = rootStore;
		if (this.rootStore.apiStore.getIsAuthenticated) {
			this.preloadData();
		}
		observe(this.rootStore.apiStore, async (change) => {
			if (change.name === "isAutheticated") {
				if (change.object.isAutheticated === true) {
					console.log("Starting preload authentication");
					await this.preloadData();
				}
			}
		});
	}

	async preloadData() {
		Promise.all([
			this.rootStore.usersStore.getUsersAsync(),
			this.rootStore.cardsStore.getAllCardSets(),
			this.rootStore.cardsStore.getAllLegalities(),
			this.rootStore.cardsStore.getAllTypes(),
			this.rootStore.cardsStore.getAllCardColors(),
			this.rootStore.cardsStore.getAllRarities(),
		]);
	}
}
