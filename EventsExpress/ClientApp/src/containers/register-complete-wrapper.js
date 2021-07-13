import React, {Component} from 'react';
import {connect} from 'react-redux';
import RegisterComplete from '../components/register/register-complete';
import registerComplete from "../actions/register/register-complete-action";

class RegisterCompleteWrapper extends Component {
    constructor(props) {
        super(props);
        this.profile = props.location.state.profile !== undefined ? this.props.location.state.profile : undefined
    }

    render() {
        return <>
            <RegisterComplete onSubmit={this.props.submit} profile={this.profile}/>
        </>
    }
}

const mapStateToProps = () => ({});

const mapDispatchToProps = (dispatch) => ({
    submit: (data) => dispatch(registerComplete(data))
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(RegisterCompleteWrapper);
