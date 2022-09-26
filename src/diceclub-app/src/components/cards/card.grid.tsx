import React from "react";
import { MtgCardDto } from "../../schemas/dice-club";
import {
	Segment,
	Card,
	SemanticWIDTHS,
	Pagination,
	Grid,
} from "semantic-ui-react";
import { CardItem } from "./card.item";
import { useStore } from "../../stores/store.context";

export const CardGrid = ({
	cards,
	itemsPerRow = 5,
}: {
	cards: MtgCardDto[];
	itemsPerRow: SemanticWIDTHS;
}) => {
	const { rootStore } = useStore();

	return (
		<Segment.Group>
			<Segment>
				<Pagination
					onPageChange={(e, data) => {
						rootStore.cardsStore.goToPageOwnedCardSearch(
							data.activePage as number
						);
					}}
					totalPages={rootStore.cardsStore.ownedCardSearchTotalPages}
				/>
			</Segment>
			<Card.Group itemsPerRow={itemsPerRow}>
				{cards.map((c) => {
					return <CardItem key={c.id} card={c} />;
				})}
			</Card.Group>
		</Segment.Group>
	);
};
