import React from "react";

export default function HeaderButton({children, onClick}) {

  return (
    <button
      className="headbtn btn btn-light navbtns"
      variant="contained"
      onClick={onClick}
    >
      {children}
    </button>
  );
}
