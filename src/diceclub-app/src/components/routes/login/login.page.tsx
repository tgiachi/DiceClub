import React, { useEffect, useState } from "react";

import { observer } from "mobx-react";
import { useStore } from "../../../stores/store.context";
import {
	Grid,
	Segment,
	Header,
	Form,
	Button,
	Message,
	Image,
} from "semantic-ui-react";
import { Navigate, useLocation } from "react-router-dom";

export const LoginPage = observer(() => {
	const [logged, setLogged] = useState(false);
	const [rememberMe, setRememberMe] = useState(false);
	const [loginData, setLoginData] = useState({
		email: localStorage.getItem("email")!,
	} as { email?: string; password?: string });
	const { rootStore } = useStore();
	const location = useLocation();

	useEffect(() => {
		setLogged(rootStore.apiClient.isAutheticated);
		if (rootStore.apiClient.isAutheticated) {
			location.pathname = "/dashboard";
		}
	}, [rootStore.apiClient.isAutheticated]);

	if (logged) {
		return <Navigate to="/dashboard" />;
	}

	return (
		<>
			<Grid
				textAlign="center"
				style={{ height: "100vh" }}
				verticalAlign="middle"
			>
				<Grid.Column style={{ maxWidth: 450 }}>
					<Segment inverted>
						<Header as="h2" color="teal" textAlign="center">
							<Image src="/logo.png" /> Log-in to your account
						</Header>
					</Segment>
					<Form
						size="large"
						onSubmit={async (f) => {
							if (rememberMe) {
								console.log("setting email");
								localStorage.setItem("email", loginData.email!);
							}
							const result = await rootStore.apiClient.login(
								loginData.email!,
								loginData.password!
							);
						}}
					>
						<Segment stacked inverted>
							<Form.Input
								required
								fluid
								icon="user"
								onChange={(e) => {
									setLoginData({ ...loginData, email: e.target.value });
								}}
								iconPosition="left"
								placeholder="E-mail address"
							/>
							<Form.Input
								required
								fluid
								icon="lock"
								onChange={(e) => {
									setLoginData({ ...loginData, password: e.target.value });
								}}
								iconPosition="left"
								placeholder="Password"
								type="password"
							/>
							<Form.Checkbox
								onChange={(k, data) => {
									console.log(data.checked);
									setRememberMe(data.checked!);
								}}
								label="Remember me"
							/>

							<Button color="green" fluid size="large">
								Login
							</Button>
						</Segment>
					</Form>
					<Message info>
						New to us? <a href="#">Sign Up</a>
					</Message>
				</Grid.Column>
			</Grid>
		</>
	);
});
