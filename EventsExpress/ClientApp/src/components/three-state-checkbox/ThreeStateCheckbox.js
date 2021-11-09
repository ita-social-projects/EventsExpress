import React, { useRef, useEffect } from "react";
import "./ThreeStateCheckbox.css";

const updateInput = (ref, checked) => {
  const input = ref.current;
  if (input) {
    input.checked = checked;
    input.indeterminate = checked == null;
  }
};

const ThreeStateCheckbox = ({ name, checked, onChange }) => {
  const inputRef = useRef(null);
  const checkedRef = useRef(checked);

  useEffect(() => {
    checkedRef.current = checked;
    updateInput(inputRef, checked);
  }, [checked]);

  const handleClick = (event) => {
    event.stopPropagation();
    switch (checkedRef.current) {
      case true:
        checkedRef.current = false;
        break;
      case false:
        checkedRef.current = true;
        break;
      default:
        // null
        checkedRef.current = true;
        break;
    }
    updateInput(inputRef, checkedRef.current);
    if (onChange) {
      onChange(checkedRef.current);
    }
  };

  return (
    <input
      className="tri-state-checkbox"
      ref={inputRef}
      type="checkbox"
      name={name}
      onClick={(evt) => handleClick(evt)}
    />
  );
};

export default ThreeStateCheckbox;
