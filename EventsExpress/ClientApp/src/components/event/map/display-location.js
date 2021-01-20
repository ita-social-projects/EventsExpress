import React, { Component } from 'react';
import * as Geocoding from 'esri-leaflet-geocoder';
import L from 'leaflet';
import { countries } from 'country-data';

class DisplayLocation extends Component {
    constructor(props) {
        super(props);

        this.state = {
            address: {}
        };
        
        this.defineAddress = this.defineAddress.bind(this);
        this.geocodeCoords();
    }

    geocodeCoords = () => {
        var geocodeService = Geocoding.geocodeService();
        geocodeService.reverse()
        .latlng(L.latLng(
            this.props.latitude,
            this.props.longitude))
        .language("en")
        .run(this.defineAddress);
    }

    defineAddress (error, result) {
        if (error) {
            this.setState(() => ({address: {PlaceName: "Location is not defined"}}));
            return;
        }
    
        this.setState(() => ({address: result.address}));
    }

    render() {
        const { PlaceName, City, CountryCode} = this.state.address;

        return (
            <>
                <div>
                    {PlaceName}
                </div>
                {City && City != "" &&
                    <div>
                        {City}
                    </div>
                }
                {CountryCode && CountryCode !="" && PlaceName != countries[CountryCode].name &&
                    <div>
                        {countries[CountryCode].name}
                    </div>
                }
            </>
        );
    }
}

export default DisplayLocation;