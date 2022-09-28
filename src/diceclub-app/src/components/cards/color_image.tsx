import React, { useEffect, useState } from "react";
import { useStore } from "../../stores/store.context";
import { Image } from "semantic-ui-react";

const regex = /\{(.*?)\}/gm;
export const ColorImageComponent = ({ color }: { color: string }) => {
	const { rootStore } = useStore();
	const { cardsStore } = rootStore;
	const [colors, setColors] = useState([] as string[]);

	useEffect(() => {
		let m;
		const wColors: string[] = [];

		while ((m = regex.exec(color)) !== null) {
			if (m.index === regex.lastIndex) {
				regex.lastIndex++;
			}
			m.forEach((match, groupIndex) => {
				if (!match.startsWith("{")) {
					wColors.push(match);
				}
			});
			setColors(wColors);
		}
	}, []);

	return (
		<Image.Group>
			{colors.map((c) => {
				return (
					<Image
						size="mini"
						key={c}
						src={cardsStore.colors?.filter((k) => k.name! == c)[0].imageUrl}
					/>
				);
			})}
		</Image.Group>
	);
};
