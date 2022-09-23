import { computed, makeAutoObservable, observable } from "mobx";
import { INotification } from "../interfaces/notification";
import { RootStore } from "./rootStore";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import { v4 as uuidv4 } from "uuid";
export class NotificationsStore {
	rootStore: RootStore;
	@observable notificationsQueue: INotification[] = [];

	@computed get getNotifications() {
		return this.notificationsQueue;
	}
	constructor(rootStore: RootStore) {
		this.rootStore = rootStore;
		makeAutoObservable(this);
	}

	addNotification(notification: INotification) {
		notification.id = uuidv4();
		this.notificationsQueue.push(notification);
	}
}
