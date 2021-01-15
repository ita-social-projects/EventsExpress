import React, { Component } from 'react';
import ReactDOM from 'react-dom'
import Slider from '@material-ui/core/Slider'
import Cropper from 'react-easy-crop'
import './image-resizer.css';
import Button from "@material-ui/core/Button";

class ImageResizer extends Component {
    state = {
        crop: { x: 0, y: 0 },
        zoom: 1
    }

    componentWillMount = () => {
        const isRound = this.props.cropShape === 'round';
        this.setState({
            aspect: isRound ? 1 : (16 / 9),
            showGrid: isRound ? false : true
        });
    }

    onCropChange = crop => {
        this.setState({ crop })
    }

    onCropComplete = (croppedArea, croppedAreaPixels) => {
        this.setState({ croppedAreaPixels: croppedAreaPixels })
    }

    onZoomChange = zoom => {
        this.setState({ zoom })
    }

    createImage = url =>
        new Promise((resolve, reject) => {
            const image = new Image()
            image.addEventListener('load', () => resolve(image))
            image.addEventListener('error', (error) => reject(error))
            image.src = url
        })

    getCroppedImg = async (imageSrc, pixelCrop) => {
        const image = await this.createImage(imageSrc)
        const canvas = document.createElement('canvas')
        const ctx = canvas.getContext('2d')

        const maxSize = Math.max(image.width, image.height)
        const safeArea = 2 * ((maxSize / 2) * Math.sqrt(2))

        canvas.width = safeArea
        canvas.height = safeArea

        ctx.translate(safeArea / 2, safeArea / 2)
        ctx.translate(-safeArea / 2, -safeArea / 2)

        ctx.drawImage(
            image,
            safeArea / 2 - image.width * 0.5,
            safeArea / 2 - image.height * 0.5
        )
        const data = ctx.getImageData(0, 0, safeArea, safeArea)

        canvas.width = pixelCrop.width
        canvas.height = pixelCrop.height

        ctx.putImageData(
            data,
            Math.round(0 - safeArea / 2 + image.width * 0.5 - pixelCrop.x),
            Math.round(0 - safeArea / 2 + image.height * 0.5 - pixelCrop.y)
        )

        return canvas.toDataURL('image/jpeg');
    }

    cropImage = async () => {
        const croppedImage = await this.getCroppedImg(
            this.props.image.preview,
            this.state.croppedAreaPixels
        )
        this.props.handleOnCrop([croppedImage], this.props.onChange);
        this.props.onImageCrop();
    }

    render() {
        const { cropShape } = this.props;
        const { showGrid } = this.state;

        return (
            <div>
                <div className="ImageResizer">
                    <div className="crop-container">
                        <Cropper
                            image={this.props.image.preview}
                            crop={this.state.crop}
                            zoom={this.state.zoom}
                            aspect={this.state.aspect}
                            onCropChange={this.onCropChange}
                            onCropComplete={this.onCropComplete}
                            onZoomChange={this.onZoomChange}
                            cropShape={cropShape}
                            showGrid={showGrid}
                        />
                    </div>
                    <div className="controls">
                        <Slider
                            value={this.state.zoom}
                            min={1}
                            max={3}
                            step={0.1}
                            aria-labelledby="Zoom"
                            onChange={(e, zoom) => this.onZoomChange(zoom)}
                        />
                        <Button
                            type="button"
                            color="primary"
                            disabled={this.props.submitting}
                            onClick={this.cropImage}
                            style={{ float: "right" }}
                        >
                            Crop
                        </Button>
                    </div>
                </div>
            </div>
        )
    }
}

export default ImageResizer;