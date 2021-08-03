import React, { Component } from 'react';
import { connect } from 'react-redux';
import UsersFilters from '../components/users/UsersFilters';
import history from '../history';
import { parse } from 'query-string';
import {
    accountStatus,
    get_users,
    get_count,
    initialConnection,
    closeConnection,
    change_status
} from '../actions/users/users-action';

class UsersFilterWrapper extends Component {

    componentDidMount() {
        const { Unblocked, Blocked } = parse(this.props.location.search);
        let status;

        if(Unblocked) {
            status = accountStatus.Activated;
        } else if (Blocked) {
            status = accountStatus.Blocked;
        } else {
            status = accountStatus.All;
        }

        this.props.changeStatus(status);
        this.props.get_count(status);
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
                    status = accountStatus.Blocked;
                    break;
                case 'active':
                    search_string += '&Unblocked=' + true;
                    status = accountStatus.Activated;
                    break;
                default:
                    search_string += '&All=' + true;
                    status = accountStatus.All;
            }

            this.props.changeStatus(status);
            this.props.get_count(status);

            if (filters.PageSize != null) {
                search_string += '&PageSize=' + filters.PageSize;
            }
        }

        this.props.search(search_string);
        history.push(window.location.pathname + search_string);
    }

    renderCount = (status) => {
        const { count } = this.props;
        let label;

        switch (status) {
            case accountStatus.Activated:
                label = 'Active users:';
                break;
            case accountStatus.Blocked:
                label = 'Blocked users:';
                break;
            default:
                label = 'All users:';
        }

        return <>
            <span className="ml-2">{label} {count}</span><br />
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
    count: state.users.count
});

const mapDispatchToProps = (dispatch) => {
    return {
        changeStatus: (status) => dispatch(change_status(status)),
        closeConnection: () => dispatch(closeConnection()),
        initialConnection: () => dispatch(initialConnection()),
        get_count: (status) => dispatch(get_count(status)),
        search: (values) => dispatch(get_users(values))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(UsersFilterWrapper);
