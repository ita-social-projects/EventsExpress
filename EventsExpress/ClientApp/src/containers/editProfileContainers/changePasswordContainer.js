import React from "react";
import ChangePassword from "../../components/profile/editProfile/ChangePassword";
import { connect } from "react-redux";
import changePassword from "../../actions/EditProfile/changePassword";

class ChangePasswordContainer extends React.Component {
    submit = value => {
        console.log(value);
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
        changePassword: (date) => dispatch(changePassword(date))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ChangePasswordContainer);