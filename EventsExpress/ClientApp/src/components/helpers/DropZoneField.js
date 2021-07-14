import React, { Component, Fragment } from "react";
import PropTypes from "prop-types";
import DropZone from "react-dropzone";
import ImagePreview from "./ImagePreview";
import Placeholder from "./Placeholder";
import ImageResizer from '../event/image-resizer';
import Button from "@material-ui/core/Button";
import renderFieldError from './form-helpers/render-field-error';


export default class DropZoneField extends Component {

    state = {
        imagefile: [],
        cropped: false,
        errors: null
    };

    componentDidMount() {
        this.props.loadImage().then(
            image => {
                if (image != null) {
                    const imagefile = {
                        file: '',
                        name: '',
                        preview: URL.createObjectURL(image)
                    };
                    this.setState({ imagefile: [imagefile] });
                }
            }
        )
    }

    componentWillUnmount() {
        this.revokeImageUrl();
    }

    handleOnClear = () => {
        this.revokeImageUrl();
        this.setState({ cropped: false, imagefile: [] , errors : null });
        this.props.input.onChange(null);
    }

    revokeImageUrl = () => {
        if(this.state.imagefile[0] != undefined && !this.state.cropped) {
            URL.revokeObjectURL(this.state.imagefile[0].preview);
        }
    }

    handleOnDrop = (file) => {
        if (file.length > 0) {
            const imagefile = {
                file: file[0],
                name: file[0].name,
                preview: URL.createObjectURL(file[0])
            };
            this.setState({ imagefile: [imagefile] });
        }
    }

    handleOnCrop = async (croppedImage) => {
        let err;
        URL.revokeObjectURL(this.state.imagefile[0].preview);
        const file = new File(croppedImage, "image.jpg", { type: "image/jpeg" });
        const imagefile = {
            file: file,
            name: "image.jpg",
            preview: croppedImage[0]
        };
        if (this.props.uploadImage !== undefined && typeof (this.props.uploadImage) === `function` ) {
            let response = await this.props.uploadImage(file);

            if (!response.ok) {
                err = await response.json();
                err = err.errors[`Photo`];
            }
            
        }

        this.setState({ imagefile: [imagefile], cropped: true, errors: err },
            () => this.props.input.onChange(imagefile));
    }

    render() {
        const {
            submitting,
            crop,
            cropShape,
            meta: {  touched },
            input: { onChange }
        } = this.props;
        const { errors, imagefile, cropped } = this.state;
        const { handleOnCrop, handleOnDrop, handleOnClear } = this;
        const containerClass = errors && touched ? "invalid" : "valid";
        return (
            <Fragment className={`preview-container ${containerClass}`}>
                <div >  
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
                                            <ImagePreview imagefile={imagefile} shape={cropShape} error={errors} touched={touched} />
                                        ) : (

                                                <Placeholder {...props} error={errors} touched={touched} />
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
                {renderFieldError({ touched, errors})}
            </Fragment>
        )
    }
}

DropZoneField.propTypes = {
    imagefile: PropTypes.arrayOf(
        PropTypes.shape({
            file: PropTypes.file,
            name: PropTypes.string,
            preview: PropTypes.string
        })
    ),
    onChange: PropTypes.func,
    touched: PropTypes.bool
};