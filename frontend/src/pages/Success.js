import React from 'react';
import successImage from '../assets/checked.png';
import { Link } from 'react-router-dom';

const Success = () => {
  return (
    <div className='flex items-center justify-center mx-auto'>
      <div className='flex justify-center items-center flex-col gap-4 text-center mt-20'>
        <img src={successImage} width={144} alt="Success" />
        <h2 className='font-semibold text-3xl'>Thank you for your order!</h2>
        <p className='text-slate-400 max-w-[600px]'>
          Your purchase has been successfully processed. We appreciate your support and can’t wait for you to enjoy our products.
        </p>
        <Link
        to={"/user-orders"} 
        className='bg-orange-300 text-white font-semibold py-1 px-6 rounded-full mt-4 hover:bg-orange-500 transition-all'>
          See your orders
        </Link>
      </div>
    </div>
  );
}

export default Success;
