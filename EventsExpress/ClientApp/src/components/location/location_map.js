import React from 'react'
import { Component } from 'react';
import { LocationMapWithMarker } from '../helpers/form-helpers/location';
import { enumLocationType } from "../../constants/EventLocationType";

class LocationByMap extends Component {
  constructor(props) {
    super(props);
  };

  onMapLocationChange = (mapLocation)=>{
        this.props.input.onChange({
          type: enumLocationType.map,
          latitude: mapLocation.latitude,
          longitude: mapLocation.longitude,
          onlineMeeting : null
      });   
    }

    render(){
      const { input:{value}} = this.props;
    return (
        <LocationMapWithMarker
          latitude={value.latitude !== null ? value.latitude : null}
          longitude={value.longitude !== null ? value.longitude : null}
          onChangeValues={this.onMapLocationChange}
        />
      )
    }
}

export default (LocationByMap)

