import { createContext, useContext } from "react";
import { RootStore } from "./rootStore";

interface IStoresContext {
	rootStore: RootStore;
}

const initialValues: IStoresContext = {
	rootStore: new RootStore(),
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
