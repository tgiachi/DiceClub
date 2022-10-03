import { ApiClientStore } from "./apiClientStore";
import { CardsStore } from "./cardsStore";
import { DeckStore } from "./deckStore";
import { NotificationsStore } from "./notificationsStore";
import { PreloadStore } from "./preLoadStore";
import { WebSocketStore } from "./webSocketStore";

export class RootStore {
	apiClient: ApiClientStore;
	notificationsStore: NotificationsStore;
	webSocketStore: WebSocketStore;
	preloadStore: PreloadStore;
	// App stores
	cardsStore: CardsStore;
	deckStore: DeckStore;

	constructor() {
		this.apiClient = new ApiClientStore(this);
		this.notificationsStore = new NotificationsStore(this);
		this.webSocketStore = new WebSocketStore(this);
		this.cardsStore = new CardsStore(this);
		this.deckStore = new DeckStore(this);
		this.preloadStore = new PreloadStore(
			[this.webSocketStore, this.cardsStore],
			this
		);
		this.apiClient.init();
	}
}
