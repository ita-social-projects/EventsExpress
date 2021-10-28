import React, { useState } from 'react';
import RadioButtonUnchecked from '@material-ui/icons/RadioButtonUnchecked';
import CheckCircle from '@material-ui/icons/CheckCircle';
import Checkbox from '@material-ui/core/Checkbox';
import './Tile.css';

const Tile = (props) => {
    const [isActive, setActive] = useState(false);

    return (
        <div className={`tile ${isActive ? "active" : ""}`}
             onClick={() => {
                setActive(!isActive);
                props.handleStatusChange(!isActive);
             }
           }
        >
            <Checkbox icon={<RadioButtonUnchecked />}
                      checkedIcon={<CheckCircle htmlColor="#018307" />}
                      onChange={(e) =>
                          props.handleCheckboxChange(e.target.checked)}
            />
        </div>
    );
}

export default Tile;