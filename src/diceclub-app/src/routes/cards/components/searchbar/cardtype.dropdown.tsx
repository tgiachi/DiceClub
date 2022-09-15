import React from "react";
import { Form } from "semantic-ui-react";
import { ICardColor } from "../../../../interfaces/cards/cardcolor.interfaces";
import { useStore } from "../../../../stores/store.context";

export const CardTypeDropDown = () => {
	const { rootStore } = useStore();

	return (
		<>
			<Form.Select
				label="Tipologia"
				selection
				multiple
				search
				options={rootStore.cardsStore.cardTypes.map((s) => {
					return {
						key: s.id,
						text: s.cardType,
						value: s.cardType
					} as ICardColor;
				})}
			></Form.Select>
		</>
	);
};
