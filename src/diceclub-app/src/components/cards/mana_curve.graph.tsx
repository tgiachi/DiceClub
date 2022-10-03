import React from "react";
import {
	BarChart,
	Bar,
	Cell,
	XAxis,
	YAxis,
	CartesianGrid,
	Tooltip,
	Legend,
	ResponsiveContainer,
	PieChart,
	Pie,
} from "recharts";
import { Container, Grid } from "semantic-ui-react";
import { MtgCardDto } from "../../schemas/dice-club";

export const ManaCurveGraphComponent = ({
	cards,
}: {
	cards?: MtgCardDto[];
}) => {
	const manaCurveArray = [] as { name: string; card_count: number }[];
	const cardTypesArray = [] as { name: string; card_count: number }[];

	const groupedByType = cards!.reduce<Map<string, MtgCardDto[]>>(
		(acc: Map<string, MtgCardDto[]>, card: MtgCardDto) => {
			const type = card.type?.name!;
			if (!acc.get(type)) acc.set(type, []);
			acc.set(type, [...acc.get(type)!, card!]);
			return acc;
		},
		new Map<string, []>()
	);
	groupedByType.forEach((value, key) => {
		cardTypesArray.push({ name: key, card_count: value.length });
	});

	for (let i = 0; i < 7; i++) {
		if (i < 6) {
			manaCurveArray.push({
				name: `${i + 1}`,
				card_count: cards?.filter((c) => c.cmc! === i + 1).length || 0,
			});
		} else {
			manaCurveArray.push({
				name: `${i + 1}+`,
				card_count: cards?.filter((c) => c.cmc! >= i + 1).length || 0,
			});
		}
	}

	return (
		<Grid>
			<Grid.Column width={8}>
				<ResponsiveContainer width="100%" height={300}>
					<BarChart data={manaCurveArray}>
						<CartesianGrid strokeDasharray="2 2" />
						<XAxis dataKey="name" />
						<YAxis />
						<Tooltip />
						<Legend />
						<Bar dataKey="card_count" fill="#8884d8" />
					</BarChart>
				</ResponsiveContainer>
			</Grid.Column>
			<Grid.Column width={8}>
				<ResponsiveContainer width="100%" height={300}>
					<PieChart>
						<Pie
							dataKey="card_count"
							startAngle={360}
							endAngle={0}
							data={cardTypesArray}
							cx="50%"
							cy="50%"
							outerRadius={80}
							fill="#8884d8"
							label
						/>
						<Tooltip />
					</PieChart>
				</ResponsiveContainer>
			</Grid.Column>
		</Grid>
	);
};
