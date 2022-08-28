import { observer } from "mobx-react-lite";
import React from "react";
import "./login.css";
import { WindowHeader, Button, Toolbar, WindowContent, Panel, Window } from "react95";

export const Login = observer(() => {
	return (
		<>
			<Window resizable className="window">
				<WindowHeader className="window-header">
					<span>react95.exe</span>
					<Button>
						<span className="close-icon" />
					</Button>
				</WindowHeader>
				<Toolbar>
					<Button variant="menu" size="sm">
						File
					</Button>
					<Button variant="menu" size="sm">
						Edit
					</Button>
					<Button variant="menu" size="sm" disabled>
						Save
					</Button>
				</Toolbar>
				<WindowContent>
					<p>
						When you set &quot;resizable&quot; prop, there will be drag handle in the bottom right corner (but resizing
						itself must be handled by you tho!)
					</p>
				</WindowContent>
				<Panel variant="well" className="footer">
					Put some useful informations here
				</Panel>
			</Window>
		</>
	);
});
