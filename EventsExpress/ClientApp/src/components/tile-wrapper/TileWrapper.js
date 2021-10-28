import React, {useState} from 'react';
import Tile from '../tile/Tile';
import './TileWrapper.css';

const TileWrapper = (props) => {
  const [isTileActive, setTileActive] = useState(false);

  const handleStatusChange = (status) => {
    props.handleStatusChange(status, props.index);
  }

  const handleCheckboxChange = (checked) => {
    props.handleCheckboxChange(checked);
  }

  return (
    <div className="tile-wrapper">
      <Tile handleStatusChange={handleStatusChange}
            handleCheckboxChange={handleCheckboxChange}/>
      <p>{props.title}</p>
    </div>
  )
}

export default TileWrapper;
