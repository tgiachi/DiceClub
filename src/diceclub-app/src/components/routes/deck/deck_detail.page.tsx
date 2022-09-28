import React, { useEffect, useState } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import { Button, Checkbox, Icon, Segment } from "semantic-ui-react";
import { useLocation, useParams } from "react-router-dom";
import { DeckDetailGrid } from "../../deck/details/deck_details.grid";
import { DeckDetailList } from "../../deck/details/deck_details.list";
import { ManaCurveComponent } from "../../cards/mana_curve";
import { DeckDetailCardType, DeckDetailDto } from "../../../schemas/dice-club";

export const DeckDetailPage = observer(() => {
	const { deckId } = useParams();
	const { rootStore } = useStore();
	const { deckStore } = rootStore;
	const [deckDetails, setDeckDetails] = useState(deckStore.deckDetails);
	const [viewGrid, setViewGrid] = useState(true);
	const [mainCards, setMainCards] = useState([] as DeckDetailDto[]);

	useEffect(() => {
		deckStore.getDeckDetails(deckId!);
	}, []);

	useEffect(() => {
		setDeckDetails(deckStore.deckDetails);
		setMainCards(
			deckStore.deckDetails
				.filter((x) => x.cardType === DeckDetailCardType.Main)
				.map((s) => s.card!)
		);
	}, [deckStore.deckDetails]);

	return (
		<>
			<Segment.Group>
				<Segment>
					<Button.Group>
						<Button
							icon
							onClick={() => {
								setViewGrid(true);
							}}
						>
							<Icon name="grid layout" />
						</Button>

						<Button
							icon
							onClick={() => {
								setViewGrid(false);
							}}>
							<Icon name="list layout" />
						</Button>
					</Button.Group>
				</Segment>
				<Segment>
					<ManaCurveComponent cards={mainCards} />
					{viewGrid && <DeckDetailGrid details={deckDetails} />}
					{!viewGrid && <DeckDetailList details={deckDetails} />}
				</Segment>
			</Segment.Group>
		</>
	);
});
