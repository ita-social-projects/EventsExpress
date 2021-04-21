import React, { Component } from 'react';
import { reduxForm, Field, getFormValues, reset, isPristine } from 'redux-form';
import { connect } from 'react-redux';
import { setEventPending, setEventSuccess, edit_event_part2 } from '../../actions/event/event-add-action';
import { validateEventFormPart2 } from '../helpers/helpers'
import 'react-widgets/dist/css/react-widgets.css'
import DropZoneField from '../helpers/DropZoneField';
import { compose } from 'redux'
import submit from './submit2';

class Part2 extends Component {

    onSubmit = () => {
        return this.props.add_event({ ...validateEventFormPart2(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }

    render() {

        const photoUrl = this.props.initialValues ?
            this.props.initialValues.photoUrl : null;
        return (
            <form onSubmit={this.props.handleSubmit(this.onSubmit)}
                encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">
                    <Field
                        id="image-field"
                        name="photo"
                        component={DropZoneField}
                        type="file"
                        crop={true}
                        cropShape='rect'
                        photoUrl={photoUrl}
                    />
                </div>
            </form >
        );

    }
}


const mapStateToProps = (state) => ({
    initialData: state.event.data,
    user_id: state.user.id,
    add_event_status: state.add_event,
    form_values: getFormValues('Part2')(state),
    pristine: isPristine('Part2')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event_part2(data)),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('Part2'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
}

export default compose(
    connect(mapStateToProps, mapDispatchToProps),
    reduxForm({ form: 'Part2', onSubmit: submit })
)(Part2);