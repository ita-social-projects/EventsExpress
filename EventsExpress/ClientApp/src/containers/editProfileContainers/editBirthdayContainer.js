import React from "react";
import EditBirthday from "../../components/profile/editProfile/editBirthday";
import { connect } from "react-redux";
import editBirthday from "../../actions/EditProfile/editBirthday";

class EditBirthdayContainer extends React.Component {
    submit = value => {
        console.log(value);
        this.props.editBirthday(value.date);
    }



    render() {
        let { isEditBirthdayPending, isEditBirthdaySuccess, EditBirthdayError } = this.props;

        return <EditBirthday onSubmit={this.submit} />;
    }


}

const mapStateToProps = state => {
    return state.editBirthday;

};

const mapDispatchToProps = dispatch => {
    return {
        editBirthday: (date) => dispatch(editBirthday(date))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditBirthdayContainer);