import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import eventHelper from '../components/helpers/eventHelper';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event-list-action';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';

class AdminEventListWrapper extends Component {
    componentWillMount() {
        this.props.get_events(this.props.params);
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending, isError } = this.props.events;
        const { items } = data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect
                    from="*"
                    to={eventHelper.getQueryStringByEventFilter(this.props.events.filter)}
                />
                : isError.ErrorCode == '401'
                    ? <Unauthorized />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage
            ? <EventList
                current_user={current_user}
                data_list={items}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                callback={this.props.get_events}
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
        events: state.events,
        current_user: state.user
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_events: (page) => dispatch(get_events(page)),
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AdminEventListWrapper);
