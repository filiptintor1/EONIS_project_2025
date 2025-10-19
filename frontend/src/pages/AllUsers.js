import React, { useContext, useEffect, useState } from "react";
import SummaryApi from "../common";
import { toast } from "react-toastify";
import moment from "moment";
import { MdEdit } from "react-icons/md";
import DeleteUserModal from "../components/DeleteUserModal";
import { useSelector } from "react-redux";
import Context from "../context";
import { IoMdClose } from "react-icons/io";


const AllUsers = () => {
  const [allUsers, setAllUsers] = useState([]);
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [userToBeDeleted, setUserToBeDeleted] = useState("")
  

  const user = useSelector((state) => state?.user?.user);
  const context = useContext(Context);

  const fetchAllUsers = async () => {
    try {
      const response = await fetch('https://localhost:7123/users', {
        method: 'GET', // Use GET to fetch users
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${user?.token}`,
        }
      });
  
      if (response.ok) {
        const data = await response.json();
        setAllUsers(data);
      } else {
        console.error("Failed to fetch users");
        toast.error("Failed to fetch users");
      }
    } catch (error) {
      console.error("Error occurred while fetching users:", error);
      toast.error("An error occurred while fetching users");
    }
    
  };

  // pozivanje f-je za dobavljanje korisnika u useEffect-u
  useEffect(() => {
    fetchAllUsers();
  }, []);

  return (
    <div className="bg-white pb-4">
      <table className="w-full userTable text-center">
        <thead>
          <tr className="bg-black text-white">
            <th>Sr.</th>
            <th>Username</th>
            <th>Name</th>
            <th>Surname</th>
            <th>City</th>
            <th>Street</th>
            <th>Postal Code</th>
            <th>Contact</th>
            <th>Delete</th>
            
          </tr>
        </thead>

        <tbody className="">
          {allUsers.map((el, i) => {
            return (
              <tr className="border">
                <td>{i + 1}</td>
                <td>{el?.username}</td>
                <td>{el?.name}</td>
                <td>{el?.surname}</td>
                <td>{el?.city}</td>
                <td>{el?.street}</td>
                <td>{el?.postalCode}</td>
                <td>{el?.contact}</td>
                <td>
                  <button
                    className="bg-red-300 p-2 rounded-full cursor-pointer hover:bg-red-500 transition-all hover:text-white"
                    onClick={() => {
                      setUserToBeDeleted(el?.username)
                      
                      setOpenDeleteModal(true);
                    }}
                  >
                    <IoMdClose />
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>

      {openDeleteModal && (
        <DeleteUserModal
          onClose={() => setOpenDeleteModal(false)}
          deleteUser={userToBeDeleted}
          authUser={user}
          callFunc={fetchAllUsers}
        />
      )}
    </div>
  );
};

export default AllUsers;
