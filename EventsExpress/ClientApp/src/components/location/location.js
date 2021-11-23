import React, { Component } from "react";
import { Field, change } from "redux-form";
import "react-widgets/dist/css/react-widgets.css";
import {
  LocationMapWithMarker,
  radioButton,
  renderTextField,
} from "../helpers/form-helpers";
import { enumLocationType } from "../../constants/EventLocationType";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Radio from "@material-ui/core/Radio";
import { renderFieldError } from "../helpers/form-helpers";
import FormControl from "@material-ui/core/FormControl";
import RadioGroup from "@material-ui/core/RadioGroup";
import { FieldFeedback, FieldFeedbacks } from "react-form-with-constraints";
import TextField from "@material-ui/core/TextField";
export default class Location extends Component {
  onChangeLocationType = (event) => {
    const type = Number(event.target.value);
    if (type == enumLocationType.map) {
      this.props.input.onChange({ type, latitude: null, longitude: null });
    } else if (type == enumLocationType.online) {
      this.props.input.onChange({ type, onlineMeeting: null });
    }
    //console.log(this.props);
  };
  onUrlInputChange = (event) => {
    this.props.input.onChange({
      type: enumLocationType.online,
      onlineMeeting: event.target.value == "" ? null : event.target.value,
    });
  };
  returnLocationRender = () => {
    if (this.props.input.value != null) {
      if (
        this.props.input.value != "" &&
        this.props.input.value.type == enumLocationType.map
      ) {
        return (
          <div className="mt-2">
            <Field name="location" component={LocationMapWithMarker} />
          </div>
        );
      } else if (
        this.props.input.value != "" &&
        this.props.input.value.type == enumLocationType.online
      ) {
        return (
          <>
            <div className="mt-2">
              <label htmlFor="url">Enter an https:// URL:</label>
              <br />
              <TextField
                name="onlineMeeting"
                label="Url"
                id="url"
                type="url"
                onChange={this.onUrlInputChange}
              />
            </div>
            <br />
          </>
        );
      }
    }
  };

  render() {
    //console.log("input from props", this.props.input);
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
              checked={
                this.props.input.value != "" &&
                this.props.input.value.type == enumLocationType.map
              }
            />
            <FormControlLabel
              value={String(1)}
              control={<Radio />}
              label="Online"
              checked={
                this.props.input.value != "" &&
                this.props.input.value.type == enumLocationType.online
              }
            />
          </RadioGroup>
        </FormControl>
        {renderedLocation}
        {renderFieldError({
          touched: this.props.meta.touched,
          error: this.props.meta.error,
        })}
      </span>
    );
  }
}
