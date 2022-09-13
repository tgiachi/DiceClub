import React from "react";

import { StoreContext, initialValues } from "./stores/store.context";
import { Login } from "./routes/login/login";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container } from "react-bootstrap";

const App = () => (
	<StoreContext.Provider value={initialValues}>
		<Container>
			<Login />
		</Container>
	</StoreContext.Provider>
);

export default App;
