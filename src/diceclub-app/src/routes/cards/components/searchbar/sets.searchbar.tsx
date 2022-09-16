import { DropdownItem, Form } from "semantic-ui-react";
import { useStore } from "../../../../stores/store.context";
import { ICardColor } from "../../../../interfaces/cards/cardcolor.interfaces";

export const SetsDropDown = () => {
	const { rootStore } = useStore();

	return (
		<>
			<Form.Select
				label="Set"
				selection
				multiple
				search
				options={rootStore.cardsStore.cardSets.map((s) => {
					return {
						key: s.id,
						text: s.description,
						value: s.description,
						image: {
							avatar: true,
							src: s.image
						}
					} as ICardColor;
				})}
			></Form.Select>
		</>
	);
};
