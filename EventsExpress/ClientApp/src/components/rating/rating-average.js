import React from 'react'
import IconButton from "@material-ui/core/IconButton";


export default function RatingAverage(props) {
    
    let textColor = (props.value < 5) 
        ? 'text-danger' 
        : (props.value < 8) 
            ? 'text-warning'
            : 'text-success'

    return <div className='d-flex flex-row align-items-center'>
        <div className='h5'>
            Rating:
        </div>
        <div className='d-flex flex-column align-items-center ml-2'>
            <IconButton className={textColor} size="small" disabled>
                <i class="far fa-star"></i> 
            </IconButton>
            <div className={textColor}>
                {props.value ? props.value : "-"}
            </div>
        </div>
    </div>
}