import Datatable from "../../components/datatable/Datatable";
import Navbar from "../../components/navbar/Navbar";
import Sidebar from "../../components/sidebar/Sidebar";
import "../list/list.scss";
import ProductsData from "./ProductsData";

const ProductsList = () => {
	return (
		<div className="ProductsList list">
			<Sidebar />
			<div className="listContainer">
				<Navbar />
				<ProductsData />
			</div>
		</div>
	);
};

export default ProductsList;
