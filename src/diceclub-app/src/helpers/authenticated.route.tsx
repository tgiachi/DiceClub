import React, { useEffect } from "react";
import { observer } from "mobx-react";
import { Navigate, RouteProps, useLocation } from "react-router";
import { Route } from "react-router";
import { useStore } from "../stores/store.context";

export const AuthenticatedRoute = observer(({ children, path }: RouteProps) => {
	const location = useLocation();
	const { rootStore } = useStore();

	// useEffect(() => {

	// }, [rootStore.apiStore.isAutheticated]);

	if (rootStore.apiStore.isAutheticated) {
		return <Route path={path} element={children} />;
	} else {
		return <Navigate to="/login" state={{ from: location }} />;
	}
});
