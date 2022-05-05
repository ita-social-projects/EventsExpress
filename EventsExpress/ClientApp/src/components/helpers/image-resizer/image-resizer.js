import React, { Component } from "react";
import Slider from "@material-ui/core/Slider";
import Cropper from "react-easy-crop";
import "./image-resizer.css";
import Button from "@material-ui/core/Button";

class ImageResizer extends Component {
  state = {
    crop: { x: 0, y: 0 },
    zoom: 1,
    maxZoom: 3,
  };

  componentWillMount = () => {
    const isRound = this.props.cropShape === "round";
    this.setState({
      aspect: isRound ? 1 : 16 / 9,
      showGrid: isRound ? false : true,
    });
    this.getMaxZoom(this.props.image.preview);
  };

  onCropChange = (crop) => {
    this.setState({ crop });
  };

  onCropComplete = (croppedArea, croppedAreaPixels) => {
    this.setState({ croppedAreaPixels: croppedAreaPixels });
  };

  onZoomChange = (zoom) => {
    this.setState({ zoom });
  };

  createImage = (url) =>
    new Promise((resolve, reject) => {
      const image = new Image();
      image.addEventListener("load", () => resolve(image));
      image.addEventListener("error", (error) => reject(error));
      image.src = url;
    });

  getCroppedImg = async (imageSrc, pixelCrop) => {
    const image = await this.createImage(imageSrc);
    const res = this.props.computeResizedPhoto(
      image,
      pixelCrop,
      this.state.maxZoom,
      this.state.zoom
    );
    return res;
  };

  cropImage = async () => {
    const croppedImage = await this.getCroppedImg(
      this.props.image.preview,
      this.state.croppedAreaPixels
    );
    this.props.handleOnCrop([croppedImage], this.props.onChange);
  };

  getMaxZoom = async (url) => {
    let maxZoom = 3;
    const image = await this.createImage(url);
    const { getCompressionRatio } = this.props;

    if (getCompressionRatio) {
      let result = getCompressionRatio(image, ".crop-container");
      maxZoom = result > 1 ? result : 1;
    }
    this.setState({ maxZoom: maxZoom });
  };

  render() {
    const { cropShape, cropSize } = this.props;
    const { showGrid, crop, aspect, maxZoom } = this.state;
    const { onCropChange, onCropComplete, onZoomChange, cropImage } = this;

    return (
      <div>
        <div className="ImageResizer">
          <div className="crop-container">
            <Cropper
              image={this.props.image.preview}
              crop={crop}
              zoom={this.state.zoom}
              aspect={aspect}
              onCropChange={onCropChange}
              onCropComplete={onCropComplete}
              onZoomChange={onZoomChange}
              cropShape={cropShape}
              showGrid={showGrid}
              cropSize={cropSize}
              minZoom={1}
              maxZoom={maxZoom}
            />
          </div>
          <div className="controls">
            <Slider
              value={this.state.zoom}
              min={1}
              max={maxZoom}
              step={0.1}
              aria-labelledby="Zoom"
              onChange={(e, zoom) => onZoomChange(zoom)}
            />
            <Button
              type="button"
              color="primary"
              disabled={this.props.submitting}
              onClick={cropImage}
              style={{ float: "right" }}
            >
              Crop
            </Button>
          </div>
        </div>
      </div>
    );
  }
}

export default ImageResizer;
