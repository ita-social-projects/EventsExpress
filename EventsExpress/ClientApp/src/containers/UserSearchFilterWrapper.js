import React, { Component } from 'react';
import { connect } from 'react-redux';
import UserSearchFilter from '../components/users/UserSearchFilter';
import { get_SearchUsers, change_Filter } from '../actions/users';
import history from '../history';
import { getQueryStringByUsersFilter } from '../components/helpers/userHelper';


class UserSearchFilterWrapper extends Component {
    onSubmit = (filters) => {
        if (filters !== null) {
            this.props.change_Filter(filters);
        }
        const queryString = getQueryStringByUsersFilter(filters);
        this.props.search(queryString);
        history.push(window.location.pathname + queryString);
    }

    render() {
        return <>
            <UserSearchFilter
                onSubmit={this.onSubmit}
                onReset={this.props.onReset}
            />
        </>
    }
}

const mapStateToProps = (state) => ({
    users: state.users,
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_SearchUsers(values)),
        change_Filter: (values) => dispatch(change_Filter(values))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UserSearchFilterWrapper);
