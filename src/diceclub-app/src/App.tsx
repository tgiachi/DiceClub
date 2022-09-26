import "semantic-ui-css/semantic.min.css";
import { StoreContext, initialValues } from "./stores/store.context";
import { LoaderComponent } from "./components/loader";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Container, Grid } from "semantic-ui-react";
import { NotifierComponent } from "./components/notifier";
import { LoginPage } from "./components/routes/login/login.page";
import { DashboardPage } from "./components/routes/dashboard/dashboard.page";
import { AuthenticatedRoute } from "./components/routes/authenticated.route";
import { appRoutes } from "./components/routes/routes";
import { NavBarComponent } from "./components/navbar/navbar";
import { OwnedCardSearchPage } from "./components/routes/cards/owned/ownedcards.page";
import "./App.css";

function App() {
	return (
		<StoreContext.Provider value={initialValues}>
			<LoaderComponent>
				<NotifierComponent />
				<NavBarComponent />
				<BrowserRouter>
					<Grid container >
						<Grid.Row>
							<Grid.Column>
								<Container className="main-container">
									<Routes>
										<Route
											path={appRoutes.HOME}
											element={
												<AuthenticatedRoute>
													<DashboardPage />
												</AuthenticatedRoute>
											}
										/>
										<Route path={appRoutes.AUTH} element={<LoginPage />} />
										<Route
											path={appRoutes.DASHBOARD}
											element={
												<AuthenticatedRoute>
													<DashboardPage />
												</AuthenticatedRoute>
											}
										/>
										<Route
											path={appRoutes.CARDS.OWNED_CARDS}
											element={<OwnedCardSearchPage />}
										/>
									</Routes>
								</Container>
							</Grid.Column>
						</Grid.Row>
					</Grid>
				</BrowserRouter>
			</LoaderComponent>
		</StoreContext.Provider>
	);
}

export default App;
