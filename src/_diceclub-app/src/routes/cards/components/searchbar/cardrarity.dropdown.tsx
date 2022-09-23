import { observer } from "mobx-react";
import React, { useEffect } from "react";
import { Form } from "semantic-ui-react";
import { ICardColor } from "../../../../interfaces/cards/cardcolor.interfaces";
import { useStore } from "../../../../stores/store.context";

export const CardRarityDropDown = observer(() => {
	const { rootStore } = useStore();
	const [types, setTypes] = React.useState([]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.rarity = types;
		rootStore.cardsStore.setQuery = qry;
	}, [types]);

	return (
		<>
			<Form.Select
				label="Rarity"
				selection
				multiple
				search
				onChange={(e, data) => {
					setTypes(data.value as []);
				}}
				options={rootStore.cardsStore.getRarities.map((s) => {
					return {
						key: s.id,
						text: s.name,
						value: s.name
					} as ICardColor;
				})}
			></Form.Select>
		</>
	);
});
