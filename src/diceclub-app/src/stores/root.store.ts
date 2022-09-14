import { makeAutoObservable } from "mobx";
import { ApiClientStore } from "./api.store";
import { CardStore } from "./card.store";
import ErrorStore from "./errors.store";
import LoginStore from "./login.store";
import { PreloaderStore } from "./preLoader.store";

export class RootStore {
	isLoading: boolean = false;

	loginStore: LoginStore;
	apiStore: ApiClientStore;
	errorsStore: ErrorStore;
	cardsStore: CardStore;
	preloaderStore: PreloaderStore;

	get getIsLoading() {
		return this.isLoading;
	}

	set setIsLoading(value: boolean) {
		this.isLoading = value;
	}

	constructor() {
		makeAutoObservable(this);
		this.loginStore = new LoginStore(this);
		this.apiStore = new ApiClientStore(this);
		this.errorsStore = new ErrorStore(this);
		this.cardsStore = new CardStore(this);
		this.preloaderStore = new PreloaderStore(this);
	}
}
