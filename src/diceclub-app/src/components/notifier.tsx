import { observer } from "mobx-react";
import { useStore } from "../stores/store.context";
import { INotification } from "../interfaces/notification";
import React, { useEffect, useState } from "react";
import toast, { Toaster } from "react-hot-toast";

export const NotifierComponent = observer(() => {
	const { notificationsStore } = useStore().rootStore;

	const [toasts, setToasts] = useState([] as INotification[]);

	useEffect(() => {
		console.log("recevied notification");
		const notification = notificationsStore.getNotifications.pop();
		if (notification) {
			setToasts([notification]);
		}
	}, [notificationsStore.getNotifications]);

	return (
		<div>
			<Toaster position="top-right" />
			{toasts
				.filter((s) => s.type === "toast")
				.map((t) => {
					toast(t.message);
					return "";
				})}
			;
		</div>
	);
});
