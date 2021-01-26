import React from "react";
import PropTypes from "prop-types";
import { MdInfoOutline } from "react-icons/md";

//component is not used anywhere
const ShowError = ({ error, touched }) =>
  touched && error ? (
    <div className="error">
      <MdInfoOutline
        style={{ position: "relative", top: -2, marginRight: 2, color: "red" }}
      />
      <span className="error-text">
        {error}
      </span>
    </div>
  ) : null;

ShowError.propTypes = {
  error: PropTypes.string,
  touched: PropTypes.bool
};

export default ShowError;
