import React, { useContext, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import Context from '../context';

const EditAdminProfile = () => {
    const user = useSelector((state) => state?.user?.user);
    const context = useContext(Context);

    const [data, setData] = useState({
        name: "",
        surname: "",
        username: "",
        contact: "",
    });

    const handleOnChange = (e) => {
        const { name, value } = e.target;
        setData(prevData => ({ ...prevData, [name]: value }));
        console.log(data)
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        console.log(user?.user?.adminId)
        console.log(user?.user);
        console.log(data)

        try {
        const response = await fetch(`https://localhost:7123/admins/${user?.user?.adminId}`, {
            method: 'PATCH',
            headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${user?.token}`,
            },
            body: JSON.stringify({
                name: data?.name,
                surname: data?.surname,
                username: data?.username,
                contact: data?.contact
            })
        });

        


        if (response.ok) {
            console.log("Successfully changed info!")
        } else {
            console.error("Saving data failed");
        }
        } catch (error) {
        console.error("Error occurred during sving data:", error);
        }
    };


    useEffect(() => {
        if (user) {
            setData({
                name: user?.user?.name || "",
                surname: user.user?.surname || "",
                username: user.user?.username || "",
                contact: user.user?.contact || "",
            });
        }
    }, [user]); 

    return (
        <div className='container flex flex-col mx-auto'>
            <div className='flex mx-auto flex-col w-full'> {/* Increased max width */}
                <h3 className='text-3xl mt-4 w-full'>User Info:</h3>
                <form className='pt-6 flex flex-col gap-4' onSubmit={handleSubmit}>
                    <div className='grid'>
                        <label className='mb-2'>Username :</label>
                        <div className='border py-2 px-4 rounded-xl bg-slate-200'>
                            <input
                                disabled
                                type='text'
                                placeholder='Enter username'
                                name='username'
                                value={data.username}
                                onChange={handleOnChange}
                                className='w-full h-full outline-none'
                            />
                        </div>
                    </div>


                    {/* Name input */}
                    <div className='grid'>
                        <label className='mb-2'>Name :</label>
                        <div className='border py-2 px-4 rounded-xl'>
                            <input
                                type='text'
                                placeholder='Enter name'
                                name='name'
                                value={data.name}
                                onChange={handleOnChange}
                                className='w-full h-full outline-none bg-transparent'
                            />
                        </div>
                    </div>

                    {/* Surname input */}
                    <div className='grid'>
                        <label className='mb-2'>Surname :</label>
                        <div className='border py-2 px-4 rounded-xl'>
                            <input
                                type='text'
                                placeholder='Enter surname'
                                name='surname'
                                value={data.surname}
                                onChange={handleOnChange}
                                className='w-full h-full outline-none bg-transparent'
                            />
                        </div>
                    </div>
                    

                    
                        <div className='grid w-full'>
                            <label className='mb-2'>Contact :</label>
                            <div className='border py-2 px-4 rounded-xl'>
                                <input
                                    type='text'
                                    placeholder='Enter contact'
                                    name='contact'
                                    value={data.contact}
                                    onChange={handleOnChange}
                                    className='w-full h-full outline-none bg-transparent'
                                />
                            </div>
                        </div>

                    
                    

                    <button className='bg-orange-300 text-white px-6 py-2 w-fit rounded-full hover:bg-orange-400 mx-auto mt-6 transition-all'>
                        Save Changes
                    </button>
                </form>
            </div>
        </div>
    );
}

export default EditAdminProfile;
