import React from "react";
import { connect } from "react-redux";
import ContactUs from "../../components/contactUs/contactUs-component";
import contact_Us from '../../actions/contactUs/contact-us-add-action';

class ContactUsContainer extends React.Component {
    submit = values => {
        return this.props.contactUs(values);
    };

    render() {
        return <ContactUs onSubmit={this.submit} user={this.props.user} />
    }
}

const mapStateToProps = (state) => ({
    contactUs: state.contactUs,
    user: state.user
});

const mapDispatchToProps = dispatch => {
    return {
        contactUs: (data) => dispatch(contact_Us(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactUsContainer)