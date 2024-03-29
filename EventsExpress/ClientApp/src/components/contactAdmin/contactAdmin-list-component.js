﻿import React, { Component } from "react";
import ContactAdminItemWrapper from "../../containers/contactAdmin/contactAdmin-item-container";
import RenderIssuesList from "./renderIssuesList";
import { parse as queryStringParse } from "query-string";
import filterHelper from "../helpers/filterHelper";
import { withRouter } from "react-router";

class ContactAdminList extends Component {
  pageChange = (page) => {
    const history = this.props.history;
    if (history.location.search == "")
      history.push(history.location.pathname + `?page=${page}`);
    else {
      const queryStringInObject = queryStringParse(history.location.search);
      queryStringInObject.page = page;
      history.location.search =
        filterHelper.getQueryStringByFilter(queryStringInObject);
      history.push(history.location.pathname + history.location.search);
    }
  };

  renderSingleIssue = (item) => (
    <ContactAdminItemWrapper key={item.messageId + item.status} item={item} />
  );

  render() {
    return (
      <>
        {this.props.data_list > 0 ? (
          <tr className="bg-light text-dark font-weight-bold text-center">
            <td className="justify-content-center">Title</td>
            <td className="d-flex align-items-center justify-content-center">
              Date created
            </td>
            <td className="justify-content-center">Status</td>
            <td className="justify-content-center">Details</td>
            <RenderIssuesList
              {...this.props}
              renderSingleIssue={this.renderSingleIssue}
              handlePageChange={this.pageChange}
            />
          </tr>
        ) : (
          ""
        )}
        {this.props.data_list < 1 ? (
          <RenderIssuesList
            {...this.props}
            renderSingleIssue={this.renderSingleIssue}
            handlePageChange={this.pageChange}
          />
        ) : (
          ""
        )}
      </>
    );
  }
}
export default withRouter(ContactAdminList);
