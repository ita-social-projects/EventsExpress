import React, { Component } from "react";
import PropTypes from "prop-types";
import DropZone from "react-dropzone";
import ImagePreview from "./ImagePreview";
import Placeholder from "./Placeholder";
import ShowError from "./ShowError";
import ImageResizer from '../event/image-resizer';
import Button from "@material-ui/core/Button";

export default class DropZoneField extends Component {

    state = {
        cropped: false
    };

    imageCrop = () => {
        this.setState({ cropped: true });
    };

    clearImage = () => {
        this.setState({ cropped: false });
        this.props.handleOnClear();
    };

    render() {
        const {
            handleOnDrop,
            handleOnCrop,
            submitting,
            imagefile,
            crop,
            cropShape,
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
                            handleOnCrop={handleOnCrop}
                            onImageCrop={this.imageCrop}
                            cropShape={cropShape}
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
                <Button
                    className="mt-3"
                    type="button"
                    color="primary"
                    disabled={submitting}
                    onClick={this.clearImage}
                    style={{ float: "right" }}
                >
                    Clear
                </Button>
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