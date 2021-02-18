import React, { Component } from 'react';
import CommentItemWrapper from '../../containers/delete-comment';
import PagePagination from '../shared/pagePagination';

export default class CommentList extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page) => {
        this.props.callback(this.props.evId, page);
        this.setState({
            currentPage: page
        });
    };

    renderItems = arr => arr.map(item => <CommentItemWrapper key={item.id} item={item} />)

    render() {
        const { data_list } = this.props;
        const items = this.renderItems(data_list);
        const { page, totalPages } = this.props;

        return <>
            {items}
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