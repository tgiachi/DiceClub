import { observer } from "mobx-react-lite";
import React, { FC, useEffect } from "react";
import { Container, Dropdown, Image, Menu } from "semantic-ui-react";
import { useStore } from "../stores/store.context";

export const Navbar: FC = observer(() => {
	const { rootStore } = useStore();
	const [isAuthenticated, setIsAuthenticated] = React.useState(rootStore.apiStore.getIsAuthenticated);

	useEffect(() => {
		setIsAuthenticated(rootStore.apiStore.getIsAuthenticated);
	}, [rootStore.apiStore.getIsAuthenticated]);

	if (!isAuthenticated) {
		return <></>;
	} else {
		return (
			<div>
				<Menu fixed="top" inverted>
					<Container>
						<Menu.Item as="a" header>
							<Image size="mini" src="/logo.png" style={{ marginRight: "1.5em" }} />
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
