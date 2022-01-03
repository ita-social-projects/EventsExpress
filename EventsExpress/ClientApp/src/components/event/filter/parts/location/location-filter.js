import { FilterExpansionPanel } from "../../expansion-panel/filter-expansion-panel";
import { change, getFormValues } from "redux-form";
import React from "react";
import { connect } from "react-redux";
import { enumLocationType } from "../../../../../constants/EventLocationType";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FormControl from "@material-ui/core/FormControl";
import RadioGroup from "@material-ui/core/RadioGroup";
import Radio from "@material-ui/core/Radio";
import { LocationMapWithMarker } from "../../../../helpers/form-helpers/location";
const LocationFilter = ({ dispatch, location, formValues }) => {
  const clear = () => dispatch(change("filter-form", "location", {}));

  const onChangeLocationType = (event) => {
    const type = Number(event.target.value);
    if (type === enumLocationType.map) {
      dispatch(change('filter-form', 'location', {type:enumLocationType.map}));
    } else if (type === enumLocationType.online) {
      dispatch(change('filter-form', 'location', {type:enumLocationType.online}));
    }
  };
  
  const onMapLocationChange = (mapLocation) => {
    dispatch(change('filter-form', 'location', {type:enumLocationType.map, latitude:mapLocation.latitude, longitude:mapLocation.longitude}));
  };
  return (
    <FilterExpansionPanel
      title="Location"
      onClearClick={clear}
      clearDisabled={formValues.location !== null}
    >
      <FormControl name="location.type">
        <RadioGroup onChange={onChangeLocationType}>
          <FormControlLabel
            value={String(0)}
            control={<Radio />}
            label="Map"
            checked={formValues.location !== "" && formValues.location.type === enumLocationType.map}
          />
          <FormControlLabel
            value={String(1)}
            control={<Radio />}
            label="Online"
            checked={formValues.location !== "" && formValues.location.type === enumLocationType.online}
          />
        </RadioGroup>
      </FormControl>
      {formValues !== "" && formValues.location.type === enumLocationType.map && (
            <LocationMapWithMarker
              latitude={formValues.location.latitude !== null ? formValues.location.latitude : null}
              longitude={formValues.location.longitude !== null ? formValues.location.longitude : null}
              onChangeValues={onMapLocationChange}
            />
          )}
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
