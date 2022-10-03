import React from "react";
import {
	Label,
	List,
	Popup,
	Segment,
	Image,
	Grid,
	Header,
} from "semantic-ui-react";
import { DeckDetailDto, MtgCardDto } from "../../../../schemas/dice-club";
import { ColorImageComponent } from "../../../cards/color_image";

export const DeckDetailListHeader = ({
	title,
	cards,
}: {
	title: string;
	cards: DeckDetailDto[];
}) => {
	return (
		<Segment vertical>
			<Label attached="top">
				{title} ({cards.length})
			</Label>
			<List celled>
				{cards.map((c) => {
					return (
						<List.Item key={c.id}>
							<List.Content>
								<Grid>
									<Grid.Column width={3}>
										<Grid.Row verticalAlign="middle">
											<ColorImageComponent color={c.card!.manaCost!} />
										</Grid.Row>
									</Grid.Column>
									<Grid.Column width={13}>
										<Grid.Row verticalAlign="middle">
											<Popup
												trigger={<Header as={"h5"}> ({c.quantity})  {c.card!.name} </Header>}
												content={
													<Image
														src={
															c.card!.highResImageUrl || c.card!.lowResImageUrl
														}
													/>
												}
											/>
										</Grid.Row>
									</Grid.Column>
								</Grid>
							</List.Content>
						</List.Item>
					);
				})}
			</List>
		</Segment>
	);
};
