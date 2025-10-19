import React, { useState, useEffect } from 'react';
import AdminPanelProductCard from '../components/AdminPanelProductCard';
import UploadProductForm from '../components/UploadProductForm';

const AllProducts = () => {
    const [openUploadProduct, setOpenUploadProduct] = useState(false);
    const [allProducts, setAllProducts] = useState([]);
    const [pages, setPages] = useState(1);
    const [currentPage, setCurrentPage] = useState(1);

    const fetchAllProducts = async (page = 1) => {
        try {
            const response = await fetch(`https://localhost:7123/products?pageNumber=${page}`, {
                method: 'GET', 
                headers: {
                    'Content-Type': 'application/json',
                }
            });
    
            const responseData = await response.json();
    
            console.log("RESPONSEE: ", responseData);
    
            setAllProducts(responseData?.items);
            const numOfPages = Math.ceil(responseData.totalItemsCount / 10); // Ažuriraj broj stranica
            setPages(numOfPages);
    
            console.log("DATA: ", allProducts);
    
          } catch (error) {
            console.error("Error occurred while fetching orders:", error);
          }
    };

    const handleNextPage = () => {
        if (currentPage < pages) {
            setCurrentPage(prev => prev + 1);
        }
    }

    const handlePreviousPage = () => {
        if (currentPage > 1) {
            setCurrentPage(prev => prev - 1);
        }
    }

    useEffect(() => {
        fetchAllProducts(currentPage); // Učitaj proizvode na osnovu trenutne stranice
    }, [currentPage]); // Prikazuj nove proizvode kada se promeni trenutna stranica

    return (
        <div>
            <div className="bg-white shadow-md py-2 px-4 flex justify-between items-center">
                <h2 className="font-bold text-lg">All Products</h2>
                <button
                    className="border-2 border-orange-300 text-orange-300 hover:bg-orange-300 hover:text-white transition-all py-1 px-3 rounded-full"
                    onClick={() => setOpenUploadProduct(true)}
                >
                    Upload Product
                </button>
            </div>

            <div className='flex justify-center gap-5 mt-10'>
                <button onClick={handlePreviousPage} className='px-4 py-1 bg-orange-300 text-white w-[100px] rounded-full hover:bg-orange-500 transition-all'>Previous</button>
                <p>{currentPage} of {pages}</p>
                <button onClick={handleNextPage} className='px-4 py-1 w-[100px] bg-orange-300 text-white rounded-full hover:bg-orange-500 transition-all'>Next</button>
            </div>

            {/* Lista svih proizvoda */}
            <div className="flex items-center w-full flex-wrap gap-5 py-4 h-[calc(100vh-190px)] overflow-y-scroll justify-center">
                {allProducts.map((el, i) => (
                    <AdminPanelProductCard
                        data={el}
                        key={i + "AllProducts"}
                        fetchData={fetchAllProducts}
                    />
                ))}
            </div>

            {openUploadProduct && (
                <UploadProductForm
                    onClose={() => setOpenUploadProduct(false)}
                    fetchData={fetchAllProducts}
                />
            )}
        </div>
    );
};

export default AllProducts;
