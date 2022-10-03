import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import { Segment } from "semantic-ui-react";
import { DeckTable } from "../../deck/deck.table";

export const DeckPage = observer(() => {
	const { rootStore } = useStore();
	const [decks, setDecks] = React.useState(rootStore.deckStore.decks);

	useEffect(() => {
		rootStore.deckStore.getDecksMaster();
	}, []);

	React.useEffect(() => {
		setDecks(rootStore.deckStore.decks);
	}, [rootStore.deckStore.decks]);

	return (
		<>
			<Segment>
				<DeckTable />
			</Segment>
		</>
	);
});
