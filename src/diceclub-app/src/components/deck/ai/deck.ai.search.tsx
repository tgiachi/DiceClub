import React, { useEffect } from "react";

import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import {
	Button,
	Form,
	Icon,
	Label,
	Progress,
	Segment,
} from "semantic-ui-react";
import { CardColorSelect } from "../../cards/search/colors.select";

export const DeckAiSearchComponent = observer(() => {
	const { rootStore } = useStore();
	const deckStore = rootStore.deckStore;
	const [randomDeck, setRandomDeck] = React.useState(
		deckStore.multipleDeckQuery
	);

	useEffect(() => {
		setRandomDeck(deckStore.multipleDeckQuery);
	}, [deckStore.multipleDeckQuery]);

	return (
		<Segment>
			<Form
				onSubmit={() => {
					deckStore.createMultipleDecks();
				}}
			>
				<Form.Group widths="equal">
					<Form.Input
						label="Num mazzi"
						onChange={(v) => {
							deckStore.setMultipleDeckQuery({
								count: parseInt(v.target.value),
							});
						}}
					>
						<input type="number" value={randomDeck.count} />
					</Form.Input>
					<Form.Input
						onChange={(v) => {
							deckStore.setMultipleDeckQuery({
								totalCards: parseInt(v.target.value),
							});
						}}
						label="Num carte"
					>
						<input type="number" value={randomDeck.totalCards} />
					</Form.Input>
					<Form.Input
						onChange={(v) => {
							deckStore.setMultipleDeckQuery({
								sideBoardTotalCards: parseInt(v.target.value),
							});
						}}
						label="Num sideboard"
					>
						<input type="number" value={randomDeck.sideBoardTotalCards} />
					</Form.Input>
					<Form.Field>
						<label>Colori</label>
						<CardColorSelect
							multiselect
							callback={(a) => {
								deckStore.setMultipleDeckQuery({
									colors: a,
								});
							}}
						/>
					</Form.Field>
				</Form.Group>
				<Form.Group widths="16">
					<Progress autoSuccess  active percent={10} size="large" value={30} total={100} />
				</Form.Group>
				<Form.Group>
					<Button loading={rootStore.apiClient.isLoading} type="submit" primary>
						<Icon name="search" />
						Cerca
					</Button>
				</Form.Group>
			</Form>
		</Segment>
	);
});
