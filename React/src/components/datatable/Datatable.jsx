import "./datatable.scss";
import { DataGrid } from "@mui/x-data-grid";
import { userColumns, userRows } from "../../datatablesource";
import { Link } from "react-router-dom";
import { useState } from "react";
import { Button } from "@mui/material";

const Datatable = () => {
	const [data, setData] = useState(userRows);

	const handleDelete = (id) => {
		setData(data.filter((item) => item.id !== id));
	};

	const actionColumn = [
		{
			field: "action",
			headerName: "Action",
			width: 250,
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
							// onClick={() => handleDelete(params.row.id)}
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
	return (
		<div className="datatable">
			<div className="datatableTitle">
				{/* Add New Supervisor */}
				Supervisors
				{/* <Link to="/users/new" className="link">
					Add New
				</Link> */}
			</div>
			<DataGrid
				className="datagrid"
				rows={data}
				columns={userColumns.concat(actionColumn)}
				pageSize={9}
				rowsPerPageOptions={[9]}
				checkboxSelection
			/>
		</div>
	);
};

export default Datatable;
