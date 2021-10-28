import React, { useState } from 'react';
import { CheckboxList } from '../checkbox-list/CheckboxList';
import TileWrapper from '../../components/tile-wrapper/TileWrapper';
import '../css/TileRow.css';

const array = [
  "Option 1",
  "Option 2",
  "Option 3",
  "Option 4",
  "Option 5",
  "Option 6",
  "Option 7",
  "Option 8",
];

const TileRow = (props) => {
  const [currTileIndex, setCurrTileIndex] = useState(-1);
  const [isAnyTileActive, setTileActive] = useState(false);
  const [isActiveCheckboxChecked, 
    toggleActiveCheckbox] = useState(false);

  const handleStatusChange = (status, index) => {
    setTileActive(status);
    setCurrTileIndex(index);
  }

  const handleCheckboxChange = (checked) => {
    toggleActiveCheckbox(checked);
  }

  return (
    <div className="tile-row-wrapper">
      <div className="tile-row">
        {props.data.map((item, idx) => 
          <TileWrapper index={idx}
                       handleStatusChange={handleStatusChange}
                       handleCheckboxChange={handleCheckboxChange}
                       key={item.id} 
                       title={item.title}/>)
        }        
      </div>
      { isAnyTileActive && 
        <CheckboxList index={currTileIndex}
                      data={array}
                      allChecked={isActiveCheckboxChecked} />
      }
    </div>
  );
}

export default TileRow;
