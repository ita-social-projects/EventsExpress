import React, { Component } from "react";
import { reduxForm, Field, change } from "redux-form";
import moment from "moment";
import "react-widgets/dist/css/react-widgets.css";
import momentLocaliser from "react-widgets-moment";
import DropZoneField from "../helpers/DropZoneField";
import PhotoService from "../../services/PhotoService";
import periodicity from "../../constants/PeriodicityConstants";
import {
  renderDatePicker,
  renderCheckbox,
  renderSelectField,
  renderTextField,
  renderTextArea,
  renderMultiselect,
  parseEuDate,
} from "../helpers/form-helpers";
import { enumLocationType } from "../../constants/EventLocationType";
import "./event-form.css";
import asyncValidatePhoto from "../../containers/async-validate-photo";
import Location from "../location";
momentLocaliser(moment);

const photoService = new PhotoService();

class EventForm extends Component {
  state = { checked: this.props.initialValues.isReccurent };

  handleChange = () => {
    this.setState((state) => ({
      checked: !state.checked,
    }));
  };

  checkLocation = (location) => {
    if (location !== null) {
      if (location.type == enumLocationType.map) {
        location.latitude = null;
        location.longitude = null;
        change(`event-form`, `location`, location);
      }

      if (location.type == enumLocationType.online) {
        location.onlineMeeting = null;
        change(`event-form`, `location.onlineMeeting`, location);
      }
    }
  };

  periodicityListOptions = periodicity.map((item) => (
    <option value={item.value} key={item.value}>
      {" "}
      {item.label}{" "}
    </option>
  ));

  render() {
    const { form_values, all_categories, user_name, onError, error } = this.props;
    const { checked } = this.state;

    if (onError) {
      onError(error);
    }

    return (
      <form
        onSubmit={this.props.handleSubmit(this.props.onSubmit)}
        encType="multipart/form-data"
        autoComplete="off"
      >
        <div className="text text-2 pt-md-2">
          <Field
            id="image-field"
            name="photo"
            component={DropZoneField}
            type="file"
            crop={true}
            cropShape="rect"
            loadImage={() => photoService.getFullEventPhoto(this.props.eventId)}
          />
          <div className="mt-2">
            <Field
              name="organizer"
              component={renderTextField}
              type="input"
              label="Organizer"
              InputLabelProps={{ shrink: true }}
              inputProps={{ value: user_name }}
              readOnly
            />
          </div>
          <div className="mt-2">
            <Field
              name="title"
              component={renderTextField}
              type="input"
              label="Title"
              inputProps={{ maxLength: 60 }}
            />
          </div>
          <div className="mt-2">
            <Field
              parse={Number}
              name="maxParticipants"
              component={renderTextField}
              type="number"
              label="Max Count Of Participants"
            />
          </div>
          {this.props.haveReccurentCheckBox && (
            <div className="mt-2">
              <Field
                type="checkbox"
                label="Recurrent Event"
                name="isReccurent"
                component={renderCheckbox}
                checked={checked}
                onChange={this.handleChange}
              />
            </div>
          )}
          {this.props.haveReccurentCheckBox && checked && (
            <div>
              <div className="mt-2">
                <Field
                  minWidth={200}
                  name="periodicity"
                  text="Periodicity"
                  component={renderSelectField}
                  parse={Number}
                >
                  {this.periodicityListOptions}
                </Field>
              </div>
              <div className="mt-2">
                <Field
                  name="frequency"
                  type="number"
                  component={renderTextField}
                  label="Frequency"
                  parse={Number}
                />
              </div>
            </div>
          )}
          <div className="mt-2">
            <Field
              name="isPublic"
              component={renderCheckbox}
              type="checkbox"
              label="Public"
            />
          </div>
          <div className="meta-wrap">
            <span>
              <Field
                name="dateFrom"
                label="From"
                minValue={moment(new Date())}
                component={renderDatePicker}
                parse={parseEuDate}
              />
            </span>
            {form_values && form_values.dateFrom && (
              <span className="retreat">
                <Field
                  name="dateTo"
                  label="To"
                  minValue={form_values.dateFrom}
                  component={renderDatePicker}
                  parse={parseEuDate}
                />
              </span>
            )}
          </div>
          <div className="mt-3">
            <Field
              name="description"
              component={renderTextArea}
              type="input"
              label="Description"
            />
          </div>
          <div className="mt-2">
            <Field
              name="categories"
              component={renderMultiselect}
              data={all_categories.data}
              valueField={"id"}
              textField={"name"}
              className="form-control"
              placeholder="#hashtags"
            />
          </div>
          <Field name="location" component={Location}/>
        </div>
        <div className="row my-4">{this.props.children}</div>
      </form>
    );
  }
}

export default reduxForm({
  form: "event-form",
  enableReinitialize: true,
  touchOnChange: true,
  asyncValidate: asyncValidatePhoto,
  asyncChangeFields: ["photo"],
})(EventForm);
