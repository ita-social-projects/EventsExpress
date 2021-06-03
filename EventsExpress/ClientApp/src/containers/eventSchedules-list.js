import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventSchedulesList from '../components/eventSchedule/eventSchedule-list';
import Spinner from '../components/spinner';
import { getEventSchedules } from '../actions/eventSchedule/eventSchedule-list-action';

class EventSchedulesListWrapper extends Component {
    constructor(props) {
        super(props);
        this.props.getEventSchedules();
    }

    render() {
        
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending } = this.props.eventSchedules;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending
            ? <EventSchedulesList
                current_user={current_user}
                data_list={data.items}
            />
            : null;

        return <>
            { spinner || content }
        </>
    }
}

const mapStateToProps = (state) => {
    return {
        eventSchedules: state.eventSchedules,
        current_user: state.user
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        getEventSchedules: () => dispatch(getEventSchedules()),
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventSchedulesListWrapper);
