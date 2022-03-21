import React, {Component} from 'react';
import {connect} from 'react-redux';
import RegisterComplete from '../components/register/register-complete';
import registerComplete from "../actions/register/register-complete-action";

class RegisterCompleteWrapper extends Component {
    constructor(props) {
        super(props);
        this.profile = this.props.location.state.profile;
    }

    render() {
        return <>
            <RegisterComplete onSubmit={this.props.submit}/>
        </>
    }
}

const mapDispatchToProps = dispatch => ({
    submit: data => dispatch(registerComplete(data))
});

export default connect(null, mapDispatchToProps)(RegisterCompleteWrapper);
