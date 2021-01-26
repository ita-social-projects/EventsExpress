import React from "react";
import PropTypes from "prop-types";
import { MdCloudUpload } from "react-icons/md";

const Placeholder = ({ getInputProps, getRootProps, error, touched }) => {
    const iconStyle = {
        fontSize: 100,
        paddingTop: 85
    }
    const spanStyle = {

    }
    if (error && touched) {
        iconStyle.color = "red";
        spanStyle.color = "red";
    }

    return (
        <div
            {...getRootProps()}
            className={`placeholder-preview ${error && touched ? "has-error" : ""}`}
        >
            <input {...getInputProps()} />
            <MdCloudUpload style={iconStyle} />
            <span style={spanStyle}>Click or drag image file to this area to upload.</span>
        </div>
    );
}

Placeholder.propTypes = {
    error: PropTypes.string,
    getInputProps: PropTypes.func.isRequired,
    getRootProps: PropTypes.func.isRequired,
    touched: PropTypes.bool
};

export default Placeholder;
