﻿import React from "react";
import EditGender from "../../components/profile/editProfile/editGender";
import { connect } from "react-redux";
import edit_Gender from "../../actions/redactProfile/gender-edit-action";

class EditGenderContainer extends React.Component {
    submit = value => {
       return this.props.editGender(value).then(this.props.close);
    }
    render() {
        return <EditGender onSubmit={this.submit} onClose = {this.props.close} />;
    }
}

const mapStateToProps = state => {
    return state.gender
};

const mapDispatchToProps = dispatch => {
    return {
        editGender: (gender) => dispatch(edit_Gender(gender))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditGenderContainer);