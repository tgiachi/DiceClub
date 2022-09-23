import React, { useEffect } from "react";
import { Dimmer, Loader, Image, Container } from "semantic-ui-react";
import { observer } from "mobx-react";
import { useStore } from "../stores/store.context";

export const LoaderComponent = observer(({ children }: { children: any }) => {
	const [isLoading, setIsLoading] = React.useState(false);
	const { rootStore } = useStore();
  useEffect(() => {
    setIsLoading(rootStore.getIsLoading);
  }, [rootStore.getIsLoading]);

	return (
		<Container>
			<Dimmer active={isLoading}>
				<Loader />
			</Dimmer>
			{children}
		</Container>
	);
});
