import React, { useEffect } from "react";
import {
	Dimmer,
	Loader,
	Image,
	Container,
	Header,
	Grid,
} from "semantic-ui-react";
import { observer } from "mobx-react";
import { useStore } from "../stores/store.context";
import { useTranslation } from "react-i18next";

export const LoaderComponent = observer(({ children }: { children: any }) => {
	const { t } = useTranslation();
	const [isLoading, setIsLoading] = React.useState(false);
	const { rootStore } = useStore();
	useEffect(() => {
		setIsLoading(rootStore.apiClient.isLoading);
	}, [rootStore.apiClient.isLoading]);

	return (
		<>
			<Dimmer active={isLoading}>
				<Grid>
					<Grid.Row>
						<Loader />
					</Grid.Row>
					<Grid.Row>
						<Header as="h5" icon inverted>
							{t("loading")}
						</Header>
					</Grid.Row>
				</Grid>
			</Dimmer>
			{children}
		</>
	);
});
