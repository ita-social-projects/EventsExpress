import React from "react";
import PropTypes from "prop-types";
import '../event/event-form.css';

const ImagePreview = ({ imagefile, shape }) =>
    imagefile.map(({ name, preview }) => (
    <div key={name} className="render-preview">
        <div className="image-container d-flex align-items-center justify-content-center">
            <img 
                src={preview} 
                alt={name} 
                className={`pic pic-container${shape === 'round' ? " pic-large pic-circle" : ""}`} 
            />
        </div>
    </div>
  ));

ImagePreview.propTypes = {
    imagefile: PropTypes.arrayOf(
        PropTypes.shape({
            file: PropTypes.file,
            name: PropTypes.string,
            preview: PropTypes.string
        })
    )
};

export default ImagePreview;
