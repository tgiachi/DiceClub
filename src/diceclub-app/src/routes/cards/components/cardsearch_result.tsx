import React, { useEffect } from "react";
import { Grid, Image } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/store.context";
import { DiceCard } from "./card.component";

export const CardSearchResultTable = observer(() => {
	const { rootStore } = useStore();
	const cardsStore = rootStore.cardsStore;

	const [cards, setCards] = React.useState(cardsStore.getSearchedCards);
	useEffect(() => {
		setCards(cardsStore.getSearchedCards);
	}, [cardsStore.getSearchedCards]);

	const [cardViewNumber, setCardViewNumber] = React.useState(cardsStore.cardTableView);

	return (
		<Grid columns={cardViewNumber} divided>
			<Grid.Row>
				{cards?.map((card) => {
					return (
						<Grid.Column key={card.id}>
							<DiceCard card={card} />
						</Grid.Column>
					);
				})}
			</Grid.Row>
		</Grid>
	);
});
