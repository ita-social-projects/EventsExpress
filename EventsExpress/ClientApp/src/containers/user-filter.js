import React, { Component } from 'react';
import { connect } from 'react-redux';
import UsersFilters from '../components/users/UsersFilters';
import {
    accountStatus,
    get_users,
    get_count,
    initialConnection,
    closeConnection,
    change_status } from '../actions/users/users-action';
import history from '../history';

const BLOCKED_USERS = "BLOCKED_USERS";
const ACTIVE_USERS = "ACTIVE_USERS";

class UsersFilterWrapper extends Component {

    componentDidMount() {
        this.props.get_count();
        this.props.get_count_of_blocked();
        this.props.get_count_of_unblocked();
        this.props.initialConnection();
    }

    componentWillUnmount = () => {
        this.props.closeConnection();
    }

    onSubmit = (filters) => {
        let search_string = '?page=1';
        let status;

        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
            if (filters.role != null) {
                search_string += '&Role=' + filters.role;
            }

            switch (filters.status) {
                case 'blocked':
                    search_string += '&Blocked=' + true;
                    this.props.get_count(accountStatus.Blocked);
                    status = BLOCKED_USERS;
                    break;
                case 'active':
                    search_string += '&Unblocked=' + true;
                    this.props.get_count(accountStatus.Activated);
                    status = ACTIVE_USERS;
                    break;
                default:
                    search_string += '&All=' + true;
                    this.props.get_count(accountStatus.All);
                    status = null;
            }

            this.props.changeStatus(status);

            if (filters.PageSize != null) {
                search_string += '&PageSize=' + filters.PageSize;
            }
        }

        this.props.search(search_string);
        history.push(window.location.pathname + search_string);
    }

    renderCount = (status) => {
        const { count, countOfBlocked, countOfUnblocked } = this.props;
        let countElement, label, total;

        switch(status)
        {
            case ACTIVE_USERS:
                label = "Active users:";
                total = countOfUnblocked;
                break;
            case BLOCKED_USERS:
                label = "Blocked users:";
                total = countOfBlocked;
                break;
            default:
                label = "All users:";
                total = count;
        }
        
        return <>
            <span className="ml-2">{label} {total}</span><br/>
        </>
    }
    
    render() {
        const { status } = this.props;

        return <>
            <UsersFilters onSubmit={this.onSubmit} />
            {this.renderCount(status)}
        </>
    }
}

const mapStateToProps = (state) => ({
    status: state.users.status,
    count: state.users.count,
    countOfBlocked: state.users.countOfBlocked,
    countOfUnblocked: state.users.countOfUnblocked
});

const mapDispatchToProps = (dispatch) => {
    return {
        changeStatus: (status) => dispatch(change_status(status)),
        closeConnection: () => dispatch(closeConnection()),
        initialConnection: () => dispatch(initialConnection()),
        get_count: (accountStatus) => dispatch(get_count(accountStatus)),
        search: (values) => dispatch(get_users(values))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(UsersFilterWrapper);