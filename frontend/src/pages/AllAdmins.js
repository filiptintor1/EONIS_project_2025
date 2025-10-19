import React, { useContext, useEffect, useState } from 'react'
import AddAdminModal from '../components/AddAdminModal';
import { IoMdClose } from 'react-icons/io';
import { useSelector } from 'react-redux';
import Context from '../context';
import { RiUserAddFill } from "react-icons/ri";
import DeleteAdminModal from '../components/DeleteAdminModal';


const AllAdmins = () => {

    const [addAdminModal, setAddAdminModal] = useState(false);
    const [allAdmins, setAllAdmins] = useState([]);
    const [adminToBeDeleted, setAdminToBeDeleted] = useState({
      id: "",
      username: ""
    });
    
    const [openDeleteModal, setOpenDeleteModal] = useState(false);

    const user = useSelector((state) => state?.user?.user);
    const context = useContext(Context);

    console.log("USERRRR: ", user)
    console.log("ALL ADMINS: ", allAdmins)

    const fetchAllAdmins = async () => {
        try {
          const response = await fetch('https://localhost:7123/admins', {
            method: 'GET', // Use GET to fetch users
            headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${user?.token}`,
            }
          });
      
          if (response.ok) {
            const data = await response.json();
            setAllAdmins(data);
          } else {
            console.error("Failed to fetch users");
          }
        } catch (error) {
          console.error("Error occurred while fetching users:", error);
          
        }
        
      };
    
      // pozivanje f-je za dobavljanje korisnika u useEffect-u
      useEffect(() => {
        fetchAllAdmins();
      }, []);

  return (
    <div className="bg-white pb-4">

        <div className='w-full flex justify-start my-4'>
            <button className='flex w-fit py-1 px-6 border text-green-500 font-semibold rounded-full border-green-500 hover:bg-green-500 hover:text-white' onClick={() => setAddAdminModal(true)}>
                Add new Admin
            </button>
        </div>

      <table className="w-full userTable text-center">
        <thead>
          <tr className="bg-black text-white">
            <th>Sr.</th>
            <th>Username</th>
            <th>Name</th>
            <th>Surname</th>
            <th>Contact</th>
            <th>Delete</th>
            
          </tr>
        </thead>

        <tbody className="">
          {allAdmins.map((el, i) => {
            return (
              <tr className="border">
                <td>{i + 1}</td>
                <td>{el?.username}</td>
                <td>{el?.name}</td>
                <td>{el?.surname}</td>
                <td>{el?.contact}</td>
                <td>
                  {
                    user?.user?.username !== el?.username && (
                      <button
                        className="bg-red-300 p-2 rounded-full cursor-pointer hover:bg-red-500 transition-all hover:text-white"
                        onClick={() => {
                            setAdminToBeDeleted({ id: el?.adminId, username: el?.username }); 
                            setOpenDeleteModal(true)
                          
                        }}
                      >
                        <IoMdClose />
                      </button>
                    )
                  }
                  
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>


      {addAdminModal && (
        <AddAdminModal
          onClose={() => setAddAdminModal(false)}
          callFunc={fetchAllAdmins} 
        />
      )}

      {openDeleteModal && (
        <DeleteAdminModal
          onClose={() => setOpenDeleteModal(false)}
          deleteAdmin={adminToBeDeleted}
          authUser={user}
          callFunc={fetchAllAdmins}
        />
      )}
    </div>
  )
}

export default AllAdmins