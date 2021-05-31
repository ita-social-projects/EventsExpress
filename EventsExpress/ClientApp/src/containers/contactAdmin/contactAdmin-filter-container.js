import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import ContactAdminFilter from '../../components/contactAdmin/contactAdmin-filter-component';
import filterHelper from '../../components/helpers/filterHelper';
import { withRouter } from "react-router";

class ContactAdminFilterWrapper extends Component {

    onReset = () => {
        this.props.resetFilters();
        this.props.history.push(this.props.history.location.pathname + "?page=1")
    }

    onSubmit = (filters) => {
        filters = filterHelper.trimUndefinedKeys(filters);
        var filterCopy = { ...this.props.contactAdminList.filter };
        Object.entries(filters).forEach(function ([key, value]) {
            switch (key) {
                case 'page':
                    filterCopy[key] = value;
                    break;
                case 'dateFrom':
                    filterCopy[key] = new Date(value).toDateString();
                    break;
                case 'dateTo':
                    filterCopy[key] = new Date(value).toDateString();
                    break;
                case 'status':
                    filterCopy[key] = value;
                    break;
                default:
                    filterCopy[key] = value;
                    break;
            }
        }.bind(this));
        const queryString = filterHelper.getQueryStringByFilter(filterCopy);

        this.props.history.push(this.props.history.location.pathname + queryString);
    }

    buildInitialFormValues = () => {
        const filter = filterHelper.trimUndefinedKeys(this.props.contactAdminList.filter);
        return Object.assign({}, filter);
    };

    render() {
        const initialFormValues = this.buildInitialFormValues();
        return <>
            <ContactAdminFilter
                onSubmit={this.onSubmit}
                onReset={this.onReset}
                form_values={this.props.form_values}
                initialFormValues={initialFormValues}
            />
        </>
    }
}

const mapStateToProps = (state) => ({
    contactAdminList: state.contactAdminList,
    form_values: getFormValues('contactAdmin-filter-form')(state),
});

const mapDispatchToProps = (dispatch) => {
    return {
        resetFilters: () => dispatch(reset('contactAdmin-filter-form')),
    }
};

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactAdminFilterWrapper));
