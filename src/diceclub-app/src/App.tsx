import React from "react";

import { StoreContext, initialValues } from "./stores/store.context";
import { Login } from "./routes/login/login";
import "bootstrap/dist/css/bootstrap.min.css";
import "semantic-ui-css/semantic.min.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Navbar } from "./components/navbar.component";
import { Dashboard } from "./routes/dashboard/dashboards";
import { AuthenticatedRoute } from "./helpers/authenticated.route";
import { CardContainer } from "./routes/cards/cards";

const App = () => (
	<StoreContext.Provider value={initialValues}>
		<Navbar />
		<BrowserRouter>
			<Routes>
				<Route path="/" element={<Login />} />
				<Route path="/login" element={<Login />} />
				<Route path="/dashboard" element={<Dashboard />} />
				<Route path="/dashboard/cards" element={<CardContainer />} />
			</Routes>
		</BrowserRouter>
	</StoreContext.Provider>
);

export default App;
