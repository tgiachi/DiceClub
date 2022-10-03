export const apiConfig = {
	timeout: 30000,
	baseURL: `${import.meta.env.BASE_API_URL || "http://localhost:5280"}`,
	defaultPageSize: 30
};
