import { observer } from "mobx-react";
import React, { FC, useEffect } from "react";
import { useStore } from "../stores/store.context";
import { Message } from "semantic-ui-react";

export const ErrorsBar: FC = observer(() => {
	const { rootStore } = useStore();
	const [errors, setErrors] = React.useState(rootStore.errorsStore.errors);
	useEffect(() => {
		setErrors(rootStore.errorsStore.errors);
	}, [rootStore.errorsStore.errors]);

	return (
		<div>
			{errors.map((error, index) => {
				return (
					<Message key={index} negative>
						<Message.Header>Error</Message.Header>
						<p>{error.message}</p>
					</Message>
				);
			})}
		</div>
	);
});
