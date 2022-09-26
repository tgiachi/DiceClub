import React from "react";
import { Card, Image, Label } from "semantic-ui-react";
import { MtgCardDto } from "../../schemas/dice-club";

export const CardItem = ({ card }: { card: MtgCardDto }) => {
	return (
		<Card>
			<Image
				src={
					card.highResImageUrl ||
					card.lowResImageUrl ||
					"https://c1.scryfall.com/file/scryfall-card-backs/large/59/597b79b3-7d77-4261-871a-60dd17403388.jpg?1562636676"
				}
				wrapped
				ui={false}
			/>
			<Card.Content>
				<Card.Header>{card.name}</Card.Header>
				<Card.Meta>{card.type?.name}</Card.Meta>
				<Card.Description>{card.description}</Card.Description>
			</Card.Content>
			<Card.Content extra>
				<Label>
					<Image floated="left" size="mini" src={card.set?.image!} />
					{card.set?.description}
				</Label>
				{/* <UserLabel id={card.ownerId!} /> */}
			</Card.Content>
		</Card>
	);
};
