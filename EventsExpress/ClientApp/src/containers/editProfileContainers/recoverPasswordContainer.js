import React from "react";
import RecoverPassword from "../../components/recoverPassword/recoverPassword";
import { connect } from "react-redux";
import recover_Password from "../../actions/redactProfile/password-recover-action";

class RecoverPasswordContainer extends React.Component {

    submit = value => {
        return this.props.recoverPassword(value);
    }

    render() {
        let { status } = this.props;



        return <>
            <RecoverPassword onSubmit={this.submit} />
            {status.isError !== undefined && status.isError &&
                <p className="text-success text-center">New password sent by your email.<br />Please use it to sign in.</p>
            }
        </>
    }
}

const mapStateToProps = state => {
    return {
        status: state.recoverPassword
    }
};

const mapDispatchToProps = dispatch => {
    return {
        recoverPassword: (data) => dispatch(recover_Password(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(RecoverPasswordContainer)