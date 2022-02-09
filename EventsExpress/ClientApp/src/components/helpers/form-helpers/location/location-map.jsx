import React, { Component } from 'react';
import { Map, TileLayer } from 'react-leaflet';
import '../../../event/map/map.css';

export default class LocationMap extends Component {
    constructor(props) {
        super(props);
        this.map = React.createRef();
    }

    updateLocation = (latlng) => {
        this.props.onUpdate(latlng);
    }

    handleClick = (e) => {
        this.updateLocation(e.latlng);
    }

    getCurrentZoom() {
        let defaultZoom = 8;
        return this.map.leafletElement != undefined ? this.map.leafletElement._zoom : defaultZoom;
    }

    render() {
        const { error, touched, invalid } = this.props.meta;
        const location = this.props.location;
        let zoom = this.getCurrentZoom();
        return (
            <div
                style={{ position: "relative", width: "100%", height: "40vh" }}
                id="my-map">
                <Map
                    ref={(ref) => { this.map = ref }}
                    id="map"
                    style={{ width: "100%", height: "100%" }}
                    center={location}
                    zoom={zoom}
                    onClick={this.handleClick}>
                    <TileLayer
                        url="https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png"
                    />
                    {this.props.children}     
                </Map>
                <span className="error-text">
                    {touched && invalid && error}
                </span>
            </div>
        );
    }
}