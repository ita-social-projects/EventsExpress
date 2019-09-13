import React from "react";
import { connect } from "react-redux";
import ContactUs from "../components/contactUs/contactUs";
import contact_Us from '../actions/contact-us';

class ContactUsContainer extends React.Component{
    submit=values=>{
        console.log('subm: ', values)
        this.props.contactUs(values);
    };

    render(){
        return<ContactUs onSubmit={this.submit}/>
    }
}

const mapStateToProps=state=>{
    return state.contactUs;
};

const mapDispatchToProps=dispatch=>{
    return{
        contactUs: (data) => dispatch(contact_Us(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactUsContainer)