import React from 'react'
import productCategory from '../helpers/productCategory'
import { Link } from 'react-router-dom'


const CategoryList = () => {
    return (
      <div className='container mx-auto p-4'>
          <div className='flex items-start gap-12 justify-center overflow-scroll scrollbar-none'>
              {
                  productCategory.map((el, i) => (
                      <Link
                          to={"/product-category?category=" + el?.value}
                          className="cursor-pointer flex flex-col justify-center items-center max-w-20"
                          key={el?.id}
                      >
                          <div className='w-16 h-16 md:w-20 md:h-20 rounded-full overflow-hidden p-4 bg-slate-400 flex items-center justify-center'>
                              {/* <img src="" alt="" /> */}
                          </div>
                          <p className='text-center text-sm md:text-base capitalize'>
                              {el?.label}
                          </p>
                      </Link>
                  ))
              }
          </div>
      </div>
    )
  }
  

export default CategoryList