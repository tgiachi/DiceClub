import React from "react";
import { makeAutoObservable } from "mobx";
import axios from "axios";
import { LoginResponseData, LoginRequestData } from "../schemas/dice-club";
import { apiConfig } from "../api_client/api.config";
import ErrorStore from "./errors.store";
import { useStore } from "./store.context";
import { RootStore } from "./root.store";

class LoginStore {
	logged: boolean;
	token: LoginResponseData;
	rootStore: RootStore;

	constructor(rootStore: RootStore) {
		this.logged = false;
		this.token = {};
		this.rootStore = rootStore;

		makeAutoObservable(this);
	}

	get isLogged() {
		return this.logged;
	}

	set setLogged(value: boolean) {
		this.logged = value;
	}

	checkAuthToken() {
		var auth = localStorage.getItem("auth");
		if (auth) {
			this.token = JSON.parse(auth);
		}
	}
	async login({ username, password }: { username: string; password: string }) {
		// const { errorsStore } = useStore();

		try {
			this.rootStore.setIsLoading = true;
			const result = await axios.post(`${apiConfig.baseURL}/api/v1/login/auth`, {
				email: username,
				password
			} as LoginRequestData);
			const loginResponse = result.data as LoginResponseData;

			if (loginResponse.accessToken && loginResponse.refreshToken && loginResponse.accessTokenExpire) {
				this.rootStore.setIsLoading = false;
				this.rootStore.apiStore.setAuthenticated(
					loginResponse.accessToken,
					loginResponse.refreshToken,
					new Date(loginResponse.accessTokenExpire)
				);
			}
		} catch (e) {
			this.rootStore.errorsStore.addError("Error during login", "error");

			this.rootStore.setIsLoading = false;
		}
	}
}

export default LoginStore;
