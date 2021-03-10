import React, { Component } from 'react';
import PagePagination from '../shared/pagePagination';
import { connect } from 'react-redux';
import { reset_events, updateEventsFilters } from '../../actions/event-list-action';
import Event from './event-item';
import { change_event_status } from '../../actions/event-item-view';
import eventStatusEnum from '../helpers/eventStatusEnum';

class EventList extends Component {
    handlePageChange = (page) => {
        this.props.updateEventsFilters({
            ...this.props.filter,
            page: page,
        });
    };

    renderItems = arr =>
        arr.map(item => (
            <Event
                key={item.id + item.Active}
                item={item}
                current_user={this.props.current_user}
                onBlock={this.props.onBlock}
                onUnBlock={this.props.onUnBlock}
            />
        ));

    render() {
        const items = this.renderItems(this.props.data_list);
        const { page, totalPages, data_list } = this.props;

        return <>
            <div className="row">
                {data_list.length > 0
                    ? items
                    : <div id="notfound" className="w-100">
                        <div className="notfound">
                            <div className="notfound-404">
                                <div className="h1">No Results</div>
                            </div>
                        </div>
                    </div>}
            </div>
            <br />
            {totalPages > 1 &&
                <PagePagination
                    currentPage={page}
                    totalPages={totalPages}
                    callback={this.handlePageChange}
                />
            }
        </>
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        reset_events: () => dispatch(reset_events()),
        updateEventsFilters: (filter) => dispatch(updateEventsFilters(filter)),
        onBlock: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Blocked)),
        onUnBlock: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Active))

    }
};

export default connect(
    null,
    mapDispatchToProps
)(EventList);
