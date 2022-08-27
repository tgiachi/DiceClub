import { createContext } from "react";
import LoginStore from "./login.store";

interface IStoresContext {
	loginStore: LoginStore;
}

const initialValues: IStoresContext = {
	loginStore: new LoginStore()
} 

const StoreContext = createContext<IStoresContext>(initialValues);


export {StoreContext, initialValues };