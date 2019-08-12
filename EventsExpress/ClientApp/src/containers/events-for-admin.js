import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_eventsForAdmin } from '../actions/event-list';
import BadRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
import Unauthorized from '../components/Route guard/401'
import Forbidden from '../components/Route guard/403'
import { Redirect } from 'react-router'
import history from '../history';


class AdminEventListWrapper extends Component {
    componentWillUpdate(prevProps, prevState) {
        if (this.props.isError.ErrorCode == '500') {
            this.getEvents("?page=1");
        }
    }
    componentWillMount() {
        this.getEvents(this.props.params);
    }
    getEvents = (page) => this.props.get_eventsForAdmin(page);


    render() {

        const { data, isPending, isError } = this.props;
        const { items } = this.props.data;
        const errorMessage = isError.ErrorCode == '403' ? <Forbidden /> : isError.ErrorCode == '500' ? <Redirect from="*" to="/admin/events?page=1" /> : isError.ErrorCode == '401' ? <Unauthorized /> : isError.ErrorCode == '400' ? <BadRequest /> : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage ? <EventList data_list={items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getEvents} /> : null;

        return <>
            {errorMessage}
            {spinner}
            {content}
        </>
    }
}

const mapStateToProps = (state) => (state.events);

const mapDispatchToProps = (dispatch) => {
    return {
        get_eventsForAdmin: (page) => dispatch(get_eventsForAdmin(page))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(AdminEventListWrapper);