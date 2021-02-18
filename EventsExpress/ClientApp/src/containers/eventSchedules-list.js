import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import EventSchedulesList from '../components/eventSchedule/eventSchedule-list';
import Spinner from '../components/spinner';
import { getEventSchedules } from '../actions/eventSchedule-list-action';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';

class EventSchedulesListWrapper extends Component {
    constructor(props) {
        super(props);
        this.props.getEventSchedules();
    }

    render() {
        
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending, isError } = this.props.eventSchedules;
        const { items } = this.props.eventSchedules.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect
                    from="*"
                    to={{
                        pathname: "/home/eventSchedules",
                    }}
                />
                : isError.ErrorCode == '401'
                    ? <Unauthorized />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage
            ? <EventSchedulesList
                current_user={current_user}
                data_list={data.items}
            />
            : null;

        return <>
            {!errorMessage
                ? spinner || content
                : errorMessage
            }
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
