import React, { useState } from 'react'
import { FaRegEye, FaRegEyeSlash } from "react-icons/fa";
import { Link, useNavigate } from 'react-router-dom';

const SignUp = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [data, setData] = useState({
    name: "",
    surname: "",
    username: "",
    password: "",
    confPassword: ""
  });

  const navigate = useNavigate();

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
      const response = await fetch('https://localhost:7123/users', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          name: data.name,
          surname: data.surname,
          username: data.username,
          password: data.password
        })
      });

      //console.log(response)

      if (response.ok) {
        navigate('/login');
      } else {
        console.error("Signup failed");
      }
    } catch (error) {
      console.error("Error occurred during signup:", error);
    }
  };

  return (
    <section id='login'>
      <div className='mx-auto container p-4'>
        <div className='bg-white p-5 w-full max-w-sm mx-auto shadow-lg rounded-3xl block mt-8 mb-8'>
          <div className='text-center font-semibold text-4xl'>Create Account!</div>
          <div className='text-center'>Enter your details to get in!</div>
          <form className='pt-6 flex flex-col gap-4' onSubmit={handleSubmit}>
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

            {/* Username input */}
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
                />
              </div>
            </div>

            {/* Password input */}
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
                />
                <div className='cursor-pointer text-xl' onClick={() => setShowPassword((prev) => !prev)}>
                  <span>{showPassword ? <FaRegEye /> : <FaRegEyeSlash />}</span>
                </div>
              </div>
            </div>

            {/* Confirm password input */}
            <div className='grid'>
              <label className='mb-2'>Confirm Password :</label>
              <div className='border py-2.5 px-4 rounded-xl flex justify-center items-center'>
                <input
                  type={showPassword ? 'text' : 'password'}
                  placeholder='Confirm Password'
                  name='confPassword'
                  value={data.confPassword}
                  onChange={handleOnChange}
                  className='w-full h-full outline-none bg-transparent'
                />
                <div className='cursor-pointer text-xl' onClick={() => setShowPassword((prev) => !prev)}>
                  <span>{showPassword ? <FaRegEye /> : <FaRegEyeSlash />}</span>
                </div>
              </div>
            </div>

            <button className='bg-orange-300 text-white px-6 py-2 w-full max-w-[150px] rounded-full hover:bg-orange-400 mx-auto mt-6 transition-all'>
              Register
            </button>
          </form>

          <p className='my-8 text-center'>
            Already have an account? <Link to={"/login"} className='text-orange-400 font-semibold'>Login</Link>
          </p>
        </div>
      </div>
    </section>
  );
};

export default SignUp;
