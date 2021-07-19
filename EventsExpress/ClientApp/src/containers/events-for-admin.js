import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event/event-list-action';

class AdminEventListWrapper extends Component {
    componentWillMount() {
        this.props.get_events(this.props.params);
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data } = this.props.events;
        const { items } = data;

        return <Spinner showContent={data != undefined}>
            <EventList
                current_user={current_user}
                data_list={items}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                callback={this.props.get_events}
            />
        </Spinner>
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
