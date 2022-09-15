import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Form, Icon, Segment, Select } from "semantic-ui-react";
import { useStore } from "../../../../stores/store.context";
import { ICardColor } from "../../../../interfaces/cards/cardcolor.interfaces";
import { ColorDropDown } from "./color.dropdown";
import { DescriptionSearch } from "./description.input";
import { SetsDropDown } from "./sets.searchbar";
import { CardTypeDropDown } from "./cardtype.dropdown";

export const CardSearchBar = observer(() => {
	const { rootStore } = useStore();
	const [description, setDescription] = React.useState("");
	const [colors, setColors] = React.useState([]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.description = description;
		rootStore.cardsStore.setQuery = qry;
	}, [description]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.colors = colors;
		rootStore.cardsStore.setQuery = qry;
	}, [colors]);

	return (
		<Segment>
			<Form>
				<Form.Group widths="equal">
					<DescriptionSearch />
					<ColorDropDown />
					<SetsDropDown />
					<CardTypeDropDown />
				</Form.Group>
				<Form.Group>
					<Form.Button
						primary
						onClick={() => {
							rootStore.cardsStore.searchCards();
						}}
					>
						Search
					</Form.Button>
				</Form.Group>
			</Form>
		</Segment>
	);
});
