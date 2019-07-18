import React from "react";
import EditGender from "../../components/profile/editProfile/editGender";
import { connect } from "react-redux";
import editGender from "../../actions/EditProfile/EditGender";

class EditGenderContainer extends React.Component {
    submit = value => {
        console.log(value);
        this.props.editGender(value.gender);
    }



    render() {
        let { isEditGenderPending, isEditGenderSuccess, EditGenderError } = this.props;

        return <EditGender onSubmit={this.submit} />;
    }


}

const mapStateToProps = state => {
    return state.editGender;

};

const mapDispatchToProps = dispatch => {
    return {
        editGender: (gender) => dispatch(editGender(gender))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditGenderContainer);