export type INotificationEventType = "Information" | "Warning" | "Error";
export interface INotificationWebSocket {
	title: string;
	message: string;
	type: INotificationEventType;
	currentProgress: number;
	maxProgress: number;
	isBroadcast: boolean;
}
