import React from "react";
import { connect } from "react-redux";
import ContactUsDetails from "../../components/contactUs/contactUs-details-component";
import change_issue_status from '../../actions/contactUs/contact-us-issue-status-action';
import get_message_by_id from '../../actions/contactUs/contact-us-item-action';
import issueStatusEnum from '../../constants/IssueStatusEnum';

class ContactUsDetailsContainer extends React.Component {
    componentWillMount = () => {
        const { id } = this.props.match.params;
        this.props.get_message_by_id(id);
    }

    onResolve = () => {
        this.props.resolve(this.props.contactUsData.messageId, this.props.contactUsData.status);
    }

    onInProgress = () => {
        this.props.inProgress(this.props.contactUsData.messageId, this.props.contactUsData.status);
    }

    render() {
        return <ContactUsDetails
            items={this.props.contactUsData}
            onResolve={this.onResolve}
            onInProgress={this.onInProgress}
        />
    }
}

const mapStateToProps = (state) => ({
    contactUsData: state.contactUsItem.data
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
)(ContactUsDetailsContainer)