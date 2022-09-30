import React from "react";
import { Card, Grid, Header, Image, Label } from "semantic-ui-react";
import { MtgCardDto } from "../../schemas/dice-club";
import { CardDescriptionSymbol } from "./card.description.symbol";

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
					<Header as="h4" textAlign="center">
						{card.name}
					</Header>
				</Card.Header>
				<Card.Meta>
					<Header as="h5" textAlign="center" style={{ fontStyle: "italic" }}>
						{card.type?.name}
					</Header>
				</Card.Meta>
				{showDetails ? (
					<Card.Description>
						<CardDescriptionSymbol description={card.description!} />{" "}
					</Card.Description>
				) : null}
			</Card.Content>

			{showDetails ? (
				<Card.Content extra>
					<Grid>
						<Grid.Row>
							<Grid.Column width={3}>
								<Header as="h3" textAlign="center">
									<Image floated="left" size="small" src={card.set?.image!} />
								</Header>
							</Grid.Column>
							<Grid.Column width={12}>
								<Header as="h6" textAlign="left">
									{card.set?.description}
								</Header>
							</Grid.Column>
						</Grid.Row>
					</Grid>

					{/* <UserLabel id={card.ownerId!} /> */}
				</Card.Content>
			) : null}
		</Card>
	);
};
