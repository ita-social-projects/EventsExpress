import React from 'react'
import { Component } from 'react';
import { connect } from 'react-redux';
import EditLastname from '../../components/profile/editProfile/editLastname';
import edit_Lastname from '../../actions/redactProfile/userLastname-edit-action';


class EditLastnameContainer extends Component{
    submit = value =>{
        return this.props.editLastname(value).then(this.props.close);
    }

    render(){
        return <EditLastname onSubmit ={this.submit} onClose = {this.props.close}/>
    }
}
const mapStateToProps = state => {
    return state.editLastname;
}
  
const mapDispatchToProps = dispatch =>{
    return{
      editLastname : (name) => dispatch(edit_Lastname(name))
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(EditLastnameContainer)