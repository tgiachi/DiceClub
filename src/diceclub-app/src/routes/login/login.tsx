import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import { StoreContext, useStore } from "../../stores/store.context";
import "./login.css";

export const Login = observer(() => {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [logged, setLogged] = useState(false);
	const { loginStore } = useStore();

	useEffect(() => {
		setLogged(loginStore.isLogged);
	}, [loginStore.isLogged]);

	return (
		<>
			<Row>
				<Col>Logged: {logged}</Col>
				<Col>
					<input type="email" onChange={(e) => setEmail(e.target.value)} />
				</Col>
				<Col>
					<input type="password" onChange={(e) => setPassword(e.target.value)} />
				</Col>
				<Col>
					<Button
						onClick={() => {
							loginStore.login({ username: email, password: password });
						}}>
						Login
					</Button>
				</Col>
			</Row>
		</>
	);
});
