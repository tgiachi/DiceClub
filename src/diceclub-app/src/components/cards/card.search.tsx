import React from "react";
import { useStore } from "../../stores/store.context";
import { Segment, Form } from "semantic-ui-react";
import { CardSearchTextbox } from "./search/search.textbox";
import { SetSelectComponent } from "./search/set.select";
import { RaritiesSelect } from "./search/rarities.select";
import { LanguageSelectComponent } from "./search/language.select";
import { CardSearchOrderBySelect } from "./search/orderby.select";
import { TypesSelectComponent } from "./search/types.select";
import { CardColorSelect } from "./search/colors.select";
import { SearchCardRequestOrderBy } from "../../schemas/dice-club";

export const CardSearch = () => {
	const { rootStore } = useStore();
	const cardStore = rootStore.cardsStore;
	return (
		<Segment>
			<Form
				onSubmit={async () => {
					await cardStore.searchOwnedCards();
				}}
			>
				<Form.Group widths="equal">
					<Form.Field>
						<CardSearchTextbox
							callback={(text) => {
								cardStore.buildSearchQuery({ description: text });
							}}
						/>
					</Form.Field>
				</Form.Group>
				<Form.Group widths="equal">
					<Form.Field>
						<SetSelectComponent
							multiselect
							callback={(s) => {
								cardStore.buildSearchQuery({ sets: s });
							}}
						/>
					</Form.Field>
					<Form.Field>
						<RaritiesSelect
							multiselect
							callback={(r) => {
								cardStore.buildSearchQuery({ rarities: r });
							}}
						/>
					</Form.Field>
					<Form.Field>
						<LanguageSelectComponent
							multiselect
							callback={(l) => {
                console.log(l);
								cardStore.buildSearchQuery({ languages: l });
							}}
						/>
					</Form.Field>
					<Form.Field>
						<CardColorSelect
							multiselect
							callback={(l) => {
								cardStore.buildSearchQuery({ colors: l });
							}}
						/>
					</Form.Field>
					<Form.Field>
						<TypesSelectComponent
							multiselect
							callback={(l) => {
								console.log(l);
								cardStore.buildSearchQuery({ types: l });
							}}
						/>
					</Form.Field>
				</Form.Group>

				<Form.Group widths="equal">
					<Form.Field>
						<CardSearchOrderBySelect
							callback={(a) => {
								cardStore.buildSearchQuery({
									orderBy: a as SearchCardRequestOrderBy,
								});
							}}
						/>
					</Form.Field>
					<Form.Button color="twitter" icon="search">
						Ricerca
					</Form.Button>
				</Form.Group>
			</Form>
		</Segment>
	);
};
