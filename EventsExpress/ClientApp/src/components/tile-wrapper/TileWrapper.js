import React from "react";
import Tile from "../tile/Tile";
import "./TileWrapper.css";

const TileWrapper = (props) => {
  const handleTileToggleAction = () => {
    props.handleTileToggleAction(props.index);
  };

  return (
    <div className="tile-wrapper">
      <Tile
        groupId={props.value.group.id}
        categories={props.value.categories}
        handleTileToggleAction={handleTileToggleAction}
      />
      <p className="tile-label">{props.value.group.title}</p>
    </div>
  );
};

export default TileWrapper;
