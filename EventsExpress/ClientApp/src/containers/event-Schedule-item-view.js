import React,{useEffect} from 'react';
import { connect } from 'react-redux';
import EventScheduleItemView from '../components/eventSchedule/eventSchedule-item-view';
import SpinnerWrapper from './spinner';
import getEventSchedule, {
    resetEventSchedule
} from '../actions/eventSchedule/eventSchedule-item-view-action';
import get_event from "../actions/event/event-item-view-action";

const EventScheduleItemViewWrapper = (props) => {
    console.log(props)
    const { id } = props.match.params;
    const { data } = props.eventSchedule;

    useEffect(() => {
        props.getEventSchedule(id);
        console.log(props.eventSchedule);
        console.log(props.current_user);
        return () => props.reset();
    },[])

    useEffect(() => {
        const evId = props.eventSchedule.data?.eventId;
        evId && props.get_event(evId);
    },[data])

    return(
    <SpinnerWrapper showContent={data !== null}>
        <EventScheduleItemView
            eventSchedule={props.eventSchedule}
            current_user={props.current_user}
            event = {props.event}
        />
    </SpinnerWrapper>
    )
};

const mapStateToProps = (state) => ({
    eventSchedule: state.eventSchedule,
    current_user: state.user,
    event: state.event
});

const mapDispatchToProps = (dispatch) => ({
    getEventSchedule: (id) => dispatch(getEventSchedule(id)),
    get_event: (id) => dispatch(get_event(id)),
    reset: () => dispatch(resetEventSchedule())
})
export default connect(mapStateToProps, mapDispatchToProps)(EventScheduleItemViewWrapper);