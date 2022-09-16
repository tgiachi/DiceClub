import React, { useEffect } from "react";
import { Card, Grid, Image, Pagination, Segment, Statistic, Sticky } from "semantic-ui-react";
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
		<Segment.Group>
			<Segment>
		
			</Segment>
			<Segment>
				<Pagination
					onPageChange={(e, data) => {
						cardsStore.goToPage(data.activePage as number);
					}}
					defaultActivePage={cardsStore.currentPage}
					totalPages={cardsStore.totalPages}
				></Pagination>
					<Statistic >
					<Statistic.Value>{cardsStore.getTotalCards}</Statistic.Value>
					<Statistic.Label>Carte</Statistic.Label>
				</Statistic>
			</Segment>
			<Segment>
				<Card.Group itemsPerRow={cardViewNumber}>
					{cards?.map((card) => {
						return <DiceCard key={card.id} card={card} />;
					})}
				</Card.Group>
			</Segment>
			<Segment>
				<Pagination
					onPageChange={(e, data) => {
						cardsStore.goToPage(data.activePage as number);
					}}
					defaultActivePage={cardsStore.currentPage}
					totalPages={cardsStore.totalPages}
				></Pagination>
			</Segment>
		</Segment.Group>
	);
});
