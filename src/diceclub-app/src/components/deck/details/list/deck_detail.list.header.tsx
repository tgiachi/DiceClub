import React from "react";
import { Label, List } from "semantic-ui-react";
import { MtgCardDto } from "../../../../schemas/dice-club";

export const DeckDetailListHeader = ({
	title,
	cards,
}: {
	title: string;
	cards: MtgCardDto[];
}) => {
	return (
		<List divided inverted relaxed>
			<List.Header>
				<Label>{title}</Label>
			</List.Header>
			{cards.map((s) => {
			return (
        <List.Item key={s.id}>
        <List.Content>
          <List.Header>Snickerdoodle</List.Header>
          An excellent companion
        </List.Content>
      </List.Item>
      )
			})}
		</List>
	);
};
