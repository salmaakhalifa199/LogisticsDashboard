import React from 'react'
import "./request.scss"
import Sidebar from '../components/sidebar/Sidebar'

function Requests() {

    const requests = [
        {
            supervisorName: "Salma",
            warehouseId: 2,
            category: "Electronics",
            situation: "decrease",
            productId: 1,
            productName: "Apple Macbook",
        },
        {
            supervisorName: "Sedra",
            warehouseId: 1,
            category: "Jewelery",
            situation: "increase",
            productId: 555,
            productName: "Diamond Rings",
        },
        {
            supervisorName: "Shehab",
            warehouseId: 2,
            category: "Electronics",
            situation: "decrease",
            productId: 12233,
            productName: "Iphone 15 Pro",
        }
    ]
  return (
    <div className='list'>

        <Sidebar />
        <div className="listContainer">
        <h2>Supervisors Requests</h2>

        <div className='requests'>
        {requests.map((req) => {
            return(
            <div className="request">
                <div className="reqInfo">
                    <p>Supervisor Name: {req.supervisorName}</p>
                    <p>
                        Request_Type: {req.situation}
                    </p>
                    <p>
                        Warehouse ID: {req.warehouseId}
                    </p>
                    <p>
                        Category: {req.category}
                    </p>
                    <p>
                        Product ID: {req.productId}
                    </p>
                    <p>
                       Product Name: {req.productName} 
                    </p>
                </div>

                <div className="adminSituation">
                    <button type="button" value="approved" className='approve'>Approved</button>
                    <button type="button" value="cancel" className='cancel'>Cancel</button>
                </div>
            </div>
            );
        })}
        </div>
        </div>
      
    </div>
  )
}

export default Requests
