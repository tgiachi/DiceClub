import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { Input } from "semantic-ui-react";
import { useStore } from "../../../stores/store.context";

export const CardSearchTextbox = observer(
	({ callback }: { callback: (text: string) => void }) => {
		const { rootStore } = useStore();
		const [loading, setLoading] = React.useState(rootStore.apiClient.isLoading);
		const [searchText, setSearchText] = React.useState("");
		useEffect(() => {
			setLoading(rootStore.apiClient.isLoading);
		});

		return (
			<Input
				onChange={(e, data) => {
					setSearchText(data.value as string);
					callback(data.value as string);
				}}
				loading={loading}
				icon="search"
				fluid
				iconPosition="left"
				placeholder="Ricerca..."
			/>
		);
	}
);
