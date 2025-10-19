import React, { useContext, useState } from 'react'
import { IoClose } from "react-icons/io5";
import productCategory from '../helpers/productCategory';
import kindOfHoney from '../helpers/kindOfHoney'
import SubmitFormButton from './SubmitFormButton';
import { useSelector } from 'react-redux';
import Context from '../context';


const UpdateProduct = (
    {
        onClose,
        fetchData,
        productData
    }
) => {

    const user = useSelector((state) => state?.user?.user);
    const context = useContext(Context);

    console.log(productData)
    const [data, setData] = useState({
        ...productData,
        productId: productData?.productId,
        name: productData?.name,
        image: productData?.image,
        category: productData?.category,
        kindOfHoney: productData?.kindOfHoney,
        price: productData?.price,
        quantity: productData?.quantity,
        description: productData?.description
      });

      const handleOnChange = (e) => {
        const { name, value } = e.target;

        setData((preve) => {
          return {
            ...preve,
            [name]: value,
          };
        });

      }

      const handleSubmit = async (e) => {
        e.preventDefault();
        
        console.log(data)
        
    
        try {
          const response = await fetch(`https://localhost:7123/products/${data?.productId}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${user?.token}`
            },
            body: JSON.stringify({
                name: data?.name,
                description: data?.description,
                category: data?.category,
                quantity: Number(data?.quantity),
                image: data?.image,
                kindOfHoney: data?.kindOfHoney,
                price: Number(data?.price)
            })
          });
    
    
          if (response.ok) {
            console.log("Product successfully updated!");
            fetchData();
            onClose();
          } else {
            console.error("Upload fail");
          }
        } catch (error) {
          console.error("Error occurred during upload:", error);
        }
      }

  return (
    <div className="fixed bg-slate-200 bg-opacity-75 h-full w-full top-0 left-0 right-0 bottom-0 flex justify-center items-center z-10">
        <div className="bg-white p-4 rounded w-full max-w-2xl h-full max-h-[85%] mt-10 overflow-hidden relative">
            <div className="flex justify-between items-center pb-3">
                <h2 className="font-bold text-lg mx-auto">Upload new Product</h2>
                <div
                    className="absolute right-4 w-fit ml-auto text-2xl hover:text-red-600 cursor-pointer"
                    onClick={onClose}
                >
                    <IoClose />
                </div>
            </div>

            <form
                className="grid p-4 gap-2 overflow-y-scroll pb-5"
                onSubmit={handleSubmit}
                >
                <label htmlFor="name">Product Name: </label>
                <input
                    type="text"
                    id="name"
                    placeholder="Enter product name"
                    name="name"
                    value={data.name}
                    onChange={handleOnChange}
                    className="p-2 bg-slate-100 border rounded"
                    required
                />
                <label htmlFor="image">Image: </label>
                <input
                    type="text"
                    id="image"
                    placeholder="Enter image url"
                    name="image"
                    value={data.image}
                    onChange={handleOnChange}
                    className="p-2 bg-slate-100 border rounded"
                    required
                />

                <div className='w-full flex gap-10'>
                    <div className='flex flex-col w-full'>
                        <label htmlFor="category">Category: </label>
                        <select
                            name='category'                            
                            value={data.category}
                            onChange={handleOnChange}
                            className="p-2 bg-slate-100 border rounded"
                            required
                        >
                            <option value={""}>Select Category</option>
                            {
                                productCategory.map((el, i) => {
                                    return (
                                        <option value={el.value} key={el.value + i}>
                                            {el.label}
                                        </option>
                                    )
                                })
                            }
                        </select>
                    </div>

                    
                    <div className='flex flex-col w-full'>
                        <label htmlFor="category">Kind of honey: </label>
                        <select
                            name='kindOfHoney'                            
                            value={data.kindOfHoney}
                            onChange={handleOnChange}
                            className="p-2 bg-slate-100 border rounded"
                            required
                        >
                            <option value={""}>Select kind of honey</option>
                            {
                                kindOfHoney.map((el, i) => {
                                    return (
                                        <option value={el.value} key={el.value + i}>
                                            {el.label}
                                        </option>
                                    )
                                })
                            }
                        </select>
                    </div>
                    
                </div>

                <div className='w-full flex gap-10'>
                    <div className='flex flex-col w-full'>
                        <label htmlFor="quantity">Quantity: </label>
                        <input
                            type="number"
                            id="quantity"
                            placeholder="Enter quantity"
                            name="quantity"
                            value={data.quantity}
                            onChange={handleOnChange}
                            className="p-2 bg-slate-100 border rounded"
                            required
                        />
                    </div>

                    <div className='flex flex-col w-full'>
                        <label htmlFor="price">Price: </label>
                        <input
                            type="number"
                            id="price"
                            placeholder="Enter price"
                            name="price"
                            value={data.price}
                            onChange={handleOnChange}
                            className="p-2 bg-slate-100 border rounded"
                            required
                        />
                    </div>
                        
                </div>

                
                
                <label htmlFor="description">Description: </label>
                <textarea
                    id="description"
                    placeholder="Enter description"
                    name="description"
                    value={data.description}
                    onChange={handleOnChange}
                    rows={3}
                    className="h-28 bg-slate-100 border resize-none p-1"
                    required
                />
                
                <SubmitFormButton title={"Update product"}/>


                
            </form>
            
                    
        </div>
    </div>
  )
}

export default UpdateProduct