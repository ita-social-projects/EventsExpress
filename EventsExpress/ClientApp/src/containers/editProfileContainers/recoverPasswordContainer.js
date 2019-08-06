import React from "react";
import RecoverPassword from "../../components/recoverPassword/recoverPassword";
import { connect } from "react-redux";
import recover_Password from "../../actions/EditProfile/recoverPassword";

class RecoverPasswordContainer extends React.Component {
    submit = value => {
        console.log(value);
        this.props.recoverPassword(value);
    }

    render() {
        let { isRecoverPasswordPending, isRecoverPasswordSuccess, RecoverPasswordError } = this.props;
        console.log(this.submit)

        return <RecoverPassword onSubmit={this.submit} />;
    }
}

const mapStateToProps = state => {
    return state.recoverPassword;

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