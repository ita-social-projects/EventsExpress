import React from "react";
import { connect } from "react-redux";
import ContactAdminDetails from "../../components/contactAdmin/contactAdmin-details-component";
import change_issue_status from '../../actions/contactAdmin/contact-admin-issue-status-action';
import get_message_by_id from '../../actions/contactAdmin/contact-admin-item-action';
import issueStatusEnum from '../../constants/IssueStatusEnum';

class ContactAdminDetailsContainer extends React.Component {
    componentWillMount = () => {
        const { id } = this.props.match.params;
        this.props.get_message_by_id(id);
    }

    onResolve = () => {
        this.props.resolve(this.props.contactAdminData.messageId, this.props.contactAdminData.status);
    }

    onInProgress = () => {
        this.props.inProgress(this.props.contactAdminData.messageId, this.props.contactAdminData.status);
    }

    render() {
        return <ContactAdminDetails
            items={this.props.contactAdminData}
            onResolve={this.onResolve}
            onInProgress={this.onInProgress}
        />
    }
}

const mapStateToProps = (state) => ({
    contactAdminData: state.contactAdminItem.data
});

const mapDispatchToProps = dispatch => {
    return {
        get_message_by_id: (id) => dispatch(get_message_by_id(id)),
        resolve: (messageId) => dispatch(change_issue_status(messageId, issueStatusEnum.Resolve)),
        inProgress: (messageId) => dispatch(change_issue_status(messageId, issueStatusEnum.InProgress)),
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactAdminDetailsContainer)