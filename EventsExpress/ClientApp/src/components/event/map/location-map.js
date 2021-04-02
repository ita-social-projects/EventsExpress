import React, { Component } from 'react';
import {
    Map,
    TileLayer,
    Marker,
    Popup,
    Circle
} from 'react-leaflet';
import Search from 'react-leaflet-search';
import './map.css';

const mapStyles = {
    width: "100%",
    height: "100%"
};

class LocationMap extends Component {
    constructor(props) {
        super(props);
        let value = null;
        let start = [50.4547, 30.5238];//start position by default

        if (this.props.initialValues != undefined) {
            if (this.props.initialValues.x != undefined) {
                value = { lat: this.props.initialValues.x, lng: this.props.initialValues.y };
                start = [this.props.initialValues.x, this.props.initialValues.y];
            }
        }

        if (this.props.selectedPos != undefined) {
            if (this.props.selectedPos[0] != undefined) {
                value = { lat: this.props.selectedPos[0], lng: this.props.selectedPos[1] };
                start = [this.props.selectedPos[0], this.props.selectedPos[1]];
            }
        }

        this.state = {
            startPosition: start,
            selectedPos: value,
        }
        this.map = React.createRef();
    }

    setSelectedPos = (latlng) => {
        this.setState({ selectedPos: latlng });
        this.props.input.onChange(latlng);
    }

    handleClick = (e) => {

        this.setSelectedPos(e.latlng);
        if (this.props.onClickCallBack != null) {
            this.props.onClickCallBack(e.latlng);
        }
    }

    handleSearch = (e) => {
        this.setSelectedPos(e.latLng);
    }

    getCurrentZoom() {
        let defaultZoom = 8;
        return this.map.leafletElement != undefined ? this.map.leafletElement._zoom : defaultZoom;
    }

    getRadiusScale(radius) {
        let resZoom = this.getCurrentZoom();
        if (radius >= 100 && radius < 1000) {
            radius = Number(Math.floor(radius / 100) * 5 * 100) + Number(radius);
        }
        else if (radius >= 1000 && radius < 5000) {
            radius = Number(Math.floor(radius / 100) * 2 * 1000) + Number(radius);
        }
        else if (radius > 5000) {
            radius = Number(Math.floor(radius / 100) * 5 * 1000) + Number(radius);
        }
        return (1.0083 * Math.pow(radius / 1, 0.5716) * (resZoom / 2)) * 1000;
    }

    getStartPosition() {
        let start = [50.4547, 30.5238];
        if (this.props.initialValues.selectedPos != undefined) {
            if (this.props.initialValues.selectedPos.lat != null && this.props.initialValues.selectedPos.lng != null) {
                start = [this.props.initialValues.selectedPos.lat, this.props.initialValues.selectedPos.lng];
            }
        }
        return start;
    }
    render() {
        const { error, touched, invalid } = this.props.meta;
        let { circle, radius, is_add_event_map_location } = this.props;
        const marker = this.state.selectedPos ? this.state.selectedPos : this.props.initialData;
        let start = this.getStartPosition();
        let resZoom = this.getCurrentZoom();
        let scaleRadius = this.getRadiusScale(radius);
        return (
            <div
                className="mb-4"
                style={{ position: "relative", width: "100%", height: "40vh" }}
                id="my-map">
                <Map
                    ref={(ref) => { this.map = ref }}
                    id="map"
                    style={mapStyles}
                    center={start}
                    zoom={resZoom}
                    onClick={this.handleClick}>

                    <TileLayer
                        url="https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png"
                    />
                    <Search
                        position="topright"
                        inputPlaceholder="Enter location"
                        showMarker={true}
                        zoom={7}
                        closeResultsOnClick={false}
                        openSearchOnLoad={false}
                        onChange={this.handleSearch}
                    />

                    {is_add_event_map_location == true
                        ? marker &&
                        <Marker position={marker}
                            draggable={true}>
                            <Popup position={marker}>
                                <pre>
                                    {JSON.stringify(marker, null, 2)}
                                </pre>
                            </Popup>
                        </Marker>

                        : this.props.initialValues.selectedPos &&
                            <Marker position={this.props.initialValues.selectedPos}
                                draggable={true}>
                                <Popup position={this.props.initialValues.selectedPos}>
                                    <pre>
                                        {JSON.stringify(this.props.initialValues.selectedPos, null, 2)}
                                    </pre>
                                </Popup>
                            </Marker>

                            ? circle && radius &&
                            <Circle center={start} pathOptions={{ color: 'blue' }} radius={scaleRadius} />
                            : null
                    }
                </Map>
                <span className="error-text">
                    {touched && invalid && error}
                </span>
            </div>
        );
    }
}

export default LocationMap;
