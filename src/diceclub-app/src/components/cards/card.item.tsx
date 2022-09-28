import React from "react";
import { Card, Header, Image, Label } from "semantic-ui-react";
import { MtgCardDto } from "../../schemas/dice-club";

export const CardItem = ({
	card,
	showDetails = true,
}: {
	card: MtgCardDto;
	showDetails?: boolean;
}) => {
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
				<Card.Header>
					<Header as="h4" textAlign="center"> {card.name}</Header>
				</Card.Header>
				<Card.Meta>
					<Header as="h5" textAlign="center" > {card.type?.name}</Header>
				</Card.Meta>
				{showDetails ? (
					<Card.Description>{card.description}</Card.Description>
				) : null}
			</Card.Content>

			{showDetails ? (
				<Card.Content extra>
					<Label>
						<Image floated="left" size="mini" src={card.set?.image!} />
						{card.set?.description}
					</Label>
					{/* <UserLabel id={card.ownerId!} /> */}
				</Card.Content>
			) : null}
		</Card>
	);
};
