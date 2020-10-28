import React, { Component } from 'react';
import { connect } from 'react-redux';
import { stringify as stringifyQueryString } from 'query-string';
import UserSearchFilter from '../components/users/UserSearchFilter';
import { get_SearchUsers } from '../actions/users';
import history from '../history';

class UserSearchFilterWrapper extends Component {
    onSubmit = (filters) => {
        // var search_string = '?page=1';
        if (filters != null) {
            if (filters.keyWord != null) {
                this.props.events.searchParams['keyWord'] = filters.keyWord;
                // search_string += '&keyWord=' + filters.search;
            }
        }
        let search_string = `?${stringifyQueryString(this.props.events.searchParams)}`;
        this.props.search(search_string);
        history.push(window.location.pathname + search_string);
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
    events: state.events,
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_SearchUsers(values))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(UserSearchFilterWrapper);
