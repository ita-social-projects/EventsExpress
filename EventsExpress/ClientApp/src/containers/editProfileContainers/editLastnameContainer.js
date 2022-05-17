import React from 'react'
import { Component } from 'react';
import { connect } from 'react-redux';
import EditLastname from '../../components/profile/editProfile/editLastname';
import edit_Lastname from '../../actions/redactProfile/userLastname-edit-action';
import { renderTextField } from '../../components/helpers/form-helpers';
import { Field } from 'redux-form';
import EditProfileHOC from '../../components/profile/editProfile/editProfilePropertyWrapper';
import {EditProfilePropertyWrapper} from '../../components/profile/editProfile/editProfilePropertyWrapper'

class EditLastnameContainer extends Component{
    submit = value =>{
        return this.props.editLastname(value).then(this.props.close);
    }

    render(){
        let Element = EditProfileHOC("EditLastname");
        return <Element onSubmit ={this.submit} onClose = {this.props.close} initialValues ={this.props}>
            <Field
                    name="lastName"
                    component={renderTextField}
                    label="LastName"
                />
        </Element>
    }
}
const mapStateToProps = state => ( {
    lastName : state.user.lastName
})

  
const mapDispatchToProps = dispatch =>{
    return{
      editLastname : (name) => dispatch(edit_Lastname(name))
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(EditLastnameContainer)