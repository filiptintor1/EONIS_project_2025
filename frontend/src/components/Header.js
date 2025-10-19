import React, { useContext, useEffect, useState } from 'react'
import logo from "../assets/honeycomb.png"
import { IoCartOutline } from "react-icons/io5";
import { Link } from 'react-router-dom';
import { FaUserCircle } from "react-icons/fa";
import { useSelector } from 'react-redux';
import Context from '../context';
import { useDispatch } from 'react-redux';
import { clearUserDetails } from '../store/userSlice';



const Header = () => {
  const { cartCount, setCartCount } = useContext(Context);

  const user = useSelector((state) => state?.user?.user);
  const context = useContext(Context);
  const dispatch = useDispatch();
  const [menuDisplay, setMenuDisplay] = useState(false);

  console.log("User: ", user);

  const handleLogOut = () => {
    dispatch(clearUserDetails());
    localStorage.removeItem('token');
    setCartCount(0);
    localStorage.removeItem('cart');
  }

  



  return (
    <header className='h-16 shadow-sm bg-white fixed w-full z-40'>
      <div className='h-full container mx-auto flex items-center px-4 justify-between'>
        
        {/**LOGO */}
        <div className='flex justify-center items-center'>
          <img src={logo} width={34} className='mr-2'/>
          <div className='font-bold'>HoneyStore</div>
        </div>

        {/**Navigacija */}
        { user?.userRole !== "Admin" && (
        <div className='flex justify-center items-center ml-12'>
          <nav className='flex justify-center items-center'>
            <Link
              to={'/'}
              className='hidden md:block hover:bg-orange-300 transition-all px-3 py-1 mx-2'
            >
              Home
            </Link>
            <Link
              to={'/about-us'}
              className='hidden md:block hover:bg-orange-300 transition-all px-3 py-1 mx-2'
            >
              About Us
            </Link>
            <Link
              to={'/category-product'}
              className='hidden md:block hover:bg-orange-300 transition-all px-2 py-1 mx-2'
            >
              Shop
            </Link>
          </nav>

        </div>
        )}

        <div className='flex justify-center items-center'>
          {
            user?.userRole === "User" && (
              <div className='relative'>
                <Link to={"/cart"}>
                  <IoCartOutline className='text-3xl mx-3 cursor-pointer'/>
                </Link>
                {
                  cartCount > 0 && (
                    <div className='bg-red-500 text-sm w-4 h-4 rounded-full text-white font-semibold flex justify-center items-center absolute top-0 right-2'>{cartCount}</div>
                  )
                }
                
              </div>
            )
          }
          <div className='relative flex justify-center'>
            <div onClick={() => setMenuDisplay((preve) => !preve)}>
              <FaUserCircle className='text-3xl mx-3 cursor-pointer'/>
            </div>
            {
              menuDisplay && (
                <div className='absolute bg-white bottom-0 top-11 h-fit p-2 shadow-lg rounded'>
                  <nav className='flex flex-col min-w-36 justify-center items-center'>
                    {
                      user?.userRole === "Admin" && (
                        <Link 
                          to={"/admin-panel/all-products"}
                          onClick={() => setMenuDisplay((preve) => !preve)}
                        >
                          Admin Panel
                        </Link>
                      )
                    }
                    {
                      user?.userRole === 'User' && (
                        <div className='flex flex-col justify-center items-center gap-4'>
                          <Link
                              to={"/user-orders"}
                              onClick={() => setMenuDisplay((preve) => !preve)}
                          >
                            My orders
                          </Link>
                          <Link
                              to={"/edit-profile"}
                              onClick={() => setMenuDisplay((preve) => !preve)}
                          >
                            Edit profile
                          </Link>
                        </div>
                      )
                    }
                    
                  </nav>
                </div>
              )
            }
          </div>
        
          
          {
            user ? (
              <Link className='bg-orange-300 text-white font-semibold px-4 py-2 rounded-full ml-3 cursor-pointer hover:bg-orange-400 transition-all' onClick={handleLogOut}>Log Out</Link>
            ) : (
              <Link 
                to={"/login"}
                className='bg-orange-300 text-white font-semibold px-4 py-2 rounded-full ml-3 cursor-pointer hover:bg-orange-400 transition-all'>Log In</Link>
            )
          }
        </div>


      </div>
    </header>
  )
}

export default Header