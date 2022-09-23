import { makeAutoObservable } from "mobx";
import axios from "axios";
import { RootStore } from "./root.store";
import { apiConfig } from "../api_client/api.config";
import { AxiosInstance } from "axios";
export type RestMethodType = "get" | "post" | "patch" | "delete" | "put";
export const ApiRequestMethods: { [key in "GET" | "POST" | "PATCH" | "DELETE" | "PUT"]: RestMethodType } = {
	GET: "get",
	POST: "post",
	PATCH: "patch",
	DELETE: "delete",
	PUT: "put"
};
/**
 * Interface for make requests
 */
export interface ApiRequest<T = unknown> {
	url: string;
	method: RestMethodType;
	payload?: T;
	customHeaders?: Record<string, unknown>;
}

export interface ApiResponse<TData> {
	request: ApiRequest;
	statusCode: number;
	statusText: string;
	data?: TData;
	rawData?: any;
	isError: boolean;
	errorData: any;
}

class ApiClientStore {
	rootStore: RootStore;
	isAutheticated: boolean;
	token: string = "";
	api: AxiosInstance = axios.create({
		baseURL: apiConfig.baseURL
	});

	constructor(rootStore: RootStore) {
		this.isAutheticated = false;
		makeAutoObservable(this);
		this.rootStore = rootStore;
		this.checkAuthToken();
	}
	get getIsAuthenticated() {
		return this.isAutheticated;
	}

	checkAuthToken() {
		const auth = localStorage.getItem("auth");
		if (auth) {
			const { token, refreshToken, expire } = JSON.parse(auth);
			console.log("Check authentication");
			if (new Date(expire) > new Date()) {
				console.log("Token found");
				this.setAuthenticated(token, refreshToken, expire);
			}
		}
	}

	setAuthenticated(token: string, refreshToken: string, expire: Date) {
		this.isAutheticated = true;
		this.token = token;
		localStorage.setItem("auth", JSON.stringify({ token, refreshToken, expire }));
	}
	public async request<T>(request: ApiRequest): Promise<ApiResponse<T>> {
		this.rootStore.setIsLoading = true;
		request.url = `${apiConfig.baseURL}${request.url}`;
		const response: ApiResponse<T> = {
			isError: false,
			statusCode: 200,
			statusText: "",
			request,
			errorData: null
		};

		let headers = {};

		headers = request.customHeaders
			? { "Content-Type": "application/json", ...request.customHeaders }
			: { "Content-Type": "application/json" };

		if (this.getIsAuthenticated) {
			headers = { ...headers, Authorization: `Bearer ${this.token}` };
		}

		try {
			let axiosResponse;

			switch (request.method) {
				case ApiRequestMethods.GET:
					axiosResponse = await axios.get(request.url, { headers });
					break;
				case ApiRequestMethods.POST:
					axiosResponse = await axios.post(request.url, request.payload, { headers });
					break;
				case ApiRequestMethods.PATCH:
					axiosResponse = await axios.patch(request.url, request.payload, { headers });
					break;
				case ApiRequestMethods.PUT:
					axiosResponse = await axios.put(request.url, request.payload, { headers });
					break;
				case ApiRequestMethods.DELETE:
					axiosResponse = await axios.delete(request.url, { headers });
					break;
			}
			this.rootStore.setIsLoading = false;
			if (axiosResponse) {
				response.statusCode = axiosResponse.status;
				response.statusText = axiosResponse.statusText;
				response.data = axiosResponse.data;
				response.rawData = axiosResponse.data;
				return response;
			}
			throw new Error("No response from server");
		} catch (err) {
			response.isError = true;
			response.errorData = err;
			this.rootStore.setIsLoading = false;
			return response;
		}
	}
}

export { ApiClientStore };
