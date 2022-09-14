import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";

import { StoreContext, useStore } from "../../stores/store.context";
import { Button, Form, Grid, Header, Image, Message, Segment } from "semantic-ui-react";
import { ErrorsBar } from "../../components/errors_bar.component";
import { Navigate, useLocation } from "react-router-dom";

export const Login = observer(() => {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [logged, setLogged] = useState(false);
	const { rootStore } = useStore();

	const location = useLocation();
	useEffect(() => {
		setLogged(rootStore.apiStore.isAutheticated);
		if (rootStore.apiStore.isAutheticated) {
			location.pathname = "/dashboard";
		}
	}, [rootStore.apiStore.isAutheticated]);

	if (logged) {
		return <Navigate to="/dashboard" />;
	}

	return (
		<>
			<ErrorsBar />
			<Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
				<Grid.Column style={{ maxWidth: 450 }}>
					<Header as="h2" color="teal" textAlign="center">
						<Image src="/logo.png" /> Log-in to your account
					</Header>
					<Form size="large">
						<Segment stacked>
							<Form.Input
								fluid
								icon="user"
								onChange={(e) => {
									setEmail(e.target.value);
								}}
								iconPosition="left"
								placeholder="E-mail address"
							/>
							<Form.Input
								fluid
								icon="lock"
								onChange={(e) => {
									setPassword(e.target.value);
								}}
								iconPosition="left"
								placeholder="Password"
								type="password"
							/>

							<Button
								onClick={() => rootStore.loginStore.login({ username: email, password })}
								color="teal"
								fluid
								size="large"
							>
								Login
							</Button>
						</Segment>
					</Form>
					<Message>
						New to us? <a href="#">Sign Up</a>
					</Message>
				</Grid.Column>
			</Grid>
		</>
	);
});
