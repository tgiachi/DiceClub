import React, { useCallback, useEffect } from "react";

import { observer } from "mobx-react";

import { useStore } from "../../../stores/store.context";

import { Select } from "semantic-ui-react";
import { MtgCardLanguageDto } from "../../../schemas/dice-club";

export const LanguageSelectComponent = observer(
	({
		multiselect = false,
		callback,
	}: {
		multiselect?: boolean;
		callback: (rarities: string[]) => void;
	}) => {
		const [languages, setSelect] = React.useState([] as MtgCardLanguageDto[]);
		const [selectedLanguages, setSelectedLanguages] = React.useState(
			[] as string[]
		);
		const { rootStore } = useStore();
		useEffect(() => {
			setSelect(rootStore.cardsStore.languages!);
		}, [rootStore.cardsStore.languages]);

		useCallback(() => {
			callback(selectedLanguages);
		}, [selectedLanguages]);

		return (
			<Select
			  placeholder="Select Language"
				fluid
				multiple={multiselect}
				search
				clearable
        onChange={(e, data) => {
					callback(data.value as string[]);
					setSelectedLanguages(data.value as string[]);
				}}
				selection
				options={languages.map((s) => {
					return {
						key: s.code!,
						value: s.code!,
						text: s.name,
					};
				})}
			></Select>
		);
	}
);
