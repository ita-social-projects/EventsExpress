import React, { Component } from 'react';
import './users.css';
import UserInfoWrapper from '../../containers/user-info';
import PagePagination from '../shared/pagePagination';

export default class Users extends Component {
    constructor(props) {
        super(props);
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
        const reducer = (accumulator, currentValue) => accumulator + currentValue.id;
        const key = user => user.id + user.isBlocked + user.roles.reduce(reducer)
        return arr.map(user =>
            <UserInfoWrapper
                key={key(user)}
                user={user}
            />
        );
    }

    render() {
        const { page, totalPages } = this.props;
        return <>
            <table className="table">
                <tbody>
                    {this.renderUsers(this.props.users)}
                </tbody>
            </table>
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
