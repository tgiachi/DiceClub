import React, { useEffect, useState } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import {
	Button,
	Checkbox,
	Grid,
	GridColumn,
	Header,
	Icon,
	Segment,
} from "semantic-ui-react";
import { Link, useLocation, useParams } from "react-router-dom";
import { DeckDetailGrid } from "../../deck/details/deck_details.grid";
import { DeckDetailList } from "../../deck/details/deck_details.list";

import { DeckDetailCardType, DeckDetailDto } from "../../../schemas/dice-club";
import { ColorImageComponent } from "../../cards/color_image";
import { ManaCurveGraphComponent } from "../../cards/mana_curve.graph";
import { appRoutes } from "../routes";

export const DeckDetailPage = observer(() => {
	const { deckId } = useParams();

	const { rootStore } = useStore();
	const { deckStore } = rootStore;
	const [deckDetails, setDeckDetails] = useState(deckStore.deckDetails);
	const [deckMaster, setDeckMaster] = useState(deckStore.selectedDeckMaster!);
	const [viewGrid, setViewGrid] = useState(deckStore.viewListSelection);
	const [mainCards, setMainCards] = useState([] as DeckDetailDto[]);

	useEffect(() => {
		deckStore.getDeckDetails(deckId!);
		deckStore.selectDeckMaster(deckId!);
	}, []);

	useEffect(() => {
		deckStore.toggleViewListSelection(viewGrid);
	}, [viewGrid]);

	useEffect(() => {
		setDeckMaster(deckStore.selectedDeckMaster!);
	}, [deckStore.selectedDeckMaster!]);

	useEffect(() => {
		setDeckDetails(deckStore.deckDetails);
		setMainCards(
			deckStore.deckDetails
				.filter((x) => x.cardType === DeckDetailCardType.Main)
				.map((s) => s.card!)
		);
	}, [deckStore.deckDetails, deckStore.selectedDeckMaster]);

	return (
		<>
			<Segment.Group>
				<Segment>
					<Grid columns={3}>
						<Grid.Row>
							<Grid.Column>
								<Button.Group>
									<Button icon>
										<Link to={appRoutes.DECK.DECKS}>
											<Icon name="step backward" />
										</Link>
									</Button>
									<Button icon
									active={viewGrid}
									onClick={() => {
										setViewGrid(true);
									}}
									>
										<Icon name="grid layout" />
									</Button>

									<Button
										icon
										active={!viewGrid}
										onClick={() => {
											setViewGrid(false);
										}}
									>
										<Icon name="list layout" />
									</Button>
								</Button.Group>
							</Grid.Column>
							<Grid.Column>
								<Header as="h2" textAlign="center">
									{deckMaster?.name}
								</Header>
							</Grid.Column>
							<Grid.Column textAlign="right">
								<ColorImageComponent
									color={deckMaster?.colorIdentity || "N/A"}
								/>
							</Grid.Column>
						</Grid.Row>
					</Grid>
				</Segment>
				<Segment>
					{/* <ManaCurveComponent cards={mainCards} /> */}
					<ManaCurveGraphComponent cards={mainCards} />
					{viewGrid && <DeckDetailGrid details={deckDetails} />}
					{!viewGrid && <DeckDetailList details={deckDetails} />}
				</Segment>
			</Segment.Group>
		</>
	);
});
