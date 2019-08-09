import React, { Component } from 'react';
import { connect } from 'react-redux';
import UserSearchFilter from '../components/users/UserSearchFilter';
import get_users from '../actions/users';
import history from '../history';
class UserSearchFilterWrapper extends Component {

    onSubmit = (filters) => {
        console.log(filters);
        var search_string = '?page=1';
        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
        }
        console.log(search_string);
        this.props.search(search_string);
        history.push(search_string);

    }

    render() {
        return <>
            <UserSearchFilter onSubmit={this.onSubmit} />
        </>
    }
}

const mapStateToProps = (state) => ({
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_users(values))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(UserSearchFilterWrapper);