import React from 'react'
import { FaArrowRightLong } from 'react-icons/fa6'
import { MdHexagon } from 'react-icons/md'
import { Link } from 'react-router-dom'

const ShopNowButton = () => {
  return (
    <div className='relative flex justify-center items-center w-fit mt-10'>
        <MdHexagon className='text-orange-400 text-8xl absolute left-0'/>
            <div className='ml-6 flex justify-center items-center'>
            <Link to={'/category-product'} className='text-2xl font-semibold z-10 mr-2'>Shop now</Link>
            <FaArrowRightLong />
        </div>
    </div>
  )
}

export default ShopNowButton