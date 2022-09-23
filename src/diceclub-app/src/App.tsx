import "./App.css";
import "semantic-ui-css/semantic.min.css";
import { StoreContext, initialValues } from "./stores/store.context";
import { LoaderComponent } from "./components/loader";
import { BrowserRouter } from "react-router-dom";
import { Container } from "semantic-ui-react";
import { NotifierComponent } from "./components/notifier";

function App() {
	return (
		<StoreContext.Provider value={initialValues}>
			<LoaderComponent>
				<NotifierComponent />
				<BrowserRouter>
					<Container className="main-container"></Container>
				</BrowserRouter>
			</LoaderComponent>
		</StoreContext.Provider>
	);
}

export default App;
