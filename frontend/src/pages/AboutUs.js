import React from 'react'
import abouUsImage from "../assets/mainAboutusImage.png"
import { MdHexagon } from 'react-icons/md'
import { FaArrowRightLong } from 'react-icons/fa6'
import { Link } from 'react-router-dom'
import ShopNowButton from '../components/ShopNowButton'

const AboutUs = () => {
  return (
    <div className='container mx-auto flex md:flex-row flex-col justify-center items-center h-full mt-20 gap-20'>
        <div className='hidden md:block'>
          <img src={abouUsImage} alt="AboutUs Image" width={1444} height={1444}/>
        </div>
        <div className='flex flex-col gap-4'>
            <h2 className='text-4xl font-semibold w-fit border-b-2 border-orange-300 py-2 mb-4'>ABOUT US</h2>
            <div>
            Welcome to HoneyStore, where we bring you the purest, most natural honey straight from the hive to your home. Our passion for beekeeping and dedication to sustainability ensure that every jar of honey we produce is rich in flavor and full of natural goodness.
            </div>
            <div>
            We believe in the power of nature, and our bees are at the heart of everything we do. Our honey is harvested with care, using traditional methods that preserve the quality and taste. Whether you're enjoying our golden honey as a sweetener, spread, or in one of our unique honey-infused products, you can trust that it’s made with love and respect for the environment.
            </div>

            <div>
            At HoneyStore, we also offer a range of honey-based products, including beeswax candles, honey skincare, and more. Each item is crafted to bring a little bit of nature’s magic into your everyday life.
            </div>

            <ShopNowButton />
        </div>
    </div>
  )
}

export default AboutUs