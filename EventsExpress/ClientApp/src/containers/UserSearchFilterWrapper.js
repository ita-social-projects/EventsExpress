import React, { Component } from 'react';
import { connect } from 'react-redux';
import UserSearchFilter from '../components/users/UserSearchFilter';
import { get_SearchUsers } from '../actions/users';
import history from '../history';
class UserSearchFilterWrapper extends Component {

    onSubmit = (filters) => {
        var search_string = '?page=1';
        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
        }
        this.props.search(search_string);
        history.push(window.location.pathname + search_string);

    }

    render() {
        return <>
            <UserSearchFilter onSubmit={this.onSubmit} onReset={this.props.onReset}/>
        </>
    }
}

const mapStateToProps = (state) => ({
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_SearchUsers(values))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(UserSearchFilterWrapper);