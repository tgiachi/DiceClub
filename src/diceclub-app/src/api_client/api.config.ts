let apiUrl = "http://127.0.0.1:5280/";
if (import.meta.env.DEV == false) {
	apiUrl = window.location.origin.toString();
}

export const apiConfig = {
	timeout: 30000,
	baseURL: apiUrl,
	defaultPageSize: 30,
};
