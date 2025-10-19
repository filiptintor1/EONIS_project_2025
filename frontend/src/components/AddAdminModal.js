import React, { useContext, useState } from 'react'
import { IoClose } from 'react-icons/io5'
import { useSelector } from 'react-redux';
import Context from '../context';
import SubmitFormButton from './SubmitFormButton';

const AddAdminModal = ({
    onClose,
    callFunc
}) => {

    const user = useSelector((state) => state?.user?.user);
    const context = useContext(Context);

    const [data, setData] = useState({
        username: "",
        name: "",
        surname: "",
        contact: "",
        password: "",
        confPassword: "",
    });

    const handleOnChange = (e) => {
        const { name, value } = e.target;
    
        setData((prev) => ({
          ...prev,
          [name]: value
        }));
    
        console.log(data)
    };



    const handleSubmit = async (e) => {
        e.preventDefault();
    
        if (data.password !== data.confPassword) {
          alert("Passwords do not match");
          return;
        }
    
        try {
          const response = await fetch('https://localhost:7123/admins', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${user?.token}`
            },
            body: JSON.stringify({
                username: data?.username,
                name: data?.name,
                surname: data?.surname,
                contact: data?.contact,
                password: data?.password,
            })
          });
    
    
          if (response.ok) {
            console.log("Admin successfully created!");
            callFunc();
            onClose();
          } else {
            console.error("Signup failed");
          }
        } catch (error) {
          console.error("Error occurred during signup:", error);
        }
      };

  return (
    <div className="fixed bg-slate-200 bg-opacity-75 h-full w-full top-0 left-0 right-0 bottom-0 flex justify-center items-center">
        <div className="bg-white p-4 rounded w-full max-w-2xl h-full max-h-[90%] mt-14 overflow-hidden relative">
            <div className="flex justify-between items-center pb-3">
                <h2 className="font-bold text-lg mx-auto">Add new Admin</h2>
                <div
                    className="absolute right-4 w-fit ml-auto text-2xl hover:text-red-600 cursor-pointer"
                    onClick={onClose}
                >
                    <IoClose />
                </div>
            </div>

            <form
                className="grid p-4 gap-4 overflow-y-scroll pb-5"
                onSubmit={handleSubmit}
                >
                <label htmlFor="username">Username: </label>
                <input
                    type="text"
                    id="username"
                    placeholder="Enter admin's username"
                    name="username"
                    value={data?.username}
                    onChange={handleOnChange}
                    className="p-2 bg-slate-100 border rounded"
                    required
                />

                <div className='flex w-full gap-6'>
                    <div className='flex flex-col w-full'>
                        <label htmlFor="name">Name: </label>
                        <input
                            type="text"
                            id="name"
                            placeholder="Enter admin's name"
                            name="name"
                            value={data?.name}
                            onChange={handleOnChange}
                            className="p-2 bg-slate-100 border rounded"
                            required
                        />
                    </div>
                    
                    <div className='flex flex-col w-full'>
                        <label htmlFor="surname">Surname: </label>
                        <input
                            type="text"
                            id="surname"
                            placeholder="Enter admin's surname"
                            name="surname"
                            value={data?.surname}
                            onChange={handleOnChange}
                            className="p-2 bg-slate-100 border rounded"
                            required
                        />
                    </div>
                </div>

                <label htmlFor="contact">Contact: </label>
                <input
                    type="text"
                    id="contact"
                    placeholder="Enter admin's contact"
                    name="contact"
                    value={data?.contact}
                    onChange={handleOnChange}
                    className="p-2 bg-slate-100 border rounded"
                    required
                />      

                <label htmlFor="password" className="mt-3">
                    Password:
                </label>
                <input
                    type="password"
                    id="password"
                    placeholder="Enter admin's password"
                    name="password"
                    value={data?.password}
                    onChange={handleOnChange}
                    className="p-2 bg-slate-100 border rounded"
                    required
                /> 

                <label htmlFor="confPassword" className="mt-3">
                    Confirm Password:
                </label>
                <input
                    type="password"
                    id="confPassword"
                    placeholder="Confirm password"
                    name="confPassword"
                    value={data?.confPassword}
                    onChange={handleOnChange}
                    className="p-2 bg-slate-100 border rounded"
                    required
                /> 

                <SubmitFormButton title={"Create new Admin"}/>
                
            </form>
            
        </div>
    </div>
  )
}

export default AddAdminModal