import React, { useEffect, useState } from "react";
import { useStore } from "../../stores/store.context";
import { Image } from "semantic-ui-react";
import { v4 } from "uuid";

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
				if (match.startsWith("{")) {
					wColors.push(match);
				}
			});
      
			setColors(wColors);
		}
	}, []);

	return (
		<Image.Group size="mini">
			{colors.map((c) => {
        const symbol = cardsStore.symbols?.filter((k) => k.symbol! == c)[0];
				return (
					<Image
						key={symbol.id! + symbol.description + v4() }
						src={symbol?.image!}
					/>
				);
			})}
		</Image.Group>
	);
};
