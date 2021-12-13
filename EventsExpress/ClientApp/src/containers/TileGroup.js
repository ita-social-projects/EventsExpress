import React from "react";
import SpinnerWrapper from "./spinner";
import TileRow from "./tile-row/TileRow";

export const TileGroup = (props) => {
  const renderRows = (data) => {
    let rows = [];
    for (let i = 0; i < data.length; i += 3) {
      rows.push(<TileRow key={i} data={data.slice(i, i + 3)} />);
    }

    return rows;
  };

  return (
    <div className="tile-group">
      <SpinnerWrapper showContent={props.data !== null}>
        {renderRows(props.data)}
      </SpinnerWrapper>
    </div>
  );
};

export default TileGroup;
