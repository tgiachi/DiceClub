import React from "react";
import { observer } from "mobx-react-lite";
import { CardDto } from "../../../schemas/dice-club";
import { Card, Divider, Icon, Image, Label } from "semantic-ui-react";
import { UserLabel } from "./users/user.label";

export const DiceCard = observer(({ card }: { card: CardDto }) => {
	return (
		<Card fluid>
			<Image src={card.imageUrl} wrapped ui={false}></Image>
			<Card.Content>
				<Card.Header>{card.cardName}</Card.Header>
				<Card.Meta>
					<span className="date">{card.cardType?.cardType}</span>
				</Card.Meta>
				<Card.Description>{card.description}</Card.Description>
			</Card.Content>
			<Card.Content extra>
				<Label>
					<Image floated="left" size="mini" src={card.cardSet?.image!} />
					{card.cardSet?.description}
				</Label>
				<UserLabel id={card.userId!} />
			</Card.Content>
		</Card>
	);
});
