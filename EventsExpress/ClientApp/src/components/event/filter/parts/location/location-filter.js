import { FilterExpansionPanel } from "../../expansion-panel/filter-expansion-panel";
import { change, getFormValues } from "redux-form";
import React from "react";
import { connect } from "react-redux";
import { enumLocationType } from "../../../../../constants/EventLocationType";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FormControl from "@material-ui/core/FormControl";
import RadioGroup from "@material-ui/core/RadioGroup";
import Radio from "@material-ui/core/Radio";
import { Field } from "redux-form";
import { LocationMapWithCircle } from "../../../../helpers/form-helpers/location";
import DisplayMap from "../../../../event/map/display-map";
import "../../../slider.css";
const LocationFilter = ({ dispatch, location, formValues }) => {
  const clear = () => dispatch(change("filter-form", "location", null));

  const onChangeLocationType = (event) => {
    const type = Number(event.target.value);
    if (type === enumLocationType.map) {
      dispatch(
        change("filter-form", "location", {
          type: enumLocationType.map,
          latitude: null,
          longitude: null,
          radius: 1,
        })
      );
    } else if (type === enumLocationType.online) {
      dispatch(
        change("filter-form", "location", { type: enumLocationType.online })
      );
    }
  };

  return (
    <FilterExpansionPanel
      title="Location"
      onClearClick={clear}
      clearDisabled={formValues.location === null}
    >
      <div className="row">
        <div>
          <FormControl name="location.type">
            <RadioGroup onChange={onChangeLocationType}>
              <FormControlLabel
                value={String(0)}
                control={<Radio />}
                label="Map"
                checked={
                  formValues !== null &&
                  formValues.location !== null &&
                  formValues.location.type === enumLocationType.map
                }
              />
              <FormControlLabel
                value={String(1)}
                control={<Radio />}
                label="Online"
                checked={
                  formValues !== null &&
                  formValues.location !== null &&
                  formValues.location.type === enumLocationType.online
                }
              />
            </RadioGroup>
          </FormControl>
        </div>
        {formValues !== null &&
          formValues.location !== null &&
          formValues.location.radius && (
            <div>
              <div class="slidecontainer">
                <label>Radius is {formValues.location.radius} km</label>
                <Field
                  name="location.radius"
                  component="input"
                  type="range"
                  min="1"
                  max="1000"
                  value={formValues.location.radius}
                  step="1"
                  className="radius-slider"
                />
              </div>
            </div>
          )}
        {formValues !== null &&
          formValues.location &&
          formValues.location.latitude &&
          formValues.location.longitude && (
            <div>
              <p>Current position on the Map is:</p>
              <p>latitude: {formValues.location.latitude}</p>
              <p>longitude: {formValues.location.longitude}</p>
              <DisplayMap location={{ ...formValues.location }} />
            </div>
          )}
        {formValues !== null &&
          formValues.location !== null &&
          formValues.location.type === enumLocationType.map && (
            <Field
              name="location"
              component={LocationMapWithCircle}
              radius={formValues.location.radius}
            />
          )}
      </div>
    </FilterExpansionPanel>
  );
};

const mapStateToProps = (state) => {
  return {
    ...state.eventsFilter.locationFilter,
    formValues: getFormValues("filter-form")(state),
  };
};

export default connect(mapStateToProps)(LocationFilter);
