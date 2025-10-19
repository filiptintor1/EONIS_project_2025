import React from 'react'
import { FaInstagram } from "react-icons/fa";
import { FaYoutube } from "react-icons/fa";
import { FaFacebook } from "react-icons/fa";
import { FaGoogle } from "react-icons/fa";
import { FaTiktok } from "react-icons/fa";





const Footer = () => {
  return (
    <footer className='bg-orange-300'>
        <div className='container mx-auto p-4 flex md:justify-between md:items-center justify-center'>

            <div className='text-slate-600'>© 2024 HoneyStore. All rights reserved.</div>

            <div className='hidden md:flex items-center justify-center mr-8'>
                <FaInstagram className='text-3xl bg-orange-300 text-white mx-2 p-1 cursor-pointer'/>
                <FaYoutube className='text-3xl bg-orange-300 text-white mx-2 p-1 cursor-pointer'/>
                <FaFacebook className='text-3xl bg-orange-300 text-white mx-2 p-1 cursor-pointer'/>
                <FaTiktok className='text-3xl bg-orange-300 text-white mx-2 p-1 cursor-pointer'/>
                <FaGoogle className='text-3xl bg-orange-300 text-white mx-2 p-1 cursor-pointer'/>
            </div>

            <div className='hidden md:flex justify-center items-center'>
                <div className='text-slate-600 mx-4 cursor-pointer'>Terms of use</div>
                <div className='text-slate-600 mx-4 cursor-pointer'>Cookie Policy</div>
                
            </div>

        </div>
    </footer>
  )
}

export default Footer