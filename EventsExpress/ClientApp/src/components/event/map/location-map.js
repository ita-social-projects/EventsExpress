import React, { Component } from 'react';
import {
    Map,
    TileLayer,
    Marker,
    Popup
} from 'react-leaflet';
import * as Geocoding from 'esri-leaflet-geocoder';
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
            selectedPos: null,
            coords: null,
        }
    }

    defineAddress = (error, result) => {
        if (error) {
            return;
        }
        this.setState({ coords: result.address });
    }

    geocodeCoords(latlng) {
        var geocodeService = Geocoding.geocodeService();
        geocodeService.reverse()
        .latlng(latlng)
        .language("en")
        .run(this.defineAddress);
    }

    setSelectedPos = (latlng) => {
        this.setState({ selectedPos: latlng });
        this.props.input.onChange(latlng);
    }

    handleClick = (e) => {
        this.setSelectedPos(e.latlng);
    }

    handleSearch = (e) => {
        this.setSelectedPos(e.latLng);
    }

    render() {

        const marker = this.state.selectedPos ? this.state.selectedPos : this.props.position;
        console.log(this.props);

        return (
            <div
                style={{ position: "relative", width: "100%", height: "40vh" }}
                id="my-map">
                <Map
                    id="map"
                    style={mapStyles}
                    center={marker}
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
                </Map>
            </div>
        );
    }
};

export default LocationMap;
