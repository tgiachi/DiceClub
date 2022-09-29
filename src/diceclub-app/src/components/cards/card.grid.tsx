import React from "react";
import { MtgCardDto } from "../../schemas/dice-club";
import {
	Segment,
	Card,
	SemanticWIDTHS,
	Pagination,
	Grid,
} from "semantic-ui-react";

import { useStore } from "../../stores/store.context";
import { CardResultGrid } from "./search/cards.results";

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
			{cards.length > 0 ? (
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
			) : (
				<></>
			)}

			<CardResultGrid cards={cards} itemsPerRow={itemsPerRow} />
		</Segment.Group>
	);
};
