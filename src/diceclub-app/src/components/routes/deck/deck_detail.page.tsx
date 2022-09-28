import React, { useEffect, useState } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import { Segment } from "semantic-ui-react";
import { useLocation, useParams } from "react-router-dom";
import { DeckDetailGrid } from "../../deck/details/deck_details.grid";

export const DeckDetailPage = observer(() => {
	const { deckId } = useParams();
	const { rootStore } = useStore();
	const { deckStore } = rootStore;
	const [deckDetails, setDeckDetails] = useState(deckStore.deckDetails);

  useEffect(() => {
    deckStore.getDeckDetails(deckId!);
  }, [])

	useEffect(() => {
		setDeckDetails(deckStore.deckDetails);
	}, [deckStore.deckDetails]);


	return (
		<>
			<DeckDetailGrid details={deckDetails}  />
		</>
	);
});
