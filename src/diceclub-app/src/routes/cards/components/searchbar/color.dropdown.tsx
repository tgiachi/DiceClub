import React, { useEffect } from "react";
import { Form, Icon, Segment, Select } from "semantic-ui-react";
import { useStore } from "../../../../stores/store.context";
export const ColorDropDown = () => {
	const { rootStore } = useStore();
	const [colors, setColors] = React.useState([]);

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.colors = colors;
		rootStore.cardsStore.setQuery = qry;
	}, [colors]);

	return (
		<>
			<Form.Select
				fluid
				label="Colore"
				options={rootStore.cardsStore.cardColors}
				selection
				multiple
				onChange={(e, data) => {
					setColors(data.value as []);
				}}
				selectedLabel={colors.length > 0 ? colors.length + " colors selected" : "Colors"}
			></Form.Select>
		</>
	);
};
