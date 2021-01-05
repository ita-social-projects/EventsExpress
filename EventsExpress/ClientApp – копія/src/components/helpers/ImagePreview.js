import React from "react";
import PropTypes from "prop-types";
import '../event/event-form.css';

const ImagePreview = ({ imagefile }) =>
  imagefile.map(({ name, preview, size }) => (
    <div key={name} className="render-preview">
      <div className="image-container">
        {/* <div className="pic-container pic-medium pic-circle"> */}
        <img src={preview} alt={name} className="pic" />
        {/* </div> */}
      </div>
    </div>
  ));

ImagePreview.propTypes = {
  imagefile: PropTypes.arrayOf(
    PropTypes.shape({
      file: PropTypes.file,
      name: PropTypes.string,
      preview: PropTypes.string,
      size: PropTypes.number
    })
  )
};

export default ImagePreview;
