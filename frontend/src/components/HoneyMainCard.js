import React, { useEffect, useRef, useState, useContext } from 'react';
import { FaRegArrowAltCircleRight } from "react-icons/fa";
import { FaRegArrowAltCircleLeft } from "react-icons/fa";
import { Link } from 'react-router-dom';
import displayCurrencyRSD from '../helpers/displayCurrency';
import Context from '../context';  // Import the context for cart count

const HoneyMainCard = () => {
  const [products, setProducts] = useState([]);
  const { setCartCount } = useContext(Context);  // Use context to update cart count
  const scrollElement = useRef();

  const fetchProducts = async () => {
    try {
      const response = await fetch('https://localhost:7123/products', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
  
      if (response.ok) {
        const data = await response.json();
        setProducts(data?.items.slice(0, 7) || []); // Limit the results to 7 in the frontend
      } else {
        console.error('Failed to fetch products');
      }
    } catch (error) {
      console.error('Error while fetching products:', error);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []); // Fetch only once when component mounts

  // Function to add product to cart and update cart count
// Function to add product to cart and update cart count
const addToCart = (product) => {
  // Get the existing cart from localStorage, or initialize an empty array if none exists
  const currentCart = JSON.parse(localStorage.getItem('cart')) || [];

  // Find if the product already exists in the cart
  const productInCart = currentCart.find(item => item.productId === product.productId);

  if (productInCart) {
      // If the product already exists, increase its quantity
      productInCart.quantity += 1;
  } else {
      // If the product doesn't exist in the cart, add it with an initial quantity of 1
      currentCart.push({ ...product, quantity: 1 });
  }

  // Save the updated cart back to localStorage
  localStorage.setItem('cart', JSON.stringify(currentCart));

  // Update the cart count in the context based on the total number of unique items in the cart
  setCartCount(currentCart.length);
};


  const scrollLeft = () => {
    scrollElement.current.scrollLeft -= 300; // Scroll 300px left
  };

  const scrollRight = () => {
    scrollElement.current.scrollLeft += 300; // Scroll 300px right
  };

  return (
    <div className="container mx-auto px-4 my-6 relative mb-60">
      <h3 className="text-center text-4xl font-semibold mb-4">Most popular products</h3>
      <div className='h-1 w-20 mx-auto bg-orange-300 mb-10'></div>
      <p className="text-center max-w-2xl mx-auto mb-14">
        Our bestsellers include raw honey, infused flavors, and honeycomb—pure, flavorful, and full of natural benefits.
      </p>

      {/* Slider with scroll buttons */}
      <div className="relative flex items-center gap-4 overflow-hidden">
        <button className="absolute left-0 z-10 text-2xl p-2 bg-white rounded-full shadow-md hidden md:block" onClick={scrollLeft}>
          <FaRegArrowAltCircleLeft />
        </button>
        <button className="absolute right-0 z-10 text-2xl p-2 bg-white rounded-full shadow-md hidden md:block" onClick={scrollRight}>
          <FaRegArrowAltCircleRight />
        </button>

        {/* Products slider */}
        <div className="flex items-center gap-6 overflow-x-auto scroll-smooth scrollbar-none" ref={scrollElement}>
          {products.length > 0 ? (
            products.map((product) => (
              <div
                key={product.id}
                className="w-full min-w-[280px] md:min-w-[320px] max-w-[280px] md:max-w-[320px] bg-white rounded-sm shadow mx-4"
              >
                <div className="p-4 grid gap-3 text-center flex justify-center items-center">
                  <div className='flex items-center justify-center'>
                    <img
                      src={product.image} // Product image
                      alt={product.name}
                      className="w-full h-32 object-cover mb-2 rounded" // Styling for the image
                    />
                  </div>
                  <h2 className="font-medium text-base md:text-lg text-ellipsis line-clamp-1 text-black">
                    {product.name}
                  </h2>
                  <p className="capitalize text-slate-500">{product.category}</p>
                  <p className="text-slate-800 font-medium">
                    {displayCurrencyRSD(product.price)}
                  </p>
                  <button
                    className="text-sm bg-orange-300 hover:bg-orange-400 text-white px-3 py-0.5 rounded-full"
                    onClick={() => addToCart(product)}  // Add to cart
                  >
                    Add to Cart
                  </button>
                  
                </div>
              </div>
            ))
          ) : (
            <p>No products available</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default HoneyMainCard;
