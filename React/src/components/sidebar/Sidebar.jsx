import "./sidebar.scss";
import DashboardIcon from "@mui/icons-material/Dashboard";
import PersonOutlineIcon from "@mui/icons-material/PersonOutline";
import LocalShippingIcon from "@mui/icons-material/LocalShipping";
import CreditCardIcon from "@mui/icons-material/CreditCard";
import StoreIcon from "@mui/icons-material/Store";
import InsertChartIcon from "@mui/icons-material/InsertChart";
import SettingsApplicationsIcon from "@mui/icons-material/SettingsApplications";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import NotificationsNoneIcon from "@mui/icons-material/NotificationsNone";
import SettingsSystemDaydreamOutlinedIcon from "@mui/icons-material/SettingsSystemDaydreamOutlined";
import PsychologyOutlinedIcon from "@mui/icons-material/PsychologyOutlined";
import AccountCircleOutlinedIcon from "@mui/icons-material/AccountCircleOutlined";
import { Link } from "react-router-dom";
import { DarkModeContext } from "../../context/darkModeContext";
import { useContext, useEffect, useState } from "react";
import SendIcon from "@mui/icons-material/Send";
import HistoryIcon from "@mui/icons-material/History";

const Sidebar = () => {
	const { dispatch } = useContext(DarkModeContext);
	const [actor, setActor] = useState("super"); // change actor

	const logout = ()=>{
		localStorage.removeItem("JWT");
		window.location.href="/login";
	}
	const adminFeatures = [
		{
			name: "Supervisors",
			url: "/users",
			icon: <PersonOutlineIcon className="icon" />,
		},
		{
			name: "Products",
			url: "/products",
			icon: <StoreIcon className="icon" />,
		},
		{
			name: "Warehouses",
			url: "/warehouses",
			icon: <CreditCardIcon className="icon" />,
		},
	];

	const supervisorFeatures = [
		{
			name: "Send Request",
			url: "/sendRequest",
			icon: <SendIcon className="icon" />,
		},
		{
			name: "Products",
			url: "/products",
			icon: <StoreIcon className="icon" />,
		},
		{
			name: "History",
			url: "/history",
			icon: <HistoryIcon className="icon" />,
		},
	];

	const handleActorChange = () => {
		// change actor logic
	};

	const [features, setFeatures] = useState(adminFeatures);
	useEffect(() => {
		if (actor === "admin") {
			setFeatures(adminFeatures);
		} else {
			setFeatures(supervisorFeatures);
		}
	}, [actor]);
	return (
		<div className="sidebar">
			<div className="top">
				<Link to="/" style={{ textDecoration: "none" }}>
					<span className="logo">Logistics Dashboard ({actor})</span>
				</Link>
			</div>
			<div className="center">
				<ul>
					<p className="title">MAIN</p>
					<li>
						<DashboardIcon className="icon" />
						<span>Dashboard</span>
					</li>
					<p className="title">LISTS</p>
					{features.map((f) => {
						return (
							<Link key={f.name} to={f.url} style={{ textDecoration: "none" }}>
								<li>
									{f.icon}
									<span>{f.name}</span>
								</li>
							</Link>
						);
					})}
					{actor === "admin" && (
						<>
							<p className="title">Management</p>
							<li>
								<InsertChartIcon className="icon" />
								<span>Manage Supervisors</span>
							</li>
							<li>
								<NotificationsNoneIcon className="icon" />
								<span>Manage warehouses</span>
							</li>
							<li>
								<Link key="requests" to="/requests" style={{ textDecoration: "none" }}>
									<NotificationsNoneIcon className="icon" />
									<span>Requests History</span>
								</Link>	
							</li>
						</>
					)}
					<p className="title">USER</p>
					<li onClick={logout}>
						<ExitToAppIcon className="icon" />
						<span>Logout</span>
					</li>
				</ul>
			</div>
			<div className="bottom">
				<div
					className="colorOption"
					onClick={() => dispatch({ type: "LIGHT" })}
				></div>
				<div
					className="colorOption"
					onClick={() => dispatch({ type: "DARK" })}
				></div>
			</div>
		</div>
	);
};

export default Sidebar;
