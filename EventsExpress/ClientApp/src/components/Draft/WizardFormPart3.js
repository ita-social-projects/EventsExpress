import React, { Component } from 'react';
import { getFormValues, reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';
import { setEventPending, setEventSuccess, publish_event, edit_event_part3 } from '../../actions/event-add-action';
import Button from "@material-ui/core/Button";
import 'react-widgets/dist/css/react-widgets.css'
import {
    renderTextField,
    radioLocationType
} from '../helpers/helpers';
import LocationMap from '../event/map/location-map';
import { enumLocationType } from '../../constants/EventLocationType';
import { validateEventFormPart3 } from '../helpers/helpers'

class Part3 extends Component {


    onSubmit = () => {
        return this.props.add_event({ ...validateEventFormPart3(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }

    initializeIfNeed() {
        if (this.props.event) {
             let initialValues = {
                location: this.props.event.location !== null ? {
                    selectedPos: L.latLng(

                        this.props.event.location.latitude,
                        this.props.event.location.longitude
                    ),
                    onlineMeeting: this.props.event.location.onlineMeeting,
                    type: String(this.props.event.location.type)
                } : null,
            }
            this.props.initialize(initialValues);
            this.setState({ initialized: true });
        }
       
    }

    componentDidMount() {
        this.initializeIfNeed();
    }
    
    render() {

        let initialValues = {
            location: this.props.event.location !== null ? {
                selectedPos: L.latLng(

                    this.props.event.location.latitude,
                    this.props.event.location.longitude
                ),
                onlineMeeting: this.props.event.location.onlineMeeting,
                type: String(this.props.event.location.type)
            } : null,
          }
        return (
            
            <form onSubmit={this.props.handleSubmit(this.onSubmit)}>
                <Field name="location.type" component={radioLocationType} />
                {(this.props.form_values == undefined
                    || (this.props.form_values.location
                        && this.props.form_values.location.type === enumLocationType.map))
                    &&
                    <div className="mt-2">
                        <Field
                            name='location.selectedPos'
                            initialData={
                                initialValues &&
                                initialValues.location &&
                                initialValues.location.selectedPos
                            }

                            component={LocationMap}
                        />
                    </div>
                }
                {this.props.form_values
                    && this.props.form_values.location
                    && this.props.form_values.location.type === enumLocationType.online &&

                    <div className="mt-2">
                        <label for="url">Enter an https:// URL:</label>
                        <Field
                            name='location.onlineMeeting'
                            component={renderTextField}
                            type="url"
                            label="Url"

                        />
                    </div>
                }
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit"
                    >
                        Save
                        </Button>
                </div>
            </form >
        );

    }
}

const mapStateToProps = (state) => ({
    initialData: state.event.data,
    user_id: state.user.id,
    all_categories: state.categories,
    form_values: getFormValues('Part3')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event_part3(data)),
        publish: (data) => dispatch(publish_event(data)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('Part3'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
};

Part3 = connect(
    mapStateToProps,
    mapDispatchToProps
)(Part3);

export default reduxForm({
    form: 'Part3',
    enableReinitialize: true
})(Part3);

