import React from "react";
import "./Line.css";

const Line = (props) => {
  const definePath = () => {
    const param = props.index * 40;

    const start = `M 0 3`;
    const second = `L ${param + 18} 3`;
    const third = `L ${param + 20} 1`;
    const fourth = `L ${param + 22} 3`;

    return `${start} ${second} ${third} ${fourth} L 120 3`;
  };

  return (
    <svg className="line" viewBox="0 0 120 5" xmlns="http//www.w3.org/2000/svg">
      <path d={definePath()} strokeLinecap="round" />
    </svg>
  );
};

export default Line;
