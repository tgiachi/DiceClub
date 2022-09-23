export type errorType = "error" | "warning" | "info" | "success";
export interface IErrorMessage {
  id: string;
	message: string;
	type: errorType;
}
