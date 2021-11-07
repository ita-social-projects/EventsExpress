import React, { useState } from 'react';
import CheckboxList from './CheckboxList';
import TileWrapper from '../../components/tile-wrapper/TileWrapper';
import './TileRow.css';

const TileRow = (props) => {
  const [currTileIndex, setCurrTileIndex] = useState(-1);
  const [isAnyTileActive, setTileActive] = useState(false);

  const handleTileToggleAction = (status, index) => {
    if (isAnyTileActive && index == currTileIndex)
      setTileActive(false);
    else
      setTileActive(status);
    setCurrTileIndex(index);
  }

  return (
    <div className="tile-row-wrapper">
      <div className="tile-row">
        {props.data.map((item, idx) =>
          <TileWrapper 
              index={idx}
              handleTileToggleAction={handleTileToggleAction}
              key={item.group.id}
              value={item} />)
        }
      </div>
        { isAnyTileActive && 
          <CheckboxList index={currTileIndex}
                        data={props.data[currTileIndex].categories}
                    />
        }
    </div>
  );
}

export default TileRow;
