import React from "react";
import { connect } from "react-redux";
import ContactAdmin from "../../components/contactAdmin/contactAdmin-component";
import contact_Admin from '../../actions/contactAdmin/contact-admin-add-action';

class ContactAdminContainer extends React.Component {
    submit = values => {
        return this.props.contactAdmin(values);
    };

    render() {
        return <ContactAdmin onSubmit={this.submit} user={this.props.user} />
    }
}

const mapStateToProps = (state) => ({
    contactAdmin: state.contactAdmin,
    user: state.user
});

const mapDispatchToProps = dispatch => {
    return {
        contactAdmin: (data) => dispatch(contact_Admin(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactAdminContainer)