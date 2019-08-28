import React from "react";
import { connect } from "react-redux";
import ContactUs from "../components/contactUs/contactUs";
import contactUs from '../actions/contact-us';

class ContactUsContainer extends React.Component{

    render(){
        return<ContactUs/>
    }
}

const mapStateToProps=state=>{
    
};

const mapDispatchToProps=dispatch=>{
    return{
        contactUs:(data)=>dispatch(contactUs(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ContactUsContainer)