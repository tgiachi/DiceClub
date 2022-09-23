import React from "react";
import { useEffect } from "react";
import { Icon, Label } from "semantic-ui-react";
import { useStore } from "../../../../stores/store.context";

export const UserLabel = ({ id }: { id: string }) => {
	const { rootStore } = useStore();
	const [users, setUsers] = React.useState(rootStore.usersStore.getUsers);
	useEffect(() => {
		setUsers(rootStore.usersStore.getUsers);
	}, [rootStore.usersStore.getUsers]);

	return (
		<>
			<Label>
				<Icon name="user" />
				{users
					.filter((s) => s.id === id)
					.map((u) => {
						return ` ${u.nickName}`;
					})}
			</Label>
		</>
	);
};
