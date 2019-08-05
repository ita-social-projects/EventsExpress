import React, { Component } from 'react';
import { connect } from 'react-redux';
import { renderTextField } from '../components/helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import UsersFilters from '../components/users/UsersFilters';
import get_users from '../actions/users';
import history from '../history';
class UsersFilterWrapper extends Component {
   
    onSubmit = (filters) => {
        console.log(filters);
        var search_string = '?page=1';
        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
            if (filters.role != null) {
                search_string += '&Role=' + filters.role;
            }
            if (filters.blocked == true) {
                search_string += '&Blocked=' + filters.blocked;
            }
            if (filters.unblocked == true) {
                search_string += '&UnBlocked=' + filters.unblocked;
            }
        }
        console.log(search_string);
        this.props.search(search_string);
        history.push(search_string);

    }

    render() {
        return <>
            <UsersFilters onSubmit={this.onSubmit} />    
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

        export default connect(mapStateToProps, mapDispatchToProps)(UsersFilterWrapper);