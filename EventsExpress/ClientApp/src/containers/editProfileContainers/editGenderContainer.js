import React from "react";
import EditGender from "../../components/profile/editProfile/editGender";
import { connect } from "react-redux";
import edit_Gender from "../../actions/editProfile/editGender";

class EditGenderContainer extends React.Component {
    submit = value => {
        this.props.editGender(value);
    }



    render() {
        return <EditGender onSubmit={this.submit} />;
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