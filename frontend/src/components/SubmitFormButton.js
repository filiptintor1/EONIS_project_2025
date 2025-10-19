import React from 'react'

const SubmitFormButton = ({
    title,
}) => {
  return (
    <div className='w-full flex items-center justify-center mt-3'>
        <button className='py-3 px-10 bg-orange-300 hover:bg-orange-500 text-white font-semibold rounded-full'>
            {title}
        </button>
    </div>
  )
}

export default SubmitFormButton