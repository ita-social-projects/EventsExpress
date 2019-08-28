import React from 'react'
import Rating from '@material-ui/lab/Rating';


export default function RatingSetter(props) {    
    
    return <div>
        Your rate: 
        <Rating 
            value={Number(props.myRate)}
            max={10}
            size="large"
            onChange={props.callback}
        />
    </div>
}