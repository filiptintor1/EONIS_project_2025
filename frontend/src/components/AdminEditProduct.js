// import React, { useEffect, useState } from 'react';
// import productCategory from '../helpers/productCategory';
// import kindOfHoney from '../helpers/kindOfHoney';
// import { useLocation, useNavigate } from 'react-router-dom';
// import { FaSearch } from 'react-icons/fa';
// import VerticalCard from '../components/VerticalCard';

// const CategoryProduct = () => {
//     const location = useLocation();
//     const urlSearch = new URLSearchParams(location.search);
//     const urlCategoryListInArray = urlSearch.getAll('category');
//     const urlKindListInArray = urlSearch.getAll('kind');
    
//     const urlCategoryListObject = {};
//     urlCategoryListInArray.forEach(el => {
//         urlCategoryListObject[el] = true;
//     });
//     const urlKindListObject = {};
//     urlKindListInArray.forEach(el => {
//         urlKindListObject[el] = true;
//     });

//     const [selectCategory, setSelectCategory] = useState(urlCategoryListObject);
//     const [selectKind, setSelectKind] = useState(urlKindListObject);
//     const [sortBy, setSortBy] = useState('');
//     const [data, setData] = useState([]);
//     const [filteredData, setFilteredData] = useState([]);
//     const searchQuery = urlSearch.get('q') || '';
//     const [search, setSearch] = useState(searchQuery);
//     const navigate = useNavigate();

//     // Fetching data from the server
//     const fetchData = async () => {
//         const params = new URLSearchParams();
        
//         // Add categories to params
//         Object.keys(selectCategory).forEach(key => {
//             if (selectCategory[key]) params.append('category', key);
//         });

//         // Add kinds to params
//         Object.keys(selectKind).forEach(key => {
//             if (selectKind[key]) params.append('kind', key);
//         });

//         // Add search query
//         if (search) {
//             params.append('q', search);
//         }

//         try {
//             const response = await fetch(`https://localhost:7123/products?${params.toString()}`, {
//                 method: 'GET',
//                 headers: {
//                     'Content-Type': 'application/json',
//                 },
//             });

//             const responseData = await response.json();
//             console.log("Fetched data:", responseData.items);
//             setData(responseData.items);
//             setFilteredData(responseData.items);
//         } catch (error) {
//             console.error('Error occurred while fetching products:', error);
//         }
//     };

//     // Filtering data based on selected categories, kinds, and search
//     const applyFilters = () => {
//         let filtered = data;

//         // Filter by category
//         if (Object.keys(selectCategory).some(key => selectCategory[key])) {
//             filtered = filtered.filter(item => selectCategory[item.category] ?? false);
//         }

//         // Filter by kind of honey
//         if (Object.keys(selectKind).some(key => selectKind[key])) {
//             filtered = filtered.filter(item => selectKind[item.kindOfHoney] ?? false);
//         }

//         // Filter by search term
//         if (search) {
//             filtered = filtered.filter(item => 
//                 item.name.toLowerCase().includes(search.toLowerCase()) || 
//                 item.description.toLowerCase().includes(search.toLowerCase())
//             );
//         }

//         // Sorting the filtered results
//         if (sortBy === 'asc') {
//             filtered = filtered.sort((a, b) => b.price - a.price);
//         } else if (sortBy === 'dsc') {
//             filtered = filtered.sort((a, b) => a.price - b.price);
//         }

//         setFilteredData(filtered);
//     };

//     const handleOnChangeSortBy = (e) => {
//         const { value } = e.target;
//         setSortBy(value);
//     };

//     const handleSelectCategory = (e) => {
//         const { value, checked } = e.target;
//         setSelectCategory(prev => ({ ...prev, [value]: checked }));
//     };

//     const handleSelectKind = (e) => {
//         const { value, checked } = e.target;
//         setSelectKind(prev => ({ ...prev, [value]: checked }));
//     };

//     const handleSearch = (e) => {
//         const { value } = e.target;
//         setSearch(value);
//         navigate(`/category-product?q=${value}`);
//     };

//     // Apply filters and fetch data whenever relevant state changes
//     useEffect(() => {
//         fetchData();
//     }, [selectCategory, selectKind, search]);

//     useEffect(() => {
//         applyFilters();
//     }, [data, selectCategory, selectKind, search, sortBy]);

//     return (
//         <div className='container mx-auto p-4'>
//             <div className='hidden lg:grid grid-cols-[200px,1fr]'>
//                 <div className='bg-white py-2 px-6 overflow-y-scroll shadow-md'>
//                     <div>
//                         <h3 className='text-base uppercase font-medium text-slate-500 border-b pb-1 border-slate-300'>
//                             Sort by
//                         </h3>
//                         <form className='text-sm flex flex-col gap-2 py-2'>
//                             <div className='flex items-center gap-3'>
//                                 <input
//                                     type='radio'
//                                     name='sortBy'
//                                     checked={sortBy === 'asc'}
//                                     value='asc'
//                                     onChange={handleOnChangeSortBy}
//                                 />
//                                 <label>Price - Low to High</label>
//                             </div>
//                             <div className='flex items-center gap-3'>
//                                 <input
//                                     type='radio'
//                                     name='sortBy'
//                                     checked={sortBy === 'dsc'}
//                                     value='dsc'
//                                     onChange={handleOnChangeSortBy}
//                                 />
//                                 <label>Price - High to Low</label>
//                             </div>
//                         </form>
//                     </div>

//                     <div className='mt-8'>
//                         <h3 className='text-base uppercase font-medium text-slate-500 border-b pb-1 border-slate-300'>
//                             Category
//                         </h3>
//                         <form className='text-sm flex flex-col gap-2 py-2'>
//                             {productCategory.map((categoryName) => (
//                                 <div key={categoryName.value} className='flex items-center gap-3'>
//                                     <input
//                                         type='checkbox'
//                                         checked={selectCategory[categoryName.value] || false}
//                                         value={categoryName.value}
//                                         onChange={handleSelectCategory}
//                                     />
//                                     <label>{categoryName.label}</label>
//                                 </div>
//                             ))}
//                         </form>
//                     </div>

//                     <div className='mt-8'>
//                         <h3 className='text-base uppercase font-medium text-slate-500 border-b pb-1 border-slate-300'>
//                             Kind Of Honey
//                         </h3>
//                         <form className='text-sm flex flex-col gap-2 py-2'>
//                             {kindOfHoney.map((kind) => (
//                                 <div key={kind.value} className='flex items-center gap-3'>
//                                     <input
//                                         type='checkbox'
//                                         checked={selectKind[kind.value] || false}
//                                         value={kind.value}
//                                         onChange={handleSelectKind}
//                                     />
//                                     <label>{kind.label}</label>
//                                 </div>
//                             ))}
//                         </form>
//                     </div>
//                 </div>

//                 <div className='px-8'>
//                     <div className='hidden lg:flex items-center w-full justify-between border bg-white rounded-full focus-within:shadow pl-4 mx-auto mb-8'>
//                         <input
//                             type='text'
//                             placeholder='Search product...'
//                             className='w-full outline-none pl-2 bg-white'
//                             onChange={handleSearch}
//                             value={search}
//                         />
//                         <div className='text-lg min-w-[70px] h-12 bg-orange-300 flex items-center justify-center rounded-r-full text-white'>
//                             <FaSearch />
//                         </div>
//                     </div>

//                     <p className='font-medium text-slate-800 text-lg my-2'>
//                         Search results: {filteredData.length}
//                     </p>
//                     <div className='h-[calc(100vh-120px)] overflow-y-scroll max-h-[calc(100vh-120px)]'>
//                         {filteredData.length > 0 && <VerticalCard data={filteredData} />}
//                     </div>
//                 </div>
//             </div>
//         </div>
//     );
// };

// export default CategoryProduct;
