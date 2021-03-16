import React from "react";
import EditBirthday from "../../components/profile/editProfile/editBirthday";
import { connect } from "react-redux";
import editBirthday from "../../actions/editProfile/birthday-edit-action";

class EditBirthdayContainer extends React.Component {
    submit = value => {
        this.props.editBirthday(value);
    }
    render() {
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