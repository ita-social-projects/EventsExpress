import React, { Component } from 'react';
import * as Geocoding from 'esri-leaflet-geocoder';
import L from 'leaflet';
import { countries } from 'country-data';

class DisplayLocation extends Component {
    constructor(props) {
        super(props);

        this.state = {
            address: null
        };
        this.defineAddress = this.defineAddress.bind(this);
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
            return;
        }
    
        this.setState(() => ({address: result.address}));
    }

    componentWillMount() {
        this.geocodeCoords();
    }

    render() {

        const placeName = this.state.address ? this.state.address.PlaceName : "";
        const city = this.state.address ? this.state.address.City : "";
        const countryCode = this.state.address ? this.state.address.CountryCode : "";

        return (
            <>
                {placeName != "" &&
                    <span>
                        {placeName}
                    </span>
                }
                {placeName != "" && city != "" &&
                    city != placeName &&
                        <span>
                            <br />
                            {city}
                        </span>
                }
                {city != "" &&
                    placeName == "" &&
                        <span>
                            {city}
                        </span>
                }
                {countryCode !="" && 
                    placeName != countries[countryCode].name &&
                        <span>
                            <br />
                            {countries[countryCode].name}
                        </span>
                }
                {placeName == "" && 
                    city =="" && 
                    countryCode =="" &&
                        <span>
                            Location is not defined
                        </span>    
                }
            </>
        );
    }
}

export default DisplayLocation;