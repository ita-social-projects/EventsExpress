import React, { Component } from 'react';
import DraftEventCard from './DraftEventCard';
import RenderList from '../event/RenderList'

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
        />
    )

    render(){
        return <>
            <RenderList {...this.props} renderSingleItem={this.renderSingleItem} handlePageChange={this.handlePageChange} />
            </>
    }
}

export default DraftList;
