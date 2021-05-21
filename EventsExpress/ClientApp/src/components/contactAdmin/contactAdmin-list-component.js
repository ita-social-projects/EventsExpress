import React, { Component } from 'react';
import ContactAdminItemWrapper from '../../containers/contactAdmin/contactAdmin-item-container';
import RenderList from '../event/RenderList'
import { parse as queryStringParse } from 'query-string';
import filterHelper from '../helpers/filterHelper';
import { withRouter } from "react-router";

class ContactAdminList extends Component {

    handlePageChange = (page) => {
        if (this.props.history.location.search == "")
            this.props.history.push(this.props.history.location.pathname + `?page=${page}`);
        else {
            const queryStringToObject = queryStringParse(this.props.history.location.search);
            queryStringToObject.page = page;
            this.props.history.location.search = filterHelper.getQueryStringByFilter(queryStringToObject);
            this.props.history.push(this.props.history.location.pathname + this.props.history.location.search);
        }
    };

    renderSingleItem = (item) => (
        <ContactAdminItemWrapper
            key={item.messageId + item.status}
            item={item}
        />
    )

    render() {

        return (
            <>
                <tr className="bg-light text-dark font-weight-bold text-center">
                    <td className="justify-content-center">Title</td>
                    <td className="d-flex align-items-center justify-content-center">Date created</td>
                    <td className="justify-content-center">Status</td>
                    <td className="justify-content-center">Details</td>
                </tr>
                <RenderList {...this.props} renderSingleItem={this.renderSingleItem}
                    handlePageChange={this.handlePageChange} />
            </>);
    }
}
export default withRouter(ContactAdminList);




