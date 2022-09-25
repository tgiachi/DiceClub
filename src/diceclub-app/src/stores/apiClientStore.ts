import { RootStore } from "./rootStore";
import { action, computed, makeAutoObservable, observable } from "mobx";
import axios, { AxiosInstance } from "axios";
import { apiConfig } from "../api_client/api.config";
import { apiRoutes } from "../api_client/api.routes";
import { IAuthToken } from "../interfaces/authtoken";
import {
  LoginRequestDataRestResultObject,
  LoginResponseDataRestResultObject,
} from "../schemas/dice-club";

export type apiClientMethods = "get" | "post" | "put" | "delete" | "patch";

export class ApiClientStore {
  rootStore: RootStore;

  @observable isLoading = false;
  @observable isAutheticated = false;
  @observable accessToken?: IAuthToken;

  constructor(rootStore: RootStore) {
    makeAutoObservable(this);
    this.rootStore = rootStore;
  }

  @action setIsLoading = (isLoading: boolean) => {
    this.isLoading = isLoading;
  };
  @action setAccessToken = (accessToken: IAuthToken) => {
    this.accessToken = accessToken;
  };

  @computed getAuthToken() {
    return this.accessToken;
  }

  async init() {
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
      console.log("access token found");
      this.setAccessToken(JSON.parse(accessToken) as IAuthToken);
      this.isAutheticated = true;
    } else {
      console.log("access token not found");
      await this.login("squid@stormwind.it", "tommaso");
    }
  }

  public async get<TData>(path: string) {
    return this.request<TData>(path, "get");
  }
  public async getPaginated<TData>(path: string, page: number) {
    const result = await this.get<TData>(
      apiRoutes.PAGINATION.buildPaginationQuery(
        path,
        page,
        apiRoutes.PAGINATION.PAGE_SIZE
      )
    );
    return result;
  }

  public async post<TData>(path: string, body: any) {
    return this.request<TData>(path, "post", body);
  }
  public async put<TData>(path: string, body: any) {
    return this.request<TData>(path, "put", body);
  }

  public async delete<TData>(path: string) {
    return this.request<TData>(path, "delete");
  }

  public async patch<TData>(path: string) {
    return this.request<TData>(path, "patch");
  }

  public async request<TData>(
    path: string,
    method: apiClientMethods,
    data?: any
  ) {
    this.setIsLoading(true);
    let headers = {};
    if (this.accessToken) {
      headers = {
        Authorization: `Bearer ${this.accessToken.accessToken}`,
      };
      headers = {
        ...headers,
        ...{
          "Content-Type": "application/json",
        },
      };
    }
    try {
      const result = await axios.request({
        baseURL: apiConfig.baseURL,
        method,
        url: path,
        data,
        headers,
        timeout: 10000,
      });
      this.setIsLoading(false);
      return result.data as TData;
    } catch (error) {
      this.setIsLoading(false);
      throw error;
    }
  }

  @action
  public async login(email: string, password: string) {
    try {
      const result = await this.post<LoginResponseDataRestResultObject>(
        apiRoutes.AUTH.LOGIN,
        {
          email,
          password,
        }
      );
      this.accessToken = {
        accessToken: result.result?.accessToken!,
        refreshToken: result.result?.refreshToken!,
        accessTokenExpire: new Date(
          Date.parse(result.result?.accessTokenExpire!)
        ),
      };
      this.isAutheticated = true;
      localStorage.setItem("accessToken", JSON.stringify(this.accessToken));
    } catch (ex) {
      console.log(ex);
      this.rootStore.notificationsStore.addNotification({
        category: "error",
        message: ex as string,
        title: "Error during login",
        type: "message",
      });
    }
  }
}
