import React from "react";
import reactStringReplace from "react-string-replace";
import { useStore } from "../../stores/store.context";
import { Image } from "semantic-ui-react";

interface ICardDescriptionSymbol {
	description: string;
}

export const CardDescriptionSymbol: React.FC<ICardDescriptionSymbol> = (
	props
) => {
	const { rootStore } = useStore();

	const result = reactStringReplace(
		props.description,
		/\{(.*?)\}/gm,
		(match, i) => {
			const symbol = rootStore.cardsStore.symbols?.filter(
				(k) => k.symbol! == `{${match}}`
			)[0];
			return (
				<Image
					style={{
						width: "1.0em",
						height: "1.0em",
					}}
					key={i}
					src={symbol?.image!}
				/>
			);
		}
	);

	return <>{result}</>;
};
