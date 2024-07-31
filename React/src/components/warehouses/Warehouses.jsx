import { useEffect, useState } from "react";
import Navbar from "../navbar/Navbar";
import Sidebar from "../sidebar/Sidebar";
import { Button } from "@mui/material";

const Warehouses = () => {
	const [warehouses, setWarehouses] = useState([]);
	const fetchWarehouses = () => {
		fetch("https://fakestoreapi.com/products/categories")
			.then((res) => res.json())
			.then((data) => setWarehouses(data));
	};
	useEffect(() => {
		fetchWarehouses();
	}, []);
	return (
		<div className="Warehouses list">
			<Sidebar />
			<div className="listContainer">
				<Navbar />
				<div className="container">
					<div className="row">
						{warehouses
							? warehouses.map((item, index) => (
									<div className="col-12 col-md-6 p-3" key={index}>
										<Button fullWidth variant="contained" className="py-3">
											<span className="me-2 fw-bolder">{`Warehouse${
												index + 1
											}:`}</span>
											{`${item}`}
										</Button>
									</div>
							  ))
							: "loading"}
					</div>
				</div>
			</div>
		</div>
	);
};

export default Warehouses;
