import React, { useContext, useEffect, useState } from 'react';
import productCategory from '../helpers/productCategory';
import kindOfHoney from '../helpers/kindOfHoney';
import { useLocation, useNavigate } from 'react-router-dom';
import { FaSearch } from 'react-icons/fa';
import VerticalCard from '../components/VerticalCard';
import Context from '../context';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


const CategoryProduct = () => {
    const { setCartCount } = useContext(Context);
    const location = useLocation();
    const urlSearch = new URLSearchParams(location.search);
    const urlCategoryListInArray = urlSearch.getAll('category');
    const urlKindListInArray = urlSearch.getAll('kind');

    const urlCategoryListObject = {};
    urlCategoryListInArray.forEach(el => {
        urlCategoryListObject[el] = true;
    });
    const urlKindListObject = {};
    urlKindListInArray.forEach(el => {
        urlKindListObject[el] = true;
    });

    const [selectCategory, setSelectCategory] = useState(urlCategoryListObject);
    const [selectKind, setSelectKind] = useState(urlKindListObject);
    const [sortBy, setSortBy] = useState('');
    const [data, setData] = useState([]);
    const [filteredData, setFilteredData] = useState([]);
    const searchQuery = urlSearch.get('q') || '';
    const [search, setSearch] = useState(searchQuery);
    const navigate = useNavigate();

    const [pages, setPages] = useState(1);
    const [currentPage, setCurrentPage] = useState(1);

    const fetchData = async (page) => {
        const params = new URLSearchParams();
        Object.keys(selectCategory).forEach(key => {
            if (selectCategory[key]) params.append('category[]', key);
        });
        Object.keys(selectKind).forEach(key => {
            if (selectKind[key]) params.append('kind[]', key);
        });
        if (search) {
            params.append('q', search);
        }

        try {
            const response = await fetch(`https://localhost:7123/products?pageNumber=${page}&${params.toString()}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            const responseData = await response.json();
            setData(responseData.items);
            setFilteredData(responseData.items);
            setPages(Math.ceil(responseData.totalItemsCount / 10)); // Update total pages based on response
        } catch (error) {
            console.error('Error occurred while fetching products:', error);
        }
    };

    const handleNextPage = () => {
        if (currentPage < pages) {
            setCurrentPage(prev => prev + 1);
        }
    };

    const handlePreviousPage = () => {
        if (currentPage > 1) {
            setCurrentPage(prev => prev - 1);
        }
    };

    useEffect(() => {
        fetchData(currentPage); // Fetch data when currentPage changes
    }, [currentPage, selectCategory, selectKind, search, sortBy]);

    useEffect(() => {
        // Initial fetch to load data
        fetchData(currentPage);
    }, []);

    const addToCart = (e, productId) => {
        e.preventDefault();
        const currentCart = JSON.parse(localStorage.getItem('cart')) || [];
        const product = data.find(item => item.productId === productId);
        const productInCart = currentCart.find(item => item.productId === productId);
         const quantityInCart = productInCart ? productInCart.quantity : 0;

         // Proveravamo da li ima dovoljno na stanju
    if (quantityInCart + 1 > product.quantity) {
        toast.error(`Nema dovoljno proizvoda na stanju. Dostupno: ${product.quantity}`);
        return; // izlazimo iz funkcije, ne dodajemo proizvod
    }
        
        if (productInCart) {
            productInCart.quantity += 1;
        } else {
            currentCart.push({ ...product, quantity: 1 });
        }

        toast.success(`${product.name} je dodat u korpu!`);

        localStorage.setItem('cart', JSON.stringify(currentCart));
        setCartCount(currentCart.length);
    };

    const applyFilters = () => {
        let filtered = [...data];
                
        // Filter by category
        if (Object.keys(selectCategory).some(key => selectCategory[key])) {
            filtered = filtered.filter(item => selectCategory[item.category]);
        }

        // Filter by kind of honey
        if (Object.keys(selectKind).some(key => selectKind[key])) {
            filtered = filtered.filter(item => selectKind[item.kindOfHoney]);
        }

        // Filter by search term
        if (search) {
            filtered = filtered.filter(item => 
                item.name.toLowerCase().includes(search.toLowerCase())
            );
        }

        // Sorting the filtered results
        if (sortBy === 'asc') {
            filtered = filtered.sort((a, b) => a.price - b.price);
        } else if (sortBy === 'dsc') {
            filtered = filtered.sort((a, b) => b.price - a.price);
        }

        setFilteredData(filtered);
    };

    useEffect(() => {
        applyFilters();
    }, [data, selectCategory, selectKind, search, sortBy]);

    return (
        <div className='container mx-auto p-4'>
            <div className='hidden lg:grid grid-cols-[200px,1fr]'>
                <div className='bg-white py-2 px-6 overflow-y-scroll shadow-md'>
                    {/* Filters and Sorting UI */}
                    <div>
                        <h3 className='text-base uppercase font-medium text-slate-500 border-b pb-1 border-slate-300'>Sort by</h3>
                        <form className='text-sm flex flex-col gap-2 py-2'>
                            <div className='flex items-center gap-3'>
                                <input
                                    type='radio'
                                    name='sortBy'
                                    checked={sortBy === 'asc'}
                                    value='asc'
                                    onChange={(e) => setSortBy(e.target.value)}
                                />
                                <label>Price - Low to High</label>
                            </div>
                            <div className='flex items-center gap-3'>
                                <input
                                    type='radio'
                                    name='sortBy'
                                    checked={sortBy === 'dsc'}
                                    value='dsc'
                                    onChange={(e) => setSortBy(e.target.value)}
                                />
                                <label>Price - High to Low</label>
                            </div>
                        </form>
                    </div>

                    <div className='mt-8'>
                        <h3 className='text-base uppercase font-medium text-slate-500 border-b pb-1 border-slate-300'>Category</h3>
                        <form className='text-sm flex flex-col gap-2 py-2'>
                            {productCategory.map((categoryName) => (
                                <div key={categoryName.value} className='flex items-center gap-3'>
                                    <input
                                        type='checkbox'
                                        checked={selectCategory[categoryName.value] || false}
                                        value={categoryName.value}
                                        onChange={(e) => setSelectCategory(prev => ({ ...prev, [e.target.value]: e.target.checked }))}
                                    />
                                    <label>{categoryName.label}</label>
                                </div>
                            ))}
                        </form>
                    </div>

                    <div className='mt-8'>
                        <h3 className='text-base uppercase font-medium text-slate-500 border-b pb-1 border-slate-300'>Kind Of Honey</h3>
                        <form className='text-sm flex flex-col gap-2 py-2'>
                            {kindOfHoney.map((kind) => (
                                <div key={kind.value} className='flex items-center gap-3'>
                                    <input
                                        type='checkbox'
                                        checked={selectKind[kind.value] || false}
                                        value={kind.value}
                                        onChange={(e) => setSelectKind(prev => ({ ...prev, [e.target.value]: e.target.checked }))}
                                    />
                                    <label>{kind.label}</label>
                                </div>
                            ))}
                        </form>
                    </div>
                </div>

                <div className='px-8'>
                    <div className='hidden lg:flex items-center w-full justify-between border bg-white rounded-full focus-within:shadow pl-4 mx-auto mb-8'>
                        <input
                            type='text'
                            placeholder='Search product...'
                            className='w-full outline-none pl-2 bg-white'
                            onChange={(e) => {
                                setSearch(e.target.value);
                                navigate(`/category-product?q=${e.target.value}`);
                            }}
                            value={search}
                        />
                        <div className='text-lg min-w-[70px] h-12 bg-orange-300 flex items-center justify-center rounded-r-full text-white'>
                            <FaSearch />
                        </div>
                    </div>

                    <div className='flex justify-between'>
                        <button onClick={handlePreviousPage} className='px-4 py-1 bg-orange-300 text-white'>Previous</button>
                        <p>{currentPage} of {pages}</p>
                        <button onClick={handleNextPage} className='px-4 py-1 bg-orange-300 text-white'>Next</button>
                    </div>

                    <p className='font-medium text-slate-800 text-lg my-2' >
                        Search results: {filteredData.length}
                    </p>
                    <div className='h-[calc(100vh-120px)] overflow-y-scroll max-h-[calc(100vh-120px)]' >
                        {filteredData.length > 0 && (
                            <VerticalCard data={filteredData} handleAddToCart={addToCart} />
                        )}
                    </div>
                </div>
            </div>
            <ToastContainer position="top-right" autoClose={2000} />

        </div>
    );
};

export default CategoryProduct;
