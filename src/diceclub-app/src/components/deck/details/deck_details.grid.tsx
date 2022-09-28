import React from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import { Grid, Header } from "semantic-ui-react";
import {
	DeckDetailCardType,
	DeckDetailDto,
	MtgCardDto,
} from "../../../schemas/dice-club";
import { CardGrid } from "../../cards/card.grid";
import { CardResultGrid } from "../../cards/search/cards.results";
import { ManaCurveComponent } from "../../cards/mana_curve";

export const DeckDetailGrid = observer(
	({ details }: { details?: DeckDetailDto[] }) => {
		const landCards = details!
			.filter((s) => s.cardType == DeckDetailCardType.Land)
			.map((k) => k.card!)!;
		const mainCards = details!
			.filter((s) => s.cardType == DeckDetailCardType.Main)
			.sort((a, b) => (a.card!.type?.name! < b.card!.type?.name! ? 1 : 0))
			.map((k) => k.card!)!;

		return (
			<Grid celled columns={2}>
				<Grid.Row></Grid.Row>
				<Grid.Row>
					<ManaCurveComponent cards={mainCards} />
				</Grid.Row>
				<Grid.Row>
					<Grid.Column width={3}>
						<Grid.Row>
							<Header as="h3" textAlign="center">
								Terre
							</Header>
						</Grid.Row>
						<Grid.Row>
							<CardResultGrid
								cards={landCards}
								itemsPerRow="1"
								showDetails={false}
							/>
						</Grid.Row>
					</Grid.Column>
					<Grid.Column width={"13"}>
						<Grid.Row>
							<Header as="h3" textAlign="center">
								Principale
							</Header>
						</Grid.Row>
						<Grid.Row>
							<CardResultGrid
								cards={
									mainCards
								}
								itemsPerRow="6"
								showDetails={false}
							/>
						</Grid.Row>
					</Grid.Column>
				</Grid.Row>
				<Grid.Row>
					<Grid.Column width={16}>
						<Grid.Row>
							<Header as="h3" textAlign="center">
								Sideboard
							</Header>
						</Grid.Row>
						<Grid.Row>
							<CardResultGrid
								cards={
									details!
										.filter((s) => s.cardType == DeckDetailCardType.SideBoard)
										.map((k) => k.card!)!
								}
								itemsPerRow="6"
								showDetails={false}
							/>
						</Grid.Row>
					</Grid.Column>
				</Grid.Row>
			</Grid>
		);
	}
);
