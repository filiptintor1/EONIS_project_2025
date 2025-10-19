import React, { useContext, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import displayCurrencyRSD from '../helpers/displayCurrency';
import Context from '../context';

const ProductPage = () => {
    const { setCartCount } = useContext(Context);
    const params = useParams();

    const [data, setData] = useState({
        category: "",
        description: "",
        image: "",
        kindOfHoney: "",
        name: "",
        price: "",
        quantity: "",
    });

    // Fetch product data from API
    const fetchData = async () => {
        try {
            const response = await fetch(`https://localhost:7123/products/${params.id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
      
            if (response.ok) {
                const data = await response.json();
                setData({
                    category: data?.category,
                    description: data?.description,
                    image: data?.image,
                    kindOfHoney: data?.kindOfHoney,
                    name: data?.name,
                    price: data?.price,
                    quantity: data?.quantity,
                });
            } else {
                console.error("Cannot get product");
            }
        } catch (error) {
            console.error("Error occurred during getting product:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, [params.id]);  // Removed 'data' from the dependency array to prevent infinite loops

    // Add product to cart and update cart count
    const addToCart = (e) => {
        e.preventDefault();

        // Get the current cart from localStorage (or initialize an empty array if it doesn't exist)
        const currentCart = JSON.parse(localStorage.getItem('cart')) || [];

        // Check if the product is already in the cart
        const productInCart = currentCart.find(item => item.productId === params.id);

        if (productInCart) {
            // If the product exists, increase the quantity
            productInCart.quantity += 1;
        } else {
            // If the product doesn't exist, add it with a quantity of 1
            currentCart.push({ ...data, productId: params.id, quantity: 1 });
        }

        // Save the updated cart back to localStorage
        localStorage.setItem('cart', JSON.stringify(currentCart));

        // Update the cart count in the context (based on the number of unique products in the cart)
        setCartCount(currentCart.length);
    };

    return (
        <div className='h-full container flex items-center justify-center'>
            <div className='w-full flex justify-center items-center'>
                <img src={data?.image} alt="Product image" width={444} />
            </div>
            <div className='flex flex-col gap-10 w-full mt-14'>
                <div>
                    <h3 className='text-5xl font-semibold mb-6'>{data?.name}</h3>
                    <p><span className='font-semibold'>Category:</span> {data?.category}</p>
                    <p><span className='font-semibold'>Kind:</span> {data?.kindOfHoney}</p>
                </div>
                
                <div>
                    <p className='font-semibold'>Description:</p>
                    <p>{data.description}</p>
                </div>

                <div>
                    <p className='font-semibold'>Price:</p>
                    <p>{displayCurrencyRSD(data?.price)}</p>
                </div>

                <div>
                    <button 
                        className='px-6 py-1 bg-orange-300 hover:bg-orange-600 text-white rounded-full' 
                        onClick={addToCart}>
                        Add To Cart
                    </button>
                </div>
            </div>
        </div>
    );
};

export default ProductPage;
