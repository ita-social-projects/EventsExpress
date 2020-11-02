import React, { Component } from 'react';
import { connect } from 'react-redux';
import { stringify as queryStringStringify } from 'query-string';
import UserSearchFilter from '../components/users/UserSearchFilter';
import { get_SearchUsers } from '../actions/users';
import history from '../history';

class UserSearchFilterWrapper extends Component {
    onSubmit = (filters) => {
        if (filters !== null) {
            if (filters.keyWord !== null) {
                this.props.events.filter['keyWord'] = filters.keyWord;
            }
        }
        let queryString = `?${queryStringStringify(
            this.props.events.filter,
            { arrayFormat: 'index' }
        )}`;
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
    events: state.events,
});

const mapDispatchToProps = (dispatch) => {
    return {
        search: (values) => dispatch(get_SearchUsers(values))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UserSearchFilterWrapper);
