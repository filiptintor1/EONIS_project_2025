import React from 'react';
import { Link } from 'react-router-dom';
import displayCurrencyRSD from '../helpers/displayCurrency';
import Context from '../context';
import { useContext } from 'react'


const VerticalCard = ({
    data = [],
    handleAddToCart
}) => {
  const { cart, setCart, setCartCount } = useContext(Context);
  



    console.log(data)

  const handleAddToCartStorage = (e, productId) => {
    e.preventDefault();

    handleAddToCart(e, productId)
    
  };

    return (
        <div className="grid grid-cols-[repeat(auto-fit,minmax(260px,300px))] md:gap-4 overflow-x-scroll scrollbar-none transition-all">
            {
                data.map((product) => (
                    <Link to={`/product-page/${product?.productId}`} key={product?.productId} className="w-full min-w-[280px] md:min-w-[300px] max-w-[280px] md:max-w-[300px] bg-white rounded-sm shadow">
                        <div className="bg-slate-200 h-48 p-4 min-w-[280px] md:min-w-[145px] flex justify-center items-center">
                            <img
                                src={product?.image}
                                alt="Product image"
                                className="object-scale-down h-full cursor-pointer hover:scale-110 transition-all mix-blend-multiply"
                            />
                        </div>
                        <div className="p-4 grid gap-3">
                            <h2 className="font-medium text-base md:text-lg text-ellipsis line-clamp-1 text-black">
                                {product?.name}
                            </h2>
                            <p className="capitalize text-slate-500">
                                {product.category}
                            </p>
                            <p className="capitalize text-slate-500">
                                {product.kindOfHoney}
                            </p>
                            <div className="flex gap-3">
                                <p className="text-red-600 font-medium text-lg">
                                    {displayCurrencyRSD(product?.price)}
                                </p>
                            </div>
                            <button
                                className="text-sm bg-orange-300 hover:bg-orange-500 text-black px-3 py-0.5 rounded-full transition-all"
                                onClick={(e) => handleAddToCartStorage(e, product?.productId)}
                            >
                                Add to Cart

                                
                            </button>
                        </div>
                    </Link>
                ))
            }
        </div>
    );
};

export default VerticalCard;
