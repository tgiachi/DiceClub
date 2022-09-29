import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../stores/store.context";
import { Menu, Container, Dropdown, Image, Label } from "semantic-ui-react";
import { Link, NavLink } from "react-router-dom";
import { apiRoutes } from "../../api_client/api.routes";
import { appRoutes } from "../routes/routes";

export const NavBarComponent = observer(() => {
	const { rootStore } = useStore();
	const [isAuthenticated, setIsAuthenticated] = React.useState(
		rootStore.apiClient.isAutheticated
	);

	useEffect(() => {
		setIsAuthenticated(rootStore.apiClient.isAutheticated);
	}, [rootStore.apiClient.isAutheticated]);

	if (!isAuthenticated) {
		return <></>;
	} else {
		return (
			<div>
				<Menu fixed="top" inverted>
					<Container>
						<Menu.Item as="a" header>
							<Image
								size="mini"
								src="/logo.png"
								style={{ marginRight: "1.5em" }}
							/>
							Club del dado
						</Menu.Item>
						<Menu.Item as={NavLink} to="/">
							Home
						</Menu.Item>
						<Dropdown item simple text="Carte">
							<Dropdown.Menu>
								<Dropdown.Item as={NavLink} to={appRoutes.DECK.DECKS}>
									Mazzi
								</Dropdown.Item>
								<Dropdown.Item as={NavLink} to={appRoutes.CARDS.OWNED_CARDS}>
									Carte possedute
								</Dropdown.Item>
							</Dropdown.Menu>
						</Dropdown>
					</Container>
				</Menu>
			</div>
		);
	}
});
