import { makeAutoObservable } from "mobx";
import { ApiClientStore } from "./api.store";
import { CardStore } from "./card.store";
import ErrorStore from "./errors.store";
import LoginStore from "./login.store";
import { PreloaderStore } from "./preLoader.store";
import { UserStore } from "./users.store";

export class RootStore {
	isLoading: boolean = false;

	loginStore: LoginStore;
	apiStore: ApiClientStore;
	errorsStore: ErrorStore;
	cardsStore: CardStore;
	preloaderStore: PreloaderStore;
	usersStore : UserStore;

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
		this.usersStore = new UserStore(this);
		this.preloaderStore = new PreloaderStore(this);
	}
}
