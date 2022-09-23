import React, { useEffect } from "react";
import { Form } from "semantic-ui-react";
import { useStore } from "../../../../stores/store.context";

export const DescriptionSearch = () => {
	const { rootStore } = useStore();
	const [description, setDescription] = React.useState("");

	useEffect(() => {
		const qry = rootStore.cardsStore.getQuery;
		qry.description = description;
		rootStore.cardsStore.setQuery = qry;
	}, [description]);

	return (
		<>
			<Form.Input
				fluid
				label="Descrizione"
				onChange={(e) => {
					setDescription(e.target.value);
				}}
				placeholder="Description"
				content={description}
			></Form.Input>
		</>
	);
};
