export const apiConfig = {
	timeout: 30000,
	baseURL: `${
		import.meta.env.BASE_API_URL || `${window.location.origin.toString()}`
	}`,
	defaultPageSize: 30,
};
