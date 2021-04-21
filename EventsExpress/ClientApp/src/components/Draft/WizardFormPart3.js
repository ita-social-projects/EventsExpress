import React, { Component } from 'react';
import { compose } from 'redux';
import { getFormValues, reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';
import { setEventPending, setEventSuccess, publish_event, edit_event_part3 } from '../../actions/event/event-add-action';
import 'react-widgets/dist/css/react-widgets.css'
import {
    renderTextField,
    radioLocationType
} from '../helpers/helpers';
import LocationMap from '../event/map/location-map';
import { enumLocationType } from '../../constants/EventLocationType';
import submit from './submit3';
import { warn } from './Validator3';

class Part3 extends Component {


    initializeIfNeed() {
        if (this.props.event && !this.state.initialized && this.props.event.location) {
            let initialValues = {
                location: this.props.event.location !== null ? {

                } : null,
                selectedPos: L.latLng(

                    this.props.event.location.latitude,
                    this.props.event.location.longitude
                ),
                onlineMeeting: this.props.event.location.onlineMeeting,
                type: String(this.props.event.location.type)
            }
            this.props.initialize(initialValues);
            this.setState({ initialized: true });
        }

    }
    
    componentDidUpdate() {
        console.log(this.props.event);
        this.initializeIfNeed();
    }
    componentDidMount() {
        console.log(this.props.event);
        this.initializeIfNeed();
    }
    state = { initialized: false };

    
    
    render() {

        {
            if (this.props.event && this.props.event.location) {
                var initialValues = {
                    location: this.props.event.location !== null ? {

                    } : null,
                    selectedPos: L.latLng(

                        this.props.event.location.latitude,
                        this.props.event.location.longitude
                    ),
                    onlineMeeting: this.props.event.location.onlineMeeting,
                    type: String(this.props.event.location.type)
                }
            }
        }

        const { handleSubmit } = this.props;
        return (
            
            <form onSubmit={handleSubmit}>
                <Field name="type" component={radioLocationType} />
                {this.props.form_values
                    && this.props.form_values.type === enumLocationType.map &&
                    <div className="mt-2">
                        <Field
                            name='selectedPos'
                            initialData={
                                initialValues &&
                                initialValues.location &&
                                initialValues.selectedPos
                            }

                            component={LocationMap}
                        />
                    </div>
                }
                {this.props.form_values
                    && this.props.form_values.type === enumLocationType.online &&

                    <div className="mt-2">
                        <label for="url">Enter an https:// URL:</label>
                        <Field
                            name='onlineMeeting'
                            component={renderTextField}
                            type="url"
                            label="Url"

                        />
                    </div>
                }
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

export default compose(
    connect(mapStateToProps, mapDispatchToProps),
    reduxForm({ form: 'Part3', warn, enableReinitialize: true, onSubmit: submit })
)(Part3);