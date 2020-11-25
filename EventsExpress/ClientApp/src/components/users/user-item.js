import React, { Component } from 'react';
import UserInfoCard from '../user-info/User-info-card';
import PagePagination from '../shared/pagePagination';

export default class UserItemList extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page, e) => {
        this.props.callback(window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + page));
        this.setState({
            currentPage: page
        });
    };


    renderUsers = (arr) => {
        return arr.map(user =>
            <UserInfoCard key={user.id} user={user} />);
    }

    render() {
        const { page, totalPages } = this.props;
        return <>
            {this.renderUsers(this.props.users)}
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
