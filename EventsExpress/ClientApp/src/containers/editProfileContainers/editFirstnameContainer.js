import React from 'react'
import { Component } from 'react'
import { connect } from 'react-redux'
import edit_Firstname from '../../actions/redactProfile/userFirstname-edit-action'
import EditProfilePropertyWrapper from '../../components/profile/editProfile/editProfilePropertyWrapper';
import { Field } from 'redux-form';
import { renderTextField } from '../../components/helpers/form-helpers';
import EditProfileHOC from '../../components/profile/editProfile/editProfilePropertyWrapper';
class EditFirstnameContainer extends Component {
  submit = (value) =>{
    return this.props.editFirstname(value).then(this.props.close);
  }

  render(){
    let Element = EditProfileHOC("EditFirstname");
    return <Element onSubmit ={this.submit} onClose ={this.props.close} initialValues={this.props}>
                <Field
                    name="firstName"
                    component={renderTextField}
                    label="FirstName"
                />
      </Element>
  }
}

const mapStateToProps = state => ({
  firstName : state.user.firstName
})

const mapDispatchToProps = dispatch =>{
  return{
    editFirstname : (name) => dispatch(edit_Firstname(name))
  }
}

export default connect(mapStateToProps,mapDispatchToProps)(EditFirstnameContainer)


