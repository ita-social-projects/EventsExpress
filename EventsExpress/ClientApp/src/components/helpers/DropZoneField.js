import React, { Component } from "react";
import PropTypes from "prop-types";
import DropZone from "react-dropzone";
import ImagePreview from "./ImagePreview";
import Placeholder from "./Placeholder";
import ShowError from "./ShowError";
import ImageResizer from '../event/image-resizer';

export default class DropZoneField extends Component {

    state = {
        cropped: false
    };

    imageCrop = () => {
        this.setState({ cropped: true });
    };

    render() {
        const {
            handleOnDrop,
            handleOnCrop,
            imagefile,
            crop,
            meta: { error, touched },
            input: { onChange }
        } = this.props;

        return (
            <div className="preview-container">
                {imagefile.length && crop && !this.state.cropped ? (
                    <div>
                        <ImageResizer
                            image={imagefile[0]}
                            onChange={onChange}
                            handleOnDrop={handleOnDrop}
                            handleOnCrop={handleOnCrop}
                            onImageCrop={this.imageCrop}
                        />
                    </div>
                ) : (
                        <div>
                            <DropZone
                                accept="image/jpeg, image/png, image/gif, image/bmp"
                                className="upload-container"
                                onDrop={file => handleOnDrop(file, onChange)}
                                multiple={false}
                            >
                                {props =>
                                    imagefile && imagefile.length > 0 ? (
                                        <ImagePreview imagefile={imagefile} />
                                    ) : (
                                            <Placeholder {...props} error={error} touched={touched} />
                                        )
                                }
                            </DropZone>
                            <ShowError error={error} touched={touched} />
                        </div>
                    )}
            </div>
        )
    };
};

DropZoneField.propTypes = {
  error: PropTypes.string,
  handleOnDrop: PropTypes.func.isRequired,
  imagefile: PropTypes.arrayOf(
    PropTypes.shape({
      file: PropTypes.file,
      name: PropTypes.string,
      preview: PropTypes.string,
      size: PropTypes.number
    })
  ),
  onChange: PropTypes.func,
  touched: PropTypes.bool
};