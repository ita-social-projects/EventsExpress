import React, {useState} from 'react';
import Tile from '../tile/Tile';
import './TileWrapper.css';

const TileWrapper = (props) => {

  const handleTileToggleAction = (status) => {
    props.handleTileToggleAction(status, props.index);
  }

  return (
    <div className="tile-wrapper">
      <Tile categories={props.value.categories}
            handleTileToggleAction={handleTileToggleAction}
            // triStateStatus={props.triStateStatus}
          />
      <p className="tile-label">{props.value.group.title}</p>
    </div>
  )
}

export default TileWrapper;
