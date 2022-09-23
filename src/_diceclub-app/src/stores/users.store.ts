import { RootStore } from "./root.store";
import { makeAutoObservable } from "mobx";
import { ApiClientStore } from "./api.store";
import { DiceClubUserDto } from "../schemas/dice-club";
import { apiRoutes } from "../api_client/api.routes";

export class UserStore {
	rootStore: RootStore;
	apiStore: ApiClientStore;

	users: DiceClubUserDto[] = [];

	get getUsers() {
		return this.users;
	}

	set setUsers(users: DiceClubUserDto[]) {
		this.users = users;
	}

	constructor(rootStore: RootStore) {
		makeAutoObservable(this);
		this.rootStore = rootStore;
		this.apiStore = rootStore.apiStore;
	}

	async getUsersAsync() {
		const users = await this.apiStore.request<DiceClubUserDto[]>({
			url: apiRoutes.USERS.LIST,
			method: "get"
		});
		this.setUsers = users.data!;
	}
}
