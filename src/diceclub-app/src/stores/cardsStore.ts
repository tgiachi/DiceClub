import { makeAutoObservable } from "mobx";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import { RootStore } from "./rootStore";

export class CardsStore implements IBasePreloadStore {
	rootStore: RootStore;

	constructor(rootStore: RootStore) {
		this.rootStore = rootStore;
		makeAutoObservable(this);
	}

	onAuthenticated(auth: string): Promise<void> {
		throw new Error("Method not implemented.");
	}
}
