import { makeAutoObservable } from "mobx";
import axios from "axios";
import { RootStore } from "./root.store";

class ApiClientStore {
	rootStore: RootStore;
	isAutheticated: boolean;
	token: string = "";
	constructor(rootStore: RootStore) {
		this.isAutheticated = false;
		makeAutoObservable(this);
		this.rootStore = rootStore;
    this.checkAuthToken();
	}
	get getIsAuthenticated() {
		return this.isAutheticated;
	}

	checkAuthToken() {
		var auth = localStorage.getItem("auth");
		if (auth) {
			const { token, refreshToken, expire } = JSON.parse(auth);
			if (expire > new Date()) {
				this.setAuthenticated(token, refreshToken, expire);
			}
		}
	}

	setAuthenticated(token: string, refreshToken: string, expire: Date) {
		this.isAutheticated = true;
		this.token = token;
		localStorage.setItem("auth", JSON.stringify({ token, refreshToken, expire }));
	}
}

export { ApiClientStore };
