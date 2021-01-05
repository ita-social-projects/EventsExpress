import React from "react";
import PropTypes from "prop-types";
import { MdInfoOutline } from "react-icons/md";

const ShowError = ({ error, touched }) =>
  touched && error ? (
    <div className="error">
      <MdInfoOutline
        style={{ position: "relative", top: -2, marginRight: 2 }}
      />
      {error}
    </div>
  ) : null;

ShowError.propTypes = {
  error: PropTypes.string,
  touched: PropTypes.bool
};

export default ShowError;
