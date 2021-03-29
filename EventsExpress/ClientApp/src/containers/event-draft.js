import React, { Component } from 'react';
import { createBrowserHistory } from 'history';
import EventForm from '../components/event/event-form';
import { connect } from 'react-redux';
import { getFormValues, reset , isPristine} from 'redux-form';
import { setEventPending, setEventSuccess, edit_event, publish_event} from '../actions/event/event-add-action';
import { validateEventForm } from '../components/helpers/helpers'
import { resetEvent } from '../actions/event/event-item-view-action';
import Button from "@material-ui/core/Button";
import get_categories from '../actions/category/category-list-action';
import L from 'leaflet';

const history = createBrowserHistory({ forceRefresh: true });
class EventDraftWrapper extends Component {

    handleClick = () => {
        history.push(`/drafts`);
    }

    componentWillMount = () => {
        this.props.get_categories();
    }

    componentDidUpdate = () => {
        if (!this.props.add_event_status.errorEvent && this.props.add_event_status.isEventSuccess) {
            this.props.reset();
        }
    }

    componentWillUnmount() {
        this.props.reset();
    }

    onPublish = async (values) => { 
        
        if (!this.props.pristine)
        {
            await this.props.add_event({ ...validateEventForm(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
        }
         return this.props.publish(this.props.event.id);
        
    }

    onSave = () => {
        return this.props.add_event({ ...validateEventForm(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }

    
    

    render() {
        let initialValues = {
            ...this.props.event,
            location: this.props.event.location !== null ? {
                selectedPos: L.latLng(
                    
                    this.props.event.location.latitude,
                    this.props.event.location.longitude
                ),
                onlineMeeting: this.props.event.location.onlineMeeting,
                type: String(this.props.event.location.type)
            } : null,

        }
        console.log(this.props.form_values)
        return <>
           
            <EventForm
                all_categories={this.props.all_categories}
                onCancel={this.props.onCancelEditing}
                onSubmit={this.onPublish}
                onPublish={this.onSave}
                initialValues={initialValues}
                form_values={this.props.form_values}
                checked={this.props.event.isReccurent}
                haveReccurentCheckBox={false}
                haveMapCheckBox={true}
                haveOnlineLocationCheckBox={true}
                disabledDate={false}
                isCreated={true} ><div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        onClick={this.onSave}
                    >
                        Save
                        </Button>
                </div>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit"
                        >
                        Publish
                        </Button>
                </div>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        onClick={this.handleClick}>
                        Cancel
                        </Button>
                </div></EventForm>
        </>
    }
}   

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    add_event_status: state.add_event,
    all_categories: state.categories,
    form_values: getFormValues('event-form')(state),
    pristine: isPristine('event-form')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event(data)),
        publish: (data) => dispatch(publish_event(data)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventDraftWrapper);