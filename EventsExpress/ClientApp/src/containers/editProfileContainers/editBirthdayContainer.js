import React from "react";
import EditBirthday from "../../components/profile/editProfile/editBirthday";
import { connect } from "react-redux";
import editBirthday from "../../actions/redactProfile/birthday-edit-action";

class EditBirthdayContainer extends React.Component {
    submit = value => {
        return this.props.editBirthday(value).then(this.props.close);
    }
    render() {
        return <EditBirthday onSubmit={this.submit} onClose = {this.props.close} />;
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