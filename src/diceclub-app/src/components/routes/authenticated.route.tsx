import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { Navigate, RouteProps, useLocation } from "react-router";
import { Route } from "react-router";
import { useStore } from "../../stores/store.context";
import { appRoutes } from "./routes";

export const AuthenticatedRoute = observer(({ children, path }: RouteProps) => {
	const location = useLocation();
	const { rootStore } = useStore();

	if (rootStore.apiClient.isAutheticated) {
		return children;
	} else {
		return <Navigate to={appRoutes.AUTH} state={{ from: location }} />;
	}
});
