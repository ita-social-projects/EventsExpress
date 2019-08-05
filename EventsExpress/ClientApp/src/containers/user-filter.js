import React, { Component } from 'react';
import { connect } from 'react-redux';
import { renderTextField } from '../components/helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import UsersFilters from '../components/users/UsersFilters';
import get_users from '../actions/users';

class UsersFilterWrapper extends Component {

    onSubmit = (filters) => {
        console.log(filters);
        var search_string = '';
        if (filters != null) {
            if (filters.search != null) {
                search_string += '&keyWord=' + filters.search;
            }
                    if (filters.role != null) {
                        search_string += '&Role=' + filters.role;
                    }
        }
        this.props.search(window.location.search + search_string);
        console.log(window.location.search + search_string)
        window.location.search += search_string;

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