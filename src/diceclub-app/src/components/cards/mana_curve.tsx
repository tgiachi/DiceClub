import React from "react";
import { Sparklines, SparklinesBars } from "react-sparklines-typescript";
import { MtgCardDto } from "../../schemas/dice-club";

export const ManaCurveComponent = ({ cards }: { cards?: MtgCardDto[] }) => {
	const manaCurveArray = [] as number[];
	for (let i = 0; i < 7; i++) {
		if (i < 6) {
			manaCurveArray.push(cards?.filter((c) => c.cmc! === i + 1).length || 0);
		} else {
			manaCurveArray.push(cards?.filter((c) => c.cmc! >= i + 1).length || 0);
		}
	}

	return (
		<Sparklines data={manaCurveArray}>
			<SparklinesBars />
		</Sparklines>
	);
};
