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
import { useTranslation } from "react-i18next";

export const LoginPage = observer(() => {
	const [logged, setLogged] = useState(false);
	const [rememberMe, setRememberMe] = useState(false);
	const [loginData, setLoginData] = useState({
		email: localStorage.getItem("email")!,
	} as { email?: string; password?: string });
	const { rootStore } = useStore();
	const location = useLocation();
	const { t } = useTranslation();
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
							<Image size="mini" src="/logo.png" /> {t("login.title")}
						</Header>
					</Segment>
					<Form
						size="large"
						onSubmit={async (f) => {
							if (rememberMe) {
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
