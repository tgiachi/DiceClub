import React from "react";
import { observer } from "mobx-react-lite";
import { CardDto } from "../../../schemas/dice-club";
import { Card, Image } from "semantic-ui-react";

export const DiceCard = observer(({ card }: { card: CardDto }) => {
	return (
		<Card>
			<Image src={card.imageUrl} wrapped ui={false}></Image>
			<Card.Content>
				<Card.Header>{card.cardName}</Card.Header>
				<Card.Meta>
					<span className="date">{card.cardType?.cardType}</span>
				</Card.Meta>
				<Card.Description>{card.description}</Card.Description>
			</Card.Content>
		</Card>
	);
});
