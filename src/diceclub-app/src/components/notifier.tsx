import { observer } from "mobx-react";
import { useStore } from "../stores/store.context";
import { INotification } from "../interfaces/notification";
import React, { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import parser from "html-react-parser";

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

	useEffect(() => {
		const filteredToasts = toasts.filter(
			(t) => t.type == "toast" && !t.isShowed
		);
		filteredToasts.forEach((t) => {
			toast(t.message, {
				position: "top-right",
				icon: "🦄",
				progress: t.currentProgress,
			});
		});
	}, [toasts]);

	return (
		<div>
			<ToastContainer position="top-right" newestOnTop />
		</div>
	);
});
