import React from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import { Select } from "semantic-ui-react";
import { t } from "i18next";

export const RaritiesSelect = observer(
	({
		multiselect = false,
		callback,
	}: {
		multiselect?: boolean;
		callback: (rarities: string[]) => void;
	}) => {
		const { rootStore } = useStore();
		const [selectedRarities, setSelectedRarities] = React.useState(
			[] as string[]
		);
		const [rarities, setRarities] = React.useState(
			rootStore.cardsStore.rarities
		);
		React.useEffect(() => {
			setRarities(rootStore.cardsStore.rarities);
		}, [rootStore.cardsStore.rarities]);

		React.useCallback(() => {
			callback(selectedRarities);
		}, [selectedRarities]);

		return (
			<Select
				fluid
				placeholder={t("cards.select_rarity.title")}
				multiple={multiselect}
				search
				clearable
				selection
				onChange={(e, data) => {
					callback(data.value as string[]);
					setSelectedRarities(data.value as string[]);
				}}
				options={rarities.map((s) => {
					return {
						key: s.id!,
						value: s.name!,
						text: s.name!,
					};
				})}
			></Select>
		);
	}
);
