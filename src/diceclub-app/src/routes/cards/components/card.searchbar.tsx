import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Form, Select } from "semantic-ui-react";
import { useStore } from "../../../stores/store.context";

export const CardSearchBar = observer(() => {
	const colorsConst = [
		{
			key: "R",
			text: "Red",
			value: "R"
		},
		{
			key: "G",
			text: "Green",
			value: "G"
		},
		{
			key: "B",
			text: "Blue",
			value: "B"
		},
		{
			key: "W",
			text: "White",
			value: "W"
		},
		{
			key: "U",
			text: "Black",
			value: "U"
		}
	];
	const { rootStore } = useStore();
	const [description, setDescription] = React.useState("");
	const [colors, setColors] = React.useState([]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.description = description;
		rootStore.cardsStore.setQuery = qry;
		rootStore.cardsStore.searchCards();
	}, [description]);
	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.colors = colors;
		rootStore.cardsStore.setQuery = qry;
		rootStore.cardsStore.searchCards();
	}, [colors]);

	return (
		<Form>
			<Form.Group widths="equal">
				<Form.Input
					fluid
					label="Descrizione"
					onChange={(e) => {
						setDescription(e.target.value);
					}}
					placeholder="Description"
					content={description}
				></Form.Input>
				<Form.Select
					fluid
					label="Colore"
					options={colorsConst}
					selection
					multiple
					onChange={(e, data) => {
						setColors(data.value as []);
					}}
					selectedLabel={colors.length > 0 ? colors.length + " colors selected" : "Colors"}
				></Form.Select>
			</Form.Group>
		</Form>
	);
});
