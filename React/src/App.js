import Home from "./pages/home/Home";
import Login from "./pages/login/Login";
import Register from "./pages/register/Register";
import List from "./pages/list/List";
import Single from "./pages/single/Single";
import New from "./pages/new/New";
import Requests from "./pages/Requests";
import React, { useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { productInputs, userInputs } from "./formSource";
import "./style/dark.scss";
import { useContext } from "react";
import { DarkModeContext } from "./context/darkModeContext";
import ProductsList from "./pages/products/ProductsList";
import Warehouses from "./components/warehouses/Warehouses";
function App() {
	const { darkMode } = useContext(DarkModeContext);
	const [user, setUser] = useState(null);

	return (
		<div className={darkMode ? "app dark" : "app"}>
			<BrowserRouter>
				<Routes>
					<Route path="/">
						<Route index element={<Home />} />
						<Route path="login" element={<Login />} />
						<Route path="register" element={<Register/>} />
						<Route path="users">
							<Route index element={<List />} />
							<Route path=":userId" element={<Single />} />
							<Route
								path="new"
								element={<New inputs={userInputs} title="Add New User" />}
							/>
						</Route>
						<Route path="products">
							<Route index element={<ProductsList />} />
							<Route
								path="new"
								element={<New inputs={productInputs} title="Add New Product" />}
							/>
						</Route>
						
						<Route path="warehouses">
							<Route index element={<Warehouses />} />
						</Route>
						
						<Route path="requests">
							<Route index element={<Requests />} />
						</Route>
						
					</Route>
				</Routes>
			</BrowserRouter>
		</div>
	);
}

export default App;
