export const apiConfig = {
	returnRejectedPromiseOnError: true,
	withCredentials: true,
	timeout: 30000,
	baseURL: `${import.meta.env.BASE_API_URL || "http://localhost:5143"}`,
	defaultPageSize: 30
};
