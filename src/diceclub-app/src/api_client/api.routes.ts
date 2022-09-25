export const apiRoutes = {
  AUTH: {
    LOGIN: "api/v1/login/auth",
  },
  WEB_SOCKET: {
    NOTIFICATION: "/websocket/notifications",
  },
  CARDS: {
    SETS: "api/v1/cards/sets",
		COLORS: "api/v1/cards/colors",
		TYPES: "api/v1/cards/types",
		RARITIES: "api/v1/cards/rarities",
		LANGUAGES: "api/v1/cards/languages",
  },
  PAGINATION: {
    PAGE_SIZE: 50,
    buildPaginationQuery: (route: string, page: number, pageSize: number) => {
      return `${route}?page=${page}&pageSize=${pageSize}`;
    },
},
};
