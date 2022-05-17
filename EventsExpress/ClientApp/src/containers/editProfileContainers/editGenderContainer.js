import React from "react";
import { connect } from "react-redux";
import edit_Gender from "../../actions/redactProfile/gender-edit-action";
import { Field } from "redux-form";
import { renderSelectField } from "../../components/helpers/form-helpers";
import EditProfileHOC from "../../components/profile/editProfile/editProfilePropertyWrapper";

class EditGenderContainer extends React.Component {
    submit = value => {
       return this.props.editGender(value).then(this.props.close);
    }
    render() { 
        let Element = EditProfileHOC("EditGender");
        return <Element onSubmit={this.submit} onClose = {this.props.close} initialValues ={this.props}>
            <Field
                    minWidth={210}
                    name="gender"
                    component={renderSelectField}
                    label="Gender"
                >
                    <option aria-label="None" value="" />
                    <option value="1">Male</option>
                    <option value="2">Female</option>
                    <option value="3">Other</option>
            </Field>
        </Element>;
    }
}

const mapStateToProps = state => ({
    gender : state.user.gender
})
   
const mapDispatchToProps = dispatch => {
    return {
        editGender: (gender) => dispatch(edit_Gender(gender))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditGenderContainer);