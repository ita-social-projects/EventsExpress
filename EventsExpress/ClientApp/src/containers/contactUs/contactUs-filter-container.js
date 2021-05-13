import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import ContactUsFilter from '../../components/contactUs/contactUs-filter-status-component';
import filterHelper from '../../components/helpers/filterHelper';
import { withRouter } from "react-router";

class ContactUsFilterWrapper extends Component {

    onReset = () => {
        this.props.reset_filters();
        this.props.history.push(this.props.history.location.pathname + "?page=1")
    }

    onSubmit = (filters) => {
        filters = filterHelper.trimUndefinedKeys(filters);
        var filterCopy = { ...this.props.contactUsList.filter };
        Object.entries(filters).forEach(function ([key, value]) {
            switch (key) {
                case 'page':
                    filterCopy[key] = value;
                case 'dateCreated':
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

        if (filterCopy.x !== undefined && filterCopy.y !== undefined)
            this.props.history.push(this.props.history.location.pathname + queryString);
        else
            this.props.history.push(this.props.history.location.pathname)
    }

    buildInitialFormValues = () => {
        const filter = filterHelper.trimUndefinedKeys(this.props.contactUsList.filter);
        let values = Object.assign({}, filter);
        return values;
    };

    render() {
        const initialFormValues = this.buildInitialFormValues();
        return <>
            <ContactUsFilter
                onSubmit={this.onSubmit}
                onReset={this.onReset}
                form_values={this.props.form_values}
                current_user={this.props.current_user}
                initialFormValues={initialFormValues}
            />
        </>
    }
}

const mapStateToProps = (state) => ({
    contactUsList: state.contactUsList,
    form_values: getFormValues('contactUs-filter-form')(state),
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => {
    return {
        reset_filters: () => dispatch(reset('contactUs-filter-form')),
    }
};

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactUsFilterWrapper));
