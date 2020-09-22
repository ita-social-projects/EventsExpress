import React from "react";
import ChangePassword from "../../components/profile/editProfile/ChangePassword";
import { connect } from "react-redux";
import changePassword, { setChangePasswordError, setChangePasswordPending, setChangePasswordSuccess } from "../../actions/EditProfile/changePassword";
import { reset } from 'redux-form';

class ChangePasswordContainer extends React.Component {
    submit = value => {
        this.props.changePassword(value);
    }

    render() {
        let { isEditBirthdayPending, isEditBirthdaySuccess, EditBirthdayError } = this.props;
        return <ChangePassword onSubmit={this.submit} />;
    }
}

const mapStateToProps = state => {
    return state.changePassword;

};

const mapDispatchToProps = dispatch => {
    return {
        changePassword: (date) => dispatch(changePassword(date)),
        reset: () => {
            dispatch(reset('ChangePassword'));
        }
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ChangePasswordContainer);
