import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { Select } from "semantic-ui-react";
import { t } from "i18next";
import { useStore } from "../../../stores/store.context";

export const CardColorSelect = observer(
	({
		multiselect = false,
		callback,
	}: {
		multiselect: boolean;
		callback: (values: string[]) => void;
	}) => {
		const { rootStore } = useStore();
		const [colors, setColors] = React.useState(rootStore.cardsStore.colors);

		useEffect(() => {
			setColors(rootStore.cardsStore.colors);
		}, [rootStore.cardsStore.colors]);
		return (
			<Select
				placeholder={t("cards.select_color.title")}
				multiple={multiselect}
				onChange={(e, data) => {
					callback(data.value as string[]);
				}}
				options={colors!.map((s) => {
					return {
						key: s.name!,
						value: s.name!,
						text: s.description,
						image: {
							src: s.imageUrl,
							style: { width: "16px", height: "16px" },
						},
					};
				})}
			/>
		);
	}
);
