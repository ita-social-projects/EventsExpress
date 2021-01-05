import React from "react";
import PropTypes from "prop-types";
import { MdCloudUpload } from "react-icons/md";

const Placeholder = ({ getInputProps, getRootProps, error, touched }) => (
  <div
    {...getRootProps()}
    // style={{border: 'solid 1px grey'}}
    className={`placeholder-preview ${error && touched ? "has-error" : ""}`}
  >
    <input {...getInputProps()} />
    <MdCloudUpload style={{ fontSize: 100, paddingTop: 85 }} />
    <span>Click or drag image file to this area to upload.</span>
  </div>
);

Placeholder.propTypes = {
  error: PropTypes.string,
  getInputProps: PropTypes.func.isRequired,
  getRootProps: PropTypes.func.isRequired,
  touched: PropTypes.bool
};

export default Placeholder;
