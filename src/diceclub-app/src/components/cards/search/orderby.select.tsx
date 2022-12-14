import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import { Select } from "semantic-ui-react";
import { t } from "i18next";
export const CardSearchOrderBySelect = observer(
	({
		multiselect = false,
		callback,
	}: {
		multiselect?: boolean;
		callback: (values: string) => void;
	}) => {
		const { rootStore } = useStore();
		const [orderBy, setOrderBy] = React.useState([] as string[]);
		useEffect(() => {
			setOrderBy(rootStore.cardsStore.searchOrderBy!);
		}, [rootStore.cardsStore.searchOrderBy]);

		return (
			<Select
      placeholder={t("cards.select_order_by.title")}
				onChange={(e, data) => {
					callback(data.value as string);
				}}
				fluid
				options={orderBy.map((s) => {
					return { key: s!, value: s!, text: s! };
				})}
			></Select>
		);
	}
);
