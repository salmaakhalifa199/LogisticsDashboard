import { DataGrid } from "@mui/x-data-grid";
import { useState } from "react";
import { Link } from "react-router-dom";
import { productsColumns } from "../../datatablesource";
import { useEffect } from "react";
import { Button } from "@mui/material";

const ProductsData = () => {
	const [data, setData] = useState([]);

	const handleFetchData = () => {
		fetch("https://fakestoreapi.com/products")
			.then((res) => res.json())
			.then((data) => {
				setData(data);
			});
	};

	const handleDelete = (id) => {
		setData(data.filter((item) => item.id !== id));
	};

	const actionColumn = [
		{
			field: "action",
			headerName: "Action",
			width: 300,
			renderCell: (params) => {
				return (
					<div className="cellAction">
						<Button
							variant="outlined"
							color="error"
							size="small"
							onClick={() => handleDelete(params.row.id)}
						>
							Delete
						</Button>

						<Button
							variant="outlined"
							color="info"
							size="small"
							
						>
							Edit
						</Button>

						<Button
							variant="outlined"
							color="success"
							size="small"
							// onClick={() => handleDelete(params.row.id)}
						>
							View
						</Button>
					</div>
				);
			},
		},
	];

	useEffect(() => {
		handleFetchData();
	}, []);

	return (
		<div className="datatable">
			<div className="datatableTitle">
				{/* Add New Product */}
				Products
				<Link to="/products/new" className="link">
					Add New
				</Link>
			</div>
			<DataGrid
				className="datagrid"
				rows={data}
				columns={productsColumns.concat(actionColumn)}
				pageSize={10}
				rowsPerPageOptions={[10]}
				checkboxSelection
			/>
		</div>
	);
};
export default ProductsData;
