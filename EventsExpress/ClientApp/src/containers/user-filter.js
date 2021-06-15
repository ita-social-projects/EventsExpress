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

class UsersFilterWrapper extends Component {

    componentDidMount() {
        let status = accountStatus.All;

        this.props.get_count(status);
        this.props.changeStatus(status);
        this.props.initialConnection(this.props.status);
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
                    status = accountStatus.Blocked;
                    this.props.get_count(status);
                    break;
                case 'active':
                    search_string += '&Unblocked=' + true;
                    status = accountStatus.Activated;
                    this.props.get_count(status);
                    break;
                default:
                    search_string += '&All=' + true;
                    status = accountStatus.All;
                    this.props.get_count(status);
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
            case accountStatus.Activated:
                label = "Active users:";
                total = countOfUnblocked;
                break;
            case accountStatus.Blocked:
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