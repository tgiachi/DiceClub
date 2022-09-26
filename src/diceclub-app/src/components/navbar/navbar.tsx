import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { useStore } from "../../stores/store.context";
import { Menu, Container, Dropdown, Image } from "semantic-ui-react";

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
						<Menu.Item as="a">Home</Menu.Item>
						<Dropdown item simple text="Dropdown">
							<Dropdown.Menu>
								<Dropdown.Item>List Item</Dropdown.Item>
								<Dropdown.Item>List Item</Dropdown.Item>
								<Dropdown.Divider />
								<Dropdown.Header>Header Item</Dropdown.Header>
								<Dropdown.Item>
									<i className="dropdown icon" />
									<span className="text">Submenu</span>
									<Dropdown.Menu>
										<Dropdown.Item>List Item</Dropdown.Item>
										<Dropdown.Item>List Item</Dropdown.Item>
									</Dropdown.Menu>
								</Dropdown.Item>
								<Dropdown.Item>List Item</Dropdown.Item>
							</Dropdown.Menu>
						</Dropdown>
					</Container>
				</Menu>
			</div>
		);
	}
});
