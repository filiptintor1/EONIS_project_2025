import React, { useState } from 'react';
import { FaRegEye, FaRegEyeSlash } from "react-icons/fa";
import { useDispatch } from 'react-redux';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from "react-toastify"
import { setUserDetails } from '../store/userSlice';
import { jwtDecode } from "jwt-decode";


const Login = () => {
    const [showPassword, setShowPassword] = useState(false);
    const [data, setData] = useState({
        username: "",
        password: "",
    });
    const [error, setError] = useState("");
    const navigate = useNavigate(); // React Router v6 hook for navigation
    const dispatch = useDispatch(); 


    const handleOnChange = (e) => {
        const { name, value } = e.target;
        setData((prev) => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault(); // Prevent default form submission
        setError(""); // Clear error messages

        try {
            const response = await fetch('https://localhost:7123/authorization', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    username: data.username,
                    password: data.password,
                }),
            });

            const result = await response.json();


            if (response.ok) {
                // Store token in localStorage or cookies
                localStorage.setItem('token', result.token);

                
                const decodedToken = jwtDecode(result.token);
                const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];


                let userResponse;
                
                if(userRole === "Admin") {
                    userResponse = await fetch(`https://localhost:7123/admins/username/${result.username}`, {
                        method: 'GET',
                        headers: {
                            'Authorization': `Bearer ${result.token}`,
                            'Content-Type': 'application/json',
                        },
                    });
                } else {
                    userResponse = await fetch(`https://localhost:7123/users/username/${result.username}`, {
                        method: 'GET',
                        headers: {
                            'Authorization': `Bearer ${result.token}`,
                            'Content-Type': 'application/json',
                        },
                    });
                }


                

                if (userResponse.ok) {
                    const user = await userResponse.json();
                    console.log("User Details:", user);

                    // Dispatch the user details to Redux
                    dispatch(setUserDetails({ token: result.token, user, userRole }));

                    toast.success("Login successful!");
                    
                    // Redirect to the desired page, e.g., dashboard
                    if(userRole === "User")
                    {
                        navigate("/");
                    } else {
                        navigate("/admin-panel")
                    }
                    
                } else {
                    toast.error("Failed to retrieve user details");
                }
            } else {
                toast.error("Failed to log in")
                setError("Invalid email or password. Please try again.");
            }
        } catch (error) {
            console.error("Error during authentication", error);
            setError("Something went wrong. Please try again.");
        }
    };

    return (
        <section id='login'>
            <div className='mx-auto container p-4'>
                <div className='bg-white p-5 w-full max-w-sm mx-auto shadow-lg rounded-3xl block mt-16 mb-8'>
                    <div className='text-center font-semibold text-4xl'>Welcome back!</div>
                    <div className='text-center'>Enter your details to get in!</div>
                    <form className='pt-6 flex flex-col gap-4' onSubmit={handleSubmit}>
                        <div className='grid'>
                            <label className='mb-2'>Username :</label>
                            <div className='border py-2 px-4 rounded-xl'>
                                <input
                                    type='text'
                                    placeholder='Enter username'
                                    name='username'
                                    value={data.username}
                                    onChange={handleOnChange}
                                    className='w-full h-full outline-none bg-transparent'
                                    required
                                />
                            </div>
                        </div>

                        <div className='grid'>
                            <label className='mb-2'>Password :</label>
                            <div className='border py-2.5 px-4 rounded-xl flex justify-center items-center'>
                                <input
                                    type={showPassword ? 'text' : 'password'}
                                    placeholder='Password'
                                    name='password'
                                    value={data.password}
                                    onChange={handleOnChange}
                                    className='w-full h-full outline-none bg-transparent'
                                    required
                                />
                                <div className='cursor-pointer text-xl' onClick={() => setShowPassword(prev => !prev)}>
                                    {showPassword ? <FaRegEye /> : <FaRegEyeSlash />}
                                </div>
                            </div>
                        </div>

                        <button className='bg-orange-300 text-white px-6 py-2 w-full max-w-[150px] rounded-full hover:bg-orange-400 mx-auto mt-6 transition-all'>
                            Login
                        </button>
                        {error && <p className='text-red-500 mt-4'>{error}</p>}
                    </form>

                    <p className='my-8 text-center'>
                        Don't have an account? <Link to={"/sign-up"} className='text-orange-400 font-semibold'>SignUp</Link>
                    </p>
                </div>
            </div>
        </section>
    );
};

export default Login;
