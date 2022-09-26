import React, { useCallback } from "react";

import { observer } from "mobx-react";

import { useStore } from "../../../stores/store.context";
import { MtgCardSetDto } from "../../../schemas/dice-club";
import { Select } from "semantic-ui-react";

export const SetSelectComponent = observer(
	({
		multiselect = false,
		callback,
	}: {
		multiselect: boolean;
		callback: (rarities: string[]) => void;
	}) => {
		const [sets, setSelect] = React.useState([] as MtgCardSetDto[]);
		const [selectedSets, setSelectedSets] = React.useState([] as string[]);
		const { rootStore } = useStore();

		React.useEffect(() => {
			setSelect(rootStore.cardsStore.sets!);
		}, [rootStore.cardsStore.sets]);

		useCallback(() => {
			callback(selectedSets);
		}, [selectedSets]);

		return (
			<Select
				placeholder="Select Set"
				fluid
				multiple
				search
				clearable
				selection
				onChange={(e, data) => {
					callback(data.value as string[]);
					setSelectedSets(data.value as string[]);
				}}
				options={sets.map((s) => {
					return {
						key: s.code!,
						value: s.code!,
						text: s.description!,
						image: {
							avatar: true,
							src: s.image,
							style: { width: "20px", height: "20px" },
						},
					};
				})}
			></Select>
		);
	}
);
