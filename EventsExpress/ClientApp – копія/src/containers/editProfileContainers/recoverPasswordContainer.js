import React from "react";
import RecoverPassword from "../../components/recoverPassword/recoverPassword";
import { connect } from "react-redux";
import recover_Password from "../../actions/EditProfile/recoverPassword";

class RecoverPasswordContainer extends React.Component {
    componentDidUpdate(prevOps, prevState) {
        if (!this.props.RecoverPasswordError && this.props.isRecoverPasswordSuccess) {
            this.props.handleClose();
        }
    }

    submit = value => {
        this.props.recoverPassword(value);
    }

    render() {
        let { status } = this.props;



        return <>
            <RecoverPassword onSubmit={this.submit} />
            {status.isSucces &&
                <p className="text-success text-center">New password sent by your email.<br />Please use it to sign in.</p>

            }
            {
                status.isError &&
                <p className="text-danger text-center">{status.isError}</p>
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