import React, { Component } from 'react';
import ReactDOM from 'react-dom'
import Slider from '@material-ui/lab/Slider'
import Cropper from 'react-easy-crop'
import './image-resizer.css';

class ImageResizer extends Component {
    state = {
        imageSrc:
            'https://img.huffingtonpost.com/asset/5ab4d4ac2000007d06eb2c56.jpeg?cache=sih0jwle4e&ops=1910_1000',
        crop: { x: 0, y: 0 },
        zoom: 1,
        aspect: 16 / 9,
    }

    onCropChange = crop => {        
        this.setState({ crop })
    }

    onCropComplete = (croppedArea, croppedAreaPixels) => {
        console.log(croppedArea, croppedAreaPixels)
    }

    onZoomChange = zoom => {
        this.setState({ zoom })
    }    

    render() {        
        return (
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
                        classes={{ container: 'slider' }}
                    />
                </div>
            </div>
        )
    }
}

export default ImageResizer