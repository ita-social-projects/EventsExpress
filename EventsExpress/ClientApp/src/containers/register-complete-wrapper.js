import React, {Component} from 'react';
import { connect } from 'react-redux';
import RegisterBindAccount from "../components/register/register-bind-account";
import registerBindAccount from "../actions/register/register-bind-account-action";
import RegisterComplete from '../components/register/register-complete';
import registerComplete from "../actions/register/register-complete-action";

class RegisterCompleteWrapper extends Component {
    constructor(props) {
        super(props);
        this.profile = this.props.location.state.profile;
    }

    render() {
        return <>
            {this.profile !== undefined && this.profile.type !== undefined &&
                <RegisterBindAccount onSubmit={this.props.bind}/>
            }
            <RegisterComplete onSubmit={this.props.submit}/>
        </>
    }
}

const mapDispatchToProps = dispatch => ({
    bind: data => dispatch(registerBindAccount(data)),
    submit: data => dispatch(registerComplete(data))
});

export default connect(null, mapDispatchToProps)(RegisterCompleteWrapper);
