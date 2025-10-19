import React, { useContext, useEffect, useState } from 'react'
import displayCurrencyRSD from '../helpers/displayCurrency'
import { MdDelete, MdEdit } from "react-icons/md";
import UpdateProduct from './UpdateProduct';
import DeleteProductModal from './DeleteProductModal';
import { useSelector } from 'react-redux';
import Context from '../context';

const AdminPanelProductCard = ({
  data = {},
  fetchData,
}) => {
  const user = useSelector((state) => state?.user?.user);
  const context = useContext(Context);
  
  console.log("DATA: ", data);

  const [openEditProduct, setOpenEditProduct] = useState(false)
  const [openDeleteProduct, setOpenDeleteProduct] = useState(false);

  const handleEdit = () => {

  }
  
  return (
    <div className="grid grid-cols-[repeat(auto-fit,minmax(260px,300px))] justify-center md:justify-between md:gap-4 overflow-x-scroll scrollbar-none transition-all">
      <div className="relative w-full min-w-[280px] md:min-w-[300px] max-w-[280px] md:max-w-[300px] bg-white rounded-sm shadow border">
        <div className="bg-slate-200 h-48 p-4 min-w-[280px] md:min-w-[145px] flex justify-center items-center">
          <img
            src={data?.image}
            alt="Product image"
            className="object-scale-down h-full cursor-pointer hover:scale-110 transition-all mix-blend-multiply"
          />
        </div>
        <div className="p-4 grid gap-3">
          <h2 className="font-medium text-base md:text-lg text-ellipsis line-clamp-1 text-black">
            {data?.name}
          </h2>
          <p className="capitalize text-slate-500">
            {data.category}
          </p>
          <p className="capitalize text-slate-500">
            {data.kindOfHoney}
          </p>
          <div className="flex gap-3">
            <p className="text-red-600 font-medium text-lg">
              {displayCurrencyRSD(data?.price)}
            </p>
          </div>
        </div>

        <button className='absolute top-3 right-12 p-2 bg-green-300 rounded-full hover:bg-green-500 hover:text-white' onClick={() => setOpenEditProduct(true)}>
          <MdEdit />
        </button>

        <button className='absolute top-3 right-2 p-2 bg-red-300 rounded-full hover:bg-red-500 hover:text-white' onClick={() => setOpenDeleteProduct(true)}>
          <MdDelete />
        </button>
      </div>

      {openEditProduct && (
          <UpdateProduct
                  onClose={() => setOpenEditProduct(false)}
                  fetchData={fetchData}
                  productData={data}
            />
      )}
      {openDeleteProduct && (
          <DeleteProductModal
                  onClose={() => setOpenDeleteProduct(false)}
                  fetchData={fetchData}
                  productData={data}
                  user={user}
            />
      )}
    </div>
  )
}

export default AdminPanelProductCard;
