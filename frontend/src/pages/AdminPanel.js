import React, { useContext } from 'react'
import { MdAdminPanelSettings } from "react-icons/md";
import { useSelector } from 'react-redux';
import { Link, Outlet } from 'react-router-dom';
import Context from '../context';


const AdminPanel = () => {

    const user = useSelector((state) => state?.user?.user);
    const context = useContext(Context);

    console.log(user)

  return (
    <div className='min-h-[calc(100vh-120px)] md:flex hidden'>
        <aside className='bg-white min-h-full w-full max-w-60 shadow-lg py-10'>
            <div className='h-32 flex justify-center items-center flex-col'>
                <div className='text-5xl cursor-pointer relative flex justify-center'>
                    <MdAdminPanelSettings />
                </div>

                <p className='capitalize text-lg font-semibold mt-4'>{user?.user?.username}</p>
                <p className='text-sm'>ADMIN</p>
            </div>

            {/**Nav */}
            <div>
                <nav className='grid p-4 gap-2 text-center mt-10'>
                    <Link to={"all-users"} className='px-2 py-1 hover:bg-orange-100'>
                        All users
                    </Link>
                    <Link to={"all-admins"} className='px-2 py-1 hover:bg-orange-100'>
                        All admins
                    </Link>
                    <Link to={"all-products"} className='px-2 py-1 hover:bg-orange-100'>
                        All Products
                    </Link>
                    <Link to={"all-orders"} className='px-2 py-1 hover:bg-orange-100'>
                        All Orders
                    </Link>
                    <Link to={"edit-admin-profile"} className='px-2 py-1 hover:bg-orange-100'>
                        Edit your profile
                    </Link>
                </nav>
            </div>
        </aside>

        <main className='w-full h-full p-2'>
            <Outlet />
        </main>
    </div>
  )
}

export default AdminPanel