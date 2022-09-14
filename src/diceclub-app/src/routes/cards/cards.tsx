import React from "react";
import { observer } from "mobx-react-lite";
import { Button } from "semantic-ui-react";
import { useStore } from "../../stores/store.context";
import { CardSearchResultTable } from "./components/cardsearch_result";
import { Container } from "semantic-ui-react";
import { CardSearchBar } from "./components/card.searchbar";

export const CardContainer = observer(() => {
	const { rootStore } = useStore();
	const cardsStore = rootStore.cardsStore;

	return (
		<>
			<Button
				onClick={() => {
					cardsStore.searchCards();
				}}
			>
				Search
			</Button>
			<Button
				onClick={() => {
					cardsStore.nextPage();
				}}
			>
				Next page
			</Button>

			<Button
				onClick={() => {
					cardsStore.prevPage();
				}}
			>
				Prev page
			</Button>
			<CardSearchBar />
			<Container>
				<CardSearchResultTable />
			</Container>
		</>
	);
});
