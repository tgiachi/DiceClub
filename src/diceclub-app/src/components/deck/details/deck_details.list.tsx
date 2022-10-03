import React from "react";
import { List } from "semantic-ui-react";
import { DeckDetailDto, MtgCardDto } from "../../../schemas/dice-club";
import { DeckDetailListHeader } from "./list/deck_detail.list.header";

export const DeckDetailList = ({ details }: { details: DeckDetailDto[] }) => {
	const cards = details.filter((d) => d.card !== null);
	const groupedByType = cards.reduce<Map<string, MtgCardDto[]>>(
		(acc: Map<string, DeckDetailDto[]>, card: DeckDetailDto) => {
			const type = card.card!.type?.name!;
			if (!acc.get(type)) acc.set(type, []);
			acc.set(type, [...acc.get(type)!, card!]);
			return acc;
		},
		new Map<string, []>()
	);

	return (
		<>
			<List celled>
				{Array.from(groupedByType.keys()).map((type) => {
					return (
						<DeckDetailListHeader
							key={type}
							title={type}
							cards={groupedByType.get(type)!}
						/>
					);
				})}
			</List>
		</>
	);
};
