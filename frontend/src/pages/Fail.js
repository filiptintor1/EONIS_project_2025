import React from 'react';
import successImage from '../assets/cancel.png';
import { Link } from 'react-router-dom';

const Fail = () => {
  return (
    <div className='flex items-center justify-center mx-auto'>
      <div className='flex justify-center items-center flex-col gap-4 text-center mt-20'>
        <img src={successImage} width={144} alt="Success" />
        <h2 className='font-semibold text-3xl'>Oops! Something went wrong with your order.</h2>
        <p className='text-slate-400 max-w-[600px]'>
            We’re sorry, but there was an issue processing your purchase. Please check your payment details and try again. If the problem persists, feel free to contact our support team for assistance
        </p>
        <Link
        to={"/category-product"} 
        className='bg-orange-300 text-white font-semibold py-1 px-6 rounded-full mt-4 hover:bg-orange-500 transition-all'>
          Return to shop
        </Link>
      </div>
    </div>
  );
}

export default Fail;
