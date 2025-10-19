import React from 'react'
import { IoClose } from 'react-icons/io5'

const DeleteProductModal = ({
    onClose,
    fetchData,
    productData,
    user,
}) => {
    console.log("DEL USER: ", user)
    console.log("AUTH:", productData)



    const handleDeleteUser = async () => {
        try {
            // Make a DELETE request to the API
            const response = await fetch(`https://localhost:7123/products/${productData.productId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${user.token}`,  
                },
            });
    
            if (response.ok) {
                await fetchData();
                onClose();  
                console.log("Deleted successfull")
            } else {
                console.error("Failed to delete product");
            }
        } catch (error) {
            console.error("Error occurred during deletion:", error);
        }
    };

  return (
    <div>
        <div className="fixed bg-slate-200 bg-opacity-75 h-full w-full top-0 left-0 right-0 bottom-0 flex justify-center items-center z-10">
        <div className="bg-white p-4 rounded w-full max-w-2xl overflow-hidden relative">
            <div className="flex justify-between items-center pb-3">
                <h2 className="font-bold text-2xl mx-auto">Are you sure you want to delete this product?</h2>
                
                <div
                    className="absolute right-4 w-fit ml-auto text-2xl hover:text-red-600 cursor-pointer"
                    onClick={onClose}
                >
                    <IoClose />
                </div>
            </div>

            <div className='flex flex-col justify-center items-center'>
                <p className='text-slate-700 font-semibold text-center text-2xl'>{productData?.name}</p>
                
                <p>Quantity: {productData?.quantity}</p>
                <p className='text-red-500'>Price: {productData?.price}</p>
                
            </div>


            {/**BUTTON AREA */}
            <div className='flex items-center justify-center gap-2 mt-10'>
                <button className='bg-red-500 text-white px-6 py-1 rounded-full hover:bg-red-700' onClick={handleDeleteUser}>Delete</button>
                <button className='bg-slate-200 text-black px-6 py-1 rounded-full hover:bg-slate-400' onClick={onClose}>Cancel</button>
            </div>

           
            
        </div>
    </div>
    </div>
  )
}

export default DeleteProductModal