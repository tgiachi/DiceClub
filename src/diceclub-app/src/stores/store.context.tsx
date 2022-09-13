import { createContext, useContext } from "react";
import LoginStore from "./login.store";

interface IStoresContext {
	loginStore: LoginStore;
}

const initialValues: IStoresContext = {
	loginStore: new LoginStore()
};

const StoreContext = createContext<IStoresContext>(initialValues);

const useStore = () => {
	const store = useContext(StoreContext);
	if (!store) {
		throw new Error("useStore must be used within a StoreProvider");
	}
	return store;
}

export { StoreContext, initialValues, useStore };
