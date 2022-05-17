import React, { Component, useEffect} from 'react'
import { connect } from 'react-redux'
import EditLocation from '../../components/profile/editProfile/editLocation'
import edit_Location from '../../actions/redactProfile/userLocation-edit-action'

class EditLocationContainer extends Component{

  submit = value =>{
    return this.props.editLocation(value.location).then(this.props.close);
  }
  render(){
    return <EditLocation isOpen = {this.props.isOpen} onClose ={this.props.close} onSubmit = {this.submit} initialValues = { {location : this.props.location}}/>
  }
  
}

const mapStateToProps = state =>({
  location : {...state.user.location}
})
const mapDispatchToProps = dispatch =>{
  return {
    editLocation: (location) => dispatch(edit_Location(location))
  };
}


export default connect(mapStateToProps, mapDispatchToProps)(EditLocationContainer)