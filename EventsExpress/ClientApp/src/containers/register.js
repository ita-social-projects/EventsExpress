import React from 'react';
import Register from '../components/register';
import { connect } from 'react-redux';
import register from '../actions/register/register-action';
import { handleFormError } from '../components/helpers/action-helpers';

class RegisterWrapper extends React.Component {
    componentDidUpdate(prevProps, prevState) {
        if (!this.props.registerError && this.props.isRegisterSuccess) {
            this.props.handleClose();
        }
    }

    submit = async values => {
        await this.props.register(values.email, values.password);
    };

    render() {
        return <>
            <Register onSubmit={this.submit}/>
        </>;
    }
}

const mapStateToProps = state => {
    return state.register;
}

const mapDispatchToProps = dispatch => {
    return {
        handleFormError: error => dispatch(handleFormError(error)),
        register: (email, password) => dispatch(register(email, password))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(RegisterWrapper);