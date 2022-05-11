import React from 'react'
import { Component } from 'react'
import { connect } from 'react-redux'
import EditFirstname from '../../components/profile/editProfile/editFirstname';
import edit_Firstname from '../../actions/redactProfile/userFirstname-edit-action'

class EditFirstnameContainer extends Component {
  submit = (value) =>{
    return this.props.editFirstname(value).then(this.props.close);
  }

  render(){
    return <EditFirstname onSubmit ={this.submit} onClose = {this.props.close}/>
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


