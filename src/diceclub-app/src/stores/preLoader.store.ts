import { RootStore } from "./root.store";

export class PreloaderStore {
	rootStore: RootStore;
	constructor(rootStore: RootStore) {
		this.rootStore = rootStore;
	}
}
