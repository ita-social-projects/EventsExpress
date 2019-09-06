import React from 'react'
import IconButton from "@material-ui/core/IconButton";


export default function RatingAverage(props) {
    
    const textColor = (props.value < 5) 
        ? 'text-danger' 
        : (props.value < 8) 
            ? 'text-warning'
            : 'text-success';

    const direction = (props.direction == 'row')
        ? 'flex-row'
        : 'flex-column';

    return (props.value) 
        ? <div className={direction + ' d-flex align-items-center'} >
            <IconButton className={textColor} size="small" disabled>
                <i className="far fa-star"></i> 
            </IconButton>
            <div className={textColor}>{props.value}</div>
        </div>
        : null
    
    
}