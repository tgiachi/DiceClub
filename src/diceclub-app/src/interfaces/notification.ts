export type INotificationCategory = "info" | "warning" | "error";
export type INotificationType = "toast" | "message";

export interface INotification {
	id?: string;
	title: string;
	message: string;
	category: INotificationCategory;
	type: INotificationType;
	isShowed: boolean;
	currentProgress?: number;
	maxProgress?: number;
}
