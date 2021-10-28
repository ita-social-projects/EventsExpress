import React from 'react';
import Line from './Line';
import Checkbox from '@material-ui/core/Checkbox';
import { FormControlLabel, Typography } from '@material-ui/core';
import '../css/CheckboxList.css'

const CheckboxList = (props) => {
    return (
        <div>
            <Line index={props.index} />
            <h2>Choose any hobbies from list:</h2>
            <div className="checkbox-group">
                {props.data.map(el =>
                    <FormControlLabel
                        control={<Checkbox color="primary"
                                           checked={props.allChecked}
                                />}
                        label={<Typography variant="h6">{el}
                               </Typography>} />
                )}
            </div>
        </div>
    )
}

export default CheckboxList;