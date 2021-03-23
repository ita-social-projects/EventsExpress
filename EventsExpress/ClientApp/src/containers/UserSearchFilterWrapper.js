import React, { Component } from 'react';
import { connect } from 'react-redux';
import UserSearchFilter from '../components/users/UserSearchFilter';
import { get_SearchUsers, change_Filter } from '../actions/users/users-action';


class UserSearchFilterWrapper extends Component {
    onSubmit = (filters) => {
        if (filters !== null) {
            this.props.change_Filter(filters);
        }
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
