import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reset_events } from '../../actions/event-list-action';
import DraftEventCard from './Draft-item';
import RenderList from '../event/RenderList'

class DraftList extends Component {
    handlePageChange = (page) => { };

    renderSingleItem = (item) => (
        <DraftEventCard
            key={item.id + item.isBlocked}
            item={item}
            current_user={this.props.current_user}
        />
    )

    render(){
        return <>
            <RenderList {...this.props} renderSingleItem={this.renderSingleItem} handlePageChange={this.handlePageChange} />
            </>
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        reset_events: () => dispatch(reset_events()),
    }
};

export default connect(
    null,
    mapDispatchToProps
)(DraftList);
