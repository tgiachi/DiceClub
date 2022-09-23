export interface IBasePreloadStore {
  onAuthenticated(auth: string) : Promise<void>;
}