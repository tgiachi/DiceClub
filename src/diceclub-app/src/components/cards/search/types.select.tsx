import React, { useCallback, useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";

import { Select } from "semantic-ui-react";
import { MtgCardTypeDto } from "../../../schemas/dice-club";

export const TypesSelectComponent = observer(
	({
		multiselect = false,
		callback,
	}: {
		multiselect?: boolean;
		callback: (types: string[]) => void;
	}) => {
		const { rootStore } = useStore();
		const [types, setTypes] = React.useState([] as MtgCardTypeDto[]);
		const [selectedTypes, setSelectedTypes] = React.useState([] as string[]);

		useEffect(() => {
			setTypes(rootStore.cardsStore.types!);
		}, [rootStore.cardsStore.types]);

		useCallback(() => {
			callback(selectedTypes);
		}, [selectedTypes]);

		return (
			<Select
				placeholder="Select Type"
				fluid
				multiple={multiselect}
				search
				clearable
				selection
				onChange={(e, data) => {
					console.log(data.value);
					callback(data.value as string[]);
					setSelectedTypes(data.value as string[]);
				}}
				options={types.map((s) => {
					return {
						key: s.name,
						value: s.name!,
						text: s.name!,
					};
				})}
			></Select>
		);
	}
);
