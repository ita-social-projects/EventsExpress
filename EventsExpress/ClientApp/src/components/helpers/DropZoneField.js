import React, { Component, Fragment } from "react";
import PropTypes from "prop-types";
import DropZone from "react-dropzone";
import ImagePreview from "./ImagePreview";
import Placeholder from "./Placeholder";
import ImageResizer from '../event/image-resizer';
import Button from "@material-ui/core/Button";

export default class DropZoneField extends Component {

    state = {
        imagefile: [],
        cropped: false
    };

    componentWillMount = () => {
        if (this.props.photoUrl) {
            const imagefile = {
                file: '',
                name: '',
                preview: this.props.photoUrl,
                size: 1
            };
            this.setState({ imagefile: [imagefile] });
        }
    }

    handleOnClear = () => {
        this.setState({ cropped: false, imagefile: [] });
        this.props.input.onChange(null);
    }

    handleOnDrop = (file) => {
        if (file.length > 0) {
            const imagefile = {
                file: file[0],
                name: file[0].name,
                preview: URL.createObjectURL(file[0]),
                size: 1
            };
            this.setState({ imagefile: [imagefile] });
        }
    }

    handleOnCrop = (croppedImage) => {
        const file = new File([croppedImage], "image.jpg", { type: "image/jpeg" });
        const imagefile = {
            file: file,
            name: "image.jpg",
            preview: croppedImage,
            size: 1
        };
        this.setState({ imagefile: [imagefile], cropped: true },
            () => this.props.input.onChange(imagefile));
    }

    render() {
        const {
            submitting,
            crop,
            cropShape,
            meta: { error, touched },
            input: { onChange }
        } = this.props;
        const { imagefile, cropped } = this.state;
        const { handleOnCrop, handleOnDrop, handleOnClear } = this;
        const containerClass = error && touched ? "invalid" : "valid";

        return (
            <Fragment>
                <div className={`preview-container ${containerClass}`}>
                    {imagefile.length && crop && !cropped ? (
                        <div>
                            <ImageResizer
                                image={imagefile[0]}
                                onChange={onChange}
                                handleOnCrop={handleOnCrop}
                                cropShape={cropShape}
                            />
                        </div>
                    ) : (
                            <div>
                                <DropZone
                                    accept="image/jpeg, image/png, image/gif, image/bmp"
                                    className="upload-container"
                                    onDrop={file => handleOnDrop(file)}
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
                            </div>
                        )}
                    <Button
                        className="mt-3"
                        type="button"
                        color="primary"
                        disabled={submitting}
                        onClick={handleOnClear}
                        style={{ float: "right" }}
                    >
                        Clear
                </Button>
                </div>
                {error && touched ? (
                    <span className="error-text">
                        {error}
                    </span>
                ) : null}
            </Fragment>
        )
    }
}

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