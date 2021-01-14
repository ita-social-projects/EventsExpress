import React, { Component } from 'react';
import ReactDOM from 'react-dom'
import Slider from '@material-ui/core/Slider'
import Cropper from 'react-easy-crop'
import './image-resizer.css';
import Button from "@material-ui/core/Button";

class ImageResizer extends Component {
    state = {        
        crop: { x: 0, y: 0 },
        zoom: 1,
        aspect: 16 / 9
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

    async getCroppedImg(imageSrc, pixelCrop) {
        const image = await this.createImage(imageSrc)
        const canvas = document.createElement('canvas')
        const ctx = canvas.getContext('2d')

        const maxSize = Math.max(image.width, image.height)
        const safeArea = 2 * ((maxSize / 2) * Math.sqrt(2))


        // set each dimensions to double largest dimension to allow for a safe area for the
        // image to rotate in without being clipped by canvas context
        canvas.width = safeArea
        canvas.height = safeArea

        // translate canvas context to a central location on image to allow rotating around the center.
        ctx.translate(safeArea / 2, safeArea / 2)
        ctx.translate(-safeArea / 2, -safeArea / 2)

        // draw rotated image and store data.
        ctx.drawImage(
            image,
            safeArea / 2 - image.width * 0.5,
            safeArea / 2 - image.height * 0.5
        )
        const data = ctx.getImageData(0, 0, safeArea, safeArea)

        // set canvas width to final desired crop size - this will clear existing context
        canvas.width = pixelCrop.width
        canvas.height = pixelCrop.height

        // paste generated rotate image with correct offsets for x,y crop values.
        ctx.putImageData(
            data,
            Math.round(0 - safeArea / 2 + image.width * 0.5 - pixelCrop.x),
            Math.round(0 - safeArea / 2 + image.height * 0.5 - pixelCrop.y)
        )

        // As Base64 string
        return canvas.toDataURL('image/jpeg');
        
        // As a blob
        //return new Promise(resolve => {
        //    canvas.toBlob(file => {
        //        resolve(URL.createObjectURL(file))
        //    }, 'image/jpeg')
        //})
    }

    async cropImage() {        
        const croppedImage = await this.getCroppedImg(
            this.props.image.preview,
            this.state.croppedAreaPixels
        )
        this.props.handleOnCrop([croppedImage], this.props.onChange);
        this.props.onImageCrop();        
    }

    render() {        
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
                            onClick={this.cropImage.bind(this)}
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