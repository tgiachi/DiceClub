import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../stores/store.context";
import { Table } from "semantic-ui-react";
import { Link } from "react-router-dom";

export const DeckTable = observer(() => {
	const { rootStore } = useStore();
	const [decks, setDecks] = React.useState(rootStore.deckStore.decks);

	useEffect(() => {
		setDecks(rootStore.deckStore.decks);
	}, [rootStore.deckStore.decks]);

	return (
		<Table>
			<Table.Header>
				<Table.Row>
					<Table.HeaderCell></Table.HeaderCell>
					<Table.HeaderCell>Deck Name</Table.HeaderCell>
          <Table.HeaderCell>Color identity</Table.HeaderCell>
					<Table.HeaderCell>Format</Table.HeaderCell>
          <Table.HeaderCell>Cards count</Table.HeaderCell>
					<Table.HeaderCell>Created</Table.HeaderCell>
					<Table.HeaderCell>Updated</Table.HeaderCell>
				</Table.Row>
        </Table.Header>
				<Table.Body>
					{decks.map((d) => {
						return (
							<Table.Row key={d.id}>
								<Table.Cell>
									<Link to={`/cards/decks/${d.id}`}>View</Link>
								</Table.Cell>
                <Table.Cell>{d.colorIdentity}</Table.Cell>
                <Table.Cell>{d.format}</Table.Cell>
								<Table.Cell>{d.name}</Table.Cell>
                <Table.Cell>{d.cardCount}</Table.Cell>
								<Table.Cell>{new Date(d.created!).toLocaleDateString()}</Table.Cell>
								<Table.Cell>{new Date(d.updated!).toLocaleDateString()}</Table.Cell>
							</Table.Row>
						);
					})}
				</Table.Body>

		</Table>
	);
});
