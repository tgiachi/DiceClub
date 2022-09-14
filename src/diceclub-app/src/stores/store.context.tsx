import { createContext, useContext } from "react";
import { ApiClientStore } from "./api.store";
import ErrorStore from "./errors.store";
import LoginStore from "./login.store";
import { RootStore } from "./root.store";

interface IStoresContext {
	rootStore: RootStore;
}

const initialValues: IStoresContext = {
	rootStore: new RootStore()
};

const StoreContext = createContext<IStoresContext>(initialValues);

const useStore = () => {
	const store = useContext(StoreContext);
	if (!store) {
		throw new Error("useStore must be used within a StoreProvider");
	}
	return store;
};

export { StoreContext, initialValues, useStore };
