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
        
        console.log(result.address);
        this.setState(() => ({address: result.address}));
    }

    componentWillMount() {
        this.geocodeCoords();
    }

    render() {

        const PlaceName = this.state.address ? this.state.address.PlaceName : "";
        const City = this.state.address ? this.state.address.City : "";
        const CountryCode = this.state.address ? this.state.address.CountryCode : "";

        return (
            <>
                {PlaceName != "" &&
                    <span>
                        {PlaceName}
                    </span>
                }
                {PlaceName != "" && City != "" &&
                    City != PlaceName &&
                    <span>
                        <br />
                        {City}
                    </span>
                }
                {City != "" &&
                    PlaceName == "" &&
                    <span>
                        {City}
                    </span>
                }
                {CountryCode !="" && 
                    PlaceName != countries[CountryCode].name &&
                        <span>
                            <br />
                            {countries[CountryCode].name}
                        </span>
                }
                {PlaceName == "" && 
                    City =="" && 
                    CountryCode =="" &&
                        <span>
                            Location is not defined
                        </span>    
                }
            </>
        );
    }
};

export default DisplayLocation;