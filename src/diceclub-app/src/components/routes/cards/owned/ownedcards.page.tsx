import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../../stores/store.context";
import {
	Card,
	Grid,
	Header,
	Icon,
	Image,
	Segment,
	Container,
} from "semantic-ui-react";
import { CardSearch } from "../../../cards/card.search";
import { CardGrid } from "../../../cards/card.grid";
import { MtgCardDto } from "../../../../schemas/dice-club";

export const OwnedCardSearchPage = observer(() => {
	const { rootStore } = useStore();
	const cardStore = rootStore.cardsStore;
	const [cards, setCards] = React.useState([] as MtgCardDto[]);

	useEffect(() => {
		setCards(cardStore.ownedCardResult);
	}, [cardStore.ownedCardResult]);

	return (
		<>
			<CardSearch />
			<Segment>
				<CardGrid cards={cards} itemsPerRow={5} />
			</Segment>
		</>
	);
});
