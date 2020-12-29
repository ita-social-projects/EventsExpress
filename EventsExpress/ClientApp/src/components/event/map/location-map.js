import React, { Component } from 'react';
import {
    MapContainer,
    TileLayer,
    Marker,
    Popup,
    MapConsumer
} from 'react-leaflet';
import GeoSearch from "./geosearch";
import * as Geocoding from 'esri-leaflet-geocoder';
import {countries} from 'country-data';

const mapStyles = {
    width: "100%",
    height: "100%"
};

class LocationMap extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentPos: null,
            selectedCoords: null,
        }
    }

    defineAddress = (error, result) => {
        if (error) {
            return;
        }
        this.setState({ selectedCoords: result.address });
    }

    geocodeCoords(latlng) {
        var geocodeService = Geocoding.geocodeService();
        geocodeService.reverse()
        .latlng(latlng)
        .run(this.defineAddress);
    }

    handleClick = (e) => {
        this.setState({ currentPos: e.latlng });
        this.geocodeCoords(this.state.currentPos);
    }

    render() {

        const marker = this.state.currentPos ? this.state.currentPos : this.props.position;
        const selectedLocationInfo = this.state.selectedCoords ? countries[this.state.selectedCoords.CountryCode].name : null
        const userLocationInfo = selectedLocationInfo ? selectedLocationInfo : this.geocodeCoords(this.props.position);
        const inf = selectedLocationInfo ? selectedLocationInfo : userLocationInfo;

        return (
            <div
                style={{ position: "relative", width: "100%", height: "40vh" }}
                id="my-map">
                <MapContainer
                    style={mapStyles}
                    center={marker}
                    zoom={10}
                    onClick={this.handleClick}>
                    <TileLayer
                        url="https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png"
                    />
                    <GeoSearch />
                    {marker &&
                        <Marker position={marker} 
                            draggable={true}>
                            <Popup position={marker}>
                                Current location: 
                                <pre>
                                    {JSON.stringify(inf, null, 2)}
                                </pre>
                            </Popup>
                        </Marker>
                    }
                    <MapConsumer>
                        {(map) => {
                            map.on("click", this.handleClick);
                            return null;
                        }}
                    </MapConsumer>
                </MapContainer>
            </div>
        );
    }
};

export default LocationMap;
