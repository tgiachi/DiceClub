import React from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";

export const DeckPage = observer(() => {
	const { rootStore } = useStore();
	const [decks, setDecks] = React.useState(rootStore.deckStore.decks);

	Promise.resolve(rootStore.deckStore.getDecksMaster());
	React.useEffect(() => {
		setDecks(rootStore.deckStore.decks);
	}, [rootStore.deckStore.decks]);

	return <></>;
});
