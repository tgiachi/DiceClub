import React from "react";
import { makeAutoObservable } from "mobx";
import axios from "axios";
import { LoginResponseData, LoginRequestData } from "../schemas/dice-club";
import { apiConfig } from "../api_client/api.config";

class LoginStore {
	logged: boolean;
	token: LoginResponseData;

	constructor() {
		this.logged = false;
		this.token = {};
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
		const result = await axios.post(`${apiConfig.baseURL}/api/v1/login/auth`, {
			email: username,
			password
		} as LoginRequestData);

		const loginResponse = result.data as LoginResponseData;
		console.log(loginResponse);
		this.logged = true;
	}
}

export default LoginStore;
