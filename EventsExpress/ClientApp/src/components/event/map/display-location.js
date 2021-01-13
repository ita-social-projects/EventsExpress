import React, { Component } from 'react';
import { countries } from 'country-data';

export class DisplayLocation extends Component {

    render() {
        const {
            PlaceName,
            City,
            CountryCode
        } = this.props.address;

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
                {PlaceName != countries[CountryCode].name &&
                    <span>
                        <br />
                        {countries[CountryCode].name}
                    </span>
                }
            </>
        );
    }
};
export default DisplayLocation;