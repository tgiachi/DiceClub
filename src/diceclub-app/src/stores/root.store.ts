import { makeAutoObservable } from "mobx";
import { ApiClientStore } from "./api.store";
import { CardStore } from "./card.store";
import ErrorStore from "./errors.store";
import LoginStore from "./login.store";

export class RootStore {
	loginStore: LoginStore;
	apiStore: ApiClientStore;
	errorsStore: ErrorStore;
	cardsStore: CardStore;

	constructor() {
		makeAutoObservable(this);
		this.loginStore = new LoginStore(this);
		this.apiStore = new ApiClientStore(this);
		this.errorsStore = new ErrorStore(this);
		this.cardsStore = new CardStore(this);
	}
}
