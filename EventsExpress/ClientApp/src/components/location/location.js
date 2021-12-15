import React, { Component } from "react";
import "react-widgets/dist/css/react-widgets.css";
import { LocationMapWithMarker } from "../helpers/form-helpers/location";
import { enumLocationType } from "../../constants/EventLocationType";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Radio from "@material-ui/core/Radio";
import { renderFieldError } from "../helpers/form-helpers";
import FormControl from "@material-ui/core/FormControl";
import RadioGroup from "@material-ui/core/RadioGroup";
import TextField from "@material-ui/core/TextField";
export default class Location extends Component {
  onChangeLocationType = (event) => {
    const type = Number(event.target.value);
    if (type === enumLocationType.map) {
      this.props.input.onChange({ type, latitude: null, longitude: null });
    } else if (type === enumLocationType.online) {
      this.props.input.onChange({ type, onlineMeeting: null });
    }
  };

  onUrlInputChange = (event) => {
    this.props.input.onChange({
      type: enumLocationType.online,
      onlineMeeting: event.target.value === "" ? null : event.target.value,
    });
  };

  onMapLocationChange = (mapLocation) =>{
    this.props.input.onChange({
      type:enumLocationType.map,
      latitude:mapLocation.latitude,
      longitude:mapLocation.longitude
    })
  }

  returnLocationRender = () => {
    const { value } = this.props.input;
    if (value != null) {
      if (value !== "" && value.type === enumLocationType.map) {
        return <LocationMapWithMarker
        latitude = {value.latitude !== null ? value.latitude : null}
        longitude = {value.longitude !== null ? value.longitude : null}
        onChangeValues = {this.onMapLocationChange}
        />;
      } else if (value !== "" && value.type === enumLocationType.online) {
        return (
          <>
            <label htmlFor="url">Enter an https:// URL:</label>
            <br />
            <TextField
              name="onlineMeeting"
              label="Url"
              id="url"
              fullWidth
              onChange={this.onUrlInputChange}
              value={
                value !== "" && value.type === enumLocationType.online
                  ? value.onlineMeeting
                  : ""
              }
            />
            <br />
          </>
        );
      }
    }
  };

  render() {
    const { value } = this.props.input;
    let renderedLocation = null;
    renderedLocation = this.returnLocationRender();
    return (
      <span>
        <FormControl name="location.type">
          <RadioGroup onChange={this.onChangeLocationType}>
            <FormControlLabel
              value={String(0)}
              control={<Radio />}
              label="Map"
              checked={value !== "" && value.type === enumLocationType.map}
            />
            <FormControlLabel
              value={String(1)}
              control={<Radio />}
              label="Online"
              checked={value !== "" && value.type === enumLocationType.online}
            />
          </RadioGroup>
        </FormControl>
        <div className="mt-2">{renderedLocation}</div>
        {renderFieldError({
          touched: this.props.meta.touched,
          error: this.props.meta.error,
        })}
      </span>
    );
  }
}
