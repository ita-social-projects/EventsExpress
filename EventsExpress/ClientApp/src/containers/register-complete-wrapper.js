import React, {Component} from 'react';
import { connect } from 'react-redux';
import RegisterBindAccount from "../components/register/register-bind-account";
import registerBindAccount from "../actions/register/register-bind-account-action";
import RegisterComplete from '../components/register/register-complete';
import registerComplete from "../actions/register/register-complete-action";

class RegisterCompleteWrapper extends Component {
    constructor(props) {
        super(props);
        this.profile = props.location.state.profile !== undefined ? this.props.location.state.profile : undefined
    }

    render() {
        return <>
            {typeof this.profile !== undefined && <RegisterBindAccount onSubmit={this.props.bind}/>}
            <RegisterComplete onSubmit={this.props.submit}/>
        </>
    }
}

const mapStateToProps = () => ({});

const mapDispatchToProps = (dispatch) => ({
    bind: (data) => dispatch(registerBindAccount(data)),
    submit: (data) => dispatch(registerComplete(data))
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(RegisterCompleteWrapper);
