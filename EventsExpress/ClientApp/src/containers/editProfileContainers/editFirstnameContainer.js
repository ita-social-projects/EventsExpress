import React from 'react'
import { Component } from 'react'
import { connect } from 'react-redux'
import edit_Firstname from '../../actions/redactProfile/userFirstname-edit-action'
import EditProfilePropertyWrapper from '../../components/profile/editProfile/editProfilePropertyWrapper';
import { Field } from 'redux-form';
import { renderTextField } from '../../components/helpers/form-helpers';
class EditFirstnameContainer extends Component {
  submit = (value) =>{
    return this.props.editFirstname(value).then(this.props.close);
  }

  render(){
    return <EditProfilePropertyWrapper onSubmit ={this.submit} onClose ={this.props.close}>
                <Field
                    name="firstName"
                    component={renderTextField}
                    label="FirstName"
                />
      </EditProfilePropertyWrapper>
  }
}

const mapStateToProps = state => {
  return state.editFirstname;
}

const mapDispatchToProps = dispatch =>{
  return{
    editFirstname : (name) => dispatch(edit_Firstname(name))
  }
}

export default connect(mapStateToProps,mapDispatchToProps)(EditFirstnameContainer)


