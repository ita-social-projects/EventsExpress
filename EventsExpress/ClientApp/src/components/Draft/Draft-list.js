import React, { Component } from 'react';
import { connect } from 'react-redux';
import DraftEventCard from './DraftEventCard';
import RenderList from '../event/RenderList'
import { change_event_status } from '../../actions/event/event-item-view-action';
import eventStatusEnum from '../../constants/eventStatusEnum';
import { createBrowserHistory } from 'history';
import { setSuccessAllert } from '../../actions/alert-action';

const history = createBrowserHistory({ forceRefresh: true });

class DraftList extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }
    handlePageChange = (page) => {
        this.props.get_drafts(page);
        this.setState({
            currentPage: page
        });
    };
        
    renderSingleItem = (item) => (
        <DraftEventCard
            key={item.id + item.isBlocked}
            item={item}
            current_user={this.props.current_user}
            onDelete={this.onDelete}
        />
    )

    onDelete = async (eventId, reason) => {
        await this.props.delete(eventId, reason);
        this.props.alert('Your event has been successfully deleted!');
        history.push(`/drafts`);
    }

    render(){
        return <>
            <RenderList {...this.props} renderSingleItem={this.renderSingleItem} handlePageChange={this.handlePageChange} />
            </>
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        alert: (msg) => dispatch(setSuccessAllert(msg)),
        delete: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Deleted)),
    }
};

export default connect(null, mapDispatchToProps)(DraftList);
