import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../stores/store.context";
import { Menu, Container, Dropdown, Image, Label } from "semantic-ui-react";
import { Link } from "react-router-dom";
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
						<Menu.Item as="a">
							<Link to="/">Home</Link>
						</Menu.Item>
						<Dropdown item simple text="Carte">
							<Dropdown.Menu>
								<Dropdown.Item as ="a">
									<Link  to={appRoutes.DECK.DECKS}>
										<span className="text"><Label>Mazzi</Label></span>
									</Link>
								</Dropdown.Item>
								<Dropdown.Item>
									<Link to={appRoutes.CARDS.OWNED_CARDS}>
										<span className="text">Carte</span>
									</Link>
								</Dropdown.Item>
								{/* <Dropdown.Divider />
								<Dropdown.Header>Header Item</Dropdown.Header>
								<Dropdown.Item>
									<i className="dropdown icon" />
									<span className="text">Submenu</span>
									<Dropdown.Menu>
										<Dropdown.Item>List Item</Dropdown.Item>
										<Dropdown.Item>List Item</Dropdown.Item>
									</Dropdown.Menu>
								</Dropdown.Item>
								<Dropdown.Item>List Item</Dropdown.Item> */}
							</Dropdown.Menu>
						</Dropdown>
					</Container>
				</Menu>
			</div>
		);
	}
});
