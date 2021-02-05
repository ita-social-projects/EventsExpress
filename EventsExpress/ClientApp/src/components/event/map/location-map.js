import React, { Component } from 'react';
import {
    Map,
    TileLayer,
    Marker,
    Popup,
    Circle 
} from 'react-leaflet';
import Search from 'react-leaflet-search';
import './map.css'

const mapStyles = {
    width: "100%",
    height: "100%"
};

class LocationMap extends Component {
    constructor(props) {
        super(props);
        this.state = {
            startPosition: [50.4547, 30.5238],
            selectedPos: null
        }
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

    render() {
        const center = this.props.initialData || this.state.startPosition;
        const marker = this.state.selectedPos ? this.state.selectedPos : this.props.initialData;
        const { error, touched, invalid } = this.props.meta;
        const { circle, radius } = this.props;
        const { selectedPos } = this.state
        return (
            <div
                className="mb-4"
                style={{ position: "relative", width: "100%", height: "40vh" }}
                id="my-map">
                <Map
                    id="map"
                    style={mapStyles}
                    center={center}
                    zoom={10}
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
                    {marker &&
                        <Marker position={marker} 
                            draggable={true}>
                            <Popup position={marker}> 
                                <pre>
                                    {JSON.stringify(marker, null, 2)}
                                </pre>
                            </Popup>
                        </Marker>
                    }
                    {circle && radius && selectedPos &&
                        <Circle center={selectedPos} pathOptions={{ color: 'blue' }} radius={radius*1000} />
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
