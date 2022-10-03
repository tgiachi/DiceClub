import { makeAutoObservable } from "mobx";
import { RootStore } from "./rootStore";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { apiConfig } from "../api_client/api.config";
import { apiRoutes } from "../api_client/api.routes";
import { INotificationWebSocket } from "../interfaces/websocket/notification.websocket";
import { IBasePreloadStore } from "../interfaces/stores/store.interface";
import { INotificationType } from "../interfaces/notification";

export class WebSocketStore implements IBasePreloadStore {
	rootStore: RootStore;

	constructor(rootStore: RootStore) {
		makeAutoObservable(this);
		this.rootStore = rootStore;
	}
	onAuthenticated(auth: string): Promise<void> {
		this.connect(auth);
		return Promise.resolve();
	}
	public connect(auth: string) {
		console.log("Connecting to websocket");

		const notificationConnection = new HubConnectionBuilder()
			.withUrl(`${apiConfig.baseURL}${apiRoutes.WEB_SOCKET.NOTIFICATION}`, {
				accessTokenFactory: () => auth,
			})
			.build();
		notificationConnection.start();
		notificationConnection.on(
			"notification",
			(data: INotificationWebSocket) => {
				console.log(`websocket notification received: ${JSON.stringify(data)}`);
				this.rootStore.notificationsStore.addNotification({
					title: data.title,
					message: data.message,
					category: "info",
					type: "toast",
					isShowed: false,
					currentProgress: data.currentProgress,
					maxProgress: data.maxProgress,
				});
			}
		);
	}

	private buildConnection(auth: string, route: string) {
		return new HubConnectionBuilder()
			.withUrl(`${apiConfig.baseURL}${apiRoutes.WEB_SOCKET.NOTIFICATION}`, {
				accessTokenFactory: () => auth,
			})
			.build();
	}
}
