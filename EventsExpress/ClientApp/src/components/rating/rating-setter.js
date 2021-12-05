import React from "react";
import Rating from "react-rating";

export default function RatingSetter(props) {
  return (
    <div>
      Your rate:
      <Rating
        initialRating={Number(props.myRate)}
        stop={10}
        step={2}
        emptySymbol="fa fa-star-o fa-2x medium"
        fullSymbol="fa fa-star fa-2x medium"
        onChange={props.callback}
      />
    </div>
  );
}
