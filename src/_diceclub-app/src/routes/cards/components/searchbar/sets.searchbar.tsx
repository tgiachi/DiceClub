import { DropdownItem, Form } from "semantic-ui-react";
import { useStore } from "../../../../stores/store.context";
import { ICardColor } from "../../../../interfaces/cards/cardcolor.interfaces";
import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";

export const SetsDropDown = observer(() => {
	const { rootStore } = useStore();
	const [sets, setSets] = React.useState([]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.sets = sets;
		rootStore.cardsStore.setQuery = qry;
	}, [sets]);

	return (
		<>
			<Form.Select
				label="Set"
				selection
				multiple
				search
				onChange={(e, data) => {
					setSets(data.value as []);
				}}
				options={rootStore.cardsStore.getSets.map((s) => {
					return {
						key: s.id,
						text: s.description,
						value: s.setCode,
						image: {
							avatar: true,
							src: s.image,
							className: "mini"
						}
					} as ICardColor;
				})}
			></Form.Select>
		</>
	);
});
