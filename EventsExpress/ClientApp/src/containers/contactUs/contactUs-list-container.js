import React, { Component } from 'react';
import { withRouter } from "react-router";
import { getFormValues } from 'redux-form';
import { connect } from 'react-redux';
import { parse as queryStringParse } from 'query-string';
import ContactUsList from '../../components/contactUs/contactUs-list-component';
import Spinner from '../../components/spinner';
import getIssues from '../../actions/contactUs/contact-us-list-action';
import filterHelper from '../../components/helpers/filterHelper';


class ContactUsListWrapper extends Component {
    constructor(props) {
        super(props);
        this.objCurrentQueryParams = Object.create(null);
        this.prevQueryStringSearch = "";
    }

    componentDidMount() {
        this.setSearchParamsToContactUsFilter(this.props.history.location.search);
        const queryString = filterHelper.getQueryStringByFilter(this.objCurrentQueryParams);
        this.props.getIssues(queryString);
    }

    componentDidUpdate() {
        if (this.props.history.location.search != this.prevQueryStringSearch) {
            this.prevQueryStringSearch = this.props.history.location.search;
            this.props.getIssues(this.props.history.location.search);
        }
    }

    setSearchParamsToContactUsFilter = search => {
        var filterCopy = { ...this.props.contactUsList.filter };
        this.objCurrentQueryParams = queryStringParse(search);

        Object.entries(this.objCurrentQueryParams).forEach(function ([key, value]) {
            filterCopy[key] = value;
        }.bind(this));
        this.objCurrentQueryParams = filterHelper.trimUndefinedKeys(filterCopy);
    }

    render() {
        const { data, isPending } = this.props.contactUsList;
        const { items } = this.props.contactUsList.data;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                    {!isPending
                        ? <ContactUsList
                            data_list={items}
                            filter={this.props.contactUsList.filter}
                            page={data.pageViewModel.pageNumber}
                            totalPages={data.pageViewModel.totalPages}
                        />
                        : null}
                </tbody>
            </table>
            {isPending ? <Spinner /> : null}
        </div>
    }
}

const mapStateToProps = (state) => {
    return {
        contactUsList: state.contactUsList,
        form_values: getFormValues('contactUs-filter-form')(state),
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        getIssues: (filter) => dispatch(getIssues(filter)),
    }
};

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactUsListWrapper));
