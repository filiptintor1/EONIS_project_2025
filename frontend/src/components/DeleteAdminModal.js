import React from 'react';
import { IoClose } from 'react-icons/io5';

const DeleteAdminModal = ({
    onClose,
    deleteAdmin, // Now directly using adminId
    authUser,
    callFunc,
}) => {
    console.log("DEL ADMIN ID: ", deleteAdmin);
    console.log("AUTH USER:", authUser);

    console.log("id: ", deleteAdmin.id)

    const handleDeleteAdmin = async () => {
      

        try {
            // Directly send the DELETE request using adminId
            const deleteResponse = await fetch(`https://localhost:7123/admins/${deleteAdmin.id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${authUser?.token}`,
                }
            });

            if (deleteResponse.ok) {
                console.log("Admin deleted successfully!");
                callFunc();
                onClose();
            } else {
                console.error("Failed to delete admin");
            }
        } catch (error) {
            console.error("Error occurred while deleting admin:", error);
        }
    };

    return (
        <div>
            <div className="fixed bg-slate-200 bg-opacity-75 h-full w-full top-0 left-0 right-0 bottom-0 flex justify-center items-center">
                <div className="bg-white p-4 rounded w-full max-w-2xl overflow-hidden relative">
                    <div className="flex justify-between items-center pb-3">
                        <h2 className="font-bold text-2xl mx-auto">Are you sure you want to delete this admin?</h2>
                        <div
                            className="absolute right-4 w-fit ml-auto text-2xl hover:text-red-600 cursor-pointer"
                            onClick={onClose}
                        >
                            <IoClose />
                        </div>
                    </div>

                    <p className='text-slate-700 font-semibold text-center text-2xl'>@{deleteAdmin.username}</p>

                    <div className='flex items-center justify-center gap-2 mt-10'>
                        <button className='bg-red-500 text-white px-6 py-1 rounded-full hover:bg-red-700' onClick={handleDeleteAdmin}>Delete</button>
                        <button className='bg-slate-200 text-black px-6 py-1 rounded-full hover:bg-slate-400' onClick={onClose}>Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DeleteAdminModal;
