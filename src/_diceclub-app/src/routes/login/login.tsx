import { observer } from "mobx-react-lite";
import React, { useCallback, useEffect, useState } from "react";

import { StoreContext, useStore } from "../../stores/store.context";
import { Button, Form, Grid, Header, Image, Message, Segment } from "semantic-ui-react";
import { ErrorsBar } from "../../components/errors_bar.component";
import { Navigate, useLocation } from "react-router-dom";
import Particles from "react-particles";
import { loadFull } from "tsparticles";

export const Login = observer(() => {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [logged, setLogged] = useState(false);
	const { rootStore } = useStore();
	const particlesInit = useCallback(async (engine) => {
		// you can initiate the tsParticles instance (engine) here, adding custom shapes or presets
		// this loads the tsparticles package bundle, it's the easiest method for getting everything ready
		// starting from v2 you can add only the features you need reducing the bundle size
		await loadFull(engine);
	}, []);

	const particlesLoaded = useCallback(async (container) => {}, []);

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
			<Particles
				id="tsparticles"
				init={particlesInit}
				loaded={particlesLoaded}
				options={{
					background: {
						color: {
							value: "#000000"
						}
					},
					fpsLimit: 120,
					interactivity: {
						events: {
							onClick: {
								enable: true,
								mode: "push"
							},
							onHover: {
								enable: true,
								mode: "repulse"
							},
							resize: true
						},
						modes: {
							push: {
								quantity: 4
							},
							repulse: {
								distance: 50,
								duration: 0.4
							}
						}
					},
					particles: {
						color: {
							value: "#ffffff"
						},
						links: {
							color: "#ffffff",
							distance: 150,
							enable: true,
							opacity: 0.5,
							width: 1
						},
						collisions: {
							enable: true
						},
						move: {
							directions: "none",
							enable: true,
							outModes: {
								default: "bounce"
							},
							random: true,
							speed: 3,
							straight: false
						},
						number: {
							density: {
								enable: true,
								area: 800
							},
							value: 80
						},
						opacity: {
							value: 0.5
						},
						shape: {
							type: "circle"
						},
						size: {
							value: { min: 1, max: 5 }
						}
					},
					detectRetina: true
				}}
			/>
			<ErrorsBar />
			<Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
				<Grid.Column style={{ maxWidth: 450 }}>
					<Segment inverted>
						<Header as="h2" color="teal" textAlign="center" >
							<Image src="/logo.png" /> Log-in to your account
						</Header>
					</Segment>
					<Form size="large">
						<Segment stacked inverted>
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
					<Message info>
						New to us? <a href="#">Sign Up</a>
					</Message>
				</Grid.Column>
			</Grid>
		</>
	);
});
