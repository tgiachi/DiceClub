import { observer } from "mobx-react";
import React, { useEffect } from "react";
import { Form } from "semantic-ui-react";
import { ICardColor } from "../../../../interfaces/cards/cardcolor.interfaces";
import { useStore } from "../../../../stores/store.context";

export const CardTypeDropDown = observer(() => {
	const { rootStore } = useStore();
	const [types, setTypes] = React.useState([]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.types = types;
		rootStore.cardsStore.setQuery = qry;
	}, [types]);

	return (
		<>
			<Form.Select
				label="Tipologia"
				selection
				multiple
				search
				onChange={(e, data) => {
					setTypes(data.value as []);
				}}
				options={rootStore.cardsStore.getTypes.map((s) => {
					return {
						key: s.id,
						text: s.cardType,
						value: s.cardType
					} as ICardColor;
				})}
			></Form.Select>
		</>
	);
});
