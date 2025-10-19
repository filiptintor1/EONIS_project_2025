import React from 'react';
import mainImage from "../assets/honeyMainImage.png";
import HoneyMainCard from '../components/HoneyMainCard';
import ShopNowButton from '../components/ShopNowButton';

const Home = () => {
  return (
    <section className='relative'>
      <div className='container max-w-5xl min-h-screen flex justify-center items-center mx-auto'>
        {/** Title, CTA */}
        <div className='flex flex-col justify-center items-start md:items-start gap-3' style={{ marginTop: '-10%' }}>
          <p className='text-gray-600 mb-4'>Pure, Natural Honey Straight from the Hive</p>
          <h1 className='text-7xl font-bold mb-4'>RAW HONEY</h1>
          <p className='text-gray-400 mb-12'>Discover the rich flavors and health benefits of our premium honey, sourced directly from the hive to your table. Our honey is 100% natural, free from additives, and harvested with care to ensure its purity and quality.</p>
          <ShopNowButton />
        </div>

        {/** Main Image */}
        <div className='hidden md:block'>
          <img 
            src={mainImage} 
            className='w-[1400px] md:w-[1400px] h-auto object-cover' 
            alt="Honey jar" 
            style={{ marginTop: '-15%' }}
          />
        </div>
      </div>

      {/** Cards section (initially hidden below the fold) */}
      <div className='overflow-hidden'>
        <HoneyMainCard />
      </div>
    </section>
  );
};

export default Home;
