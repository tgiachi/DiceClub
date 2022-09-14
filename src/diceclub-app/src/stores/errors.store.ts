import { makeAutoObservable } from "mobx";
import { errorType, IErrorMessage } from "../interfaces/errors/error.interfaces";
import { RootStore } from "./root.store";

class ErrorStore {
	errors: IErrorMessage[] = [];
	rootStore: RootStore;
	constructor(rootStore: RootStore) {
		makeAutoObservable(this);
		this.rootStore = rootStore;
	}

	get getErrors() {
		return this.errors;
	}

	addError(text: string, type: errorType) {
		this.errors.push({
			id: Math.random().toString(36).substr(2, 9),
			type,
			message: text
		});
	}
}

export default ErrorStore;
