import React, { Component } from "react";
import { connect } from 'react-redux';
import { Field, reduxForm, formValueSelector } from "redux-form";

import { renderErrorMessage, renderTextField } from '../helpers/helpers';
//import AwesomeAlert from 'react-native-awesome-alerts';
import IconButton from "@material-ui/core/IconButton";
// import { Alert } from '@material-ui/lab';

const divStyle = {
    width:"90wh"
  };
const dSt={
    // padding:"-20px",
    marginLeft:"0"
} 

const ShowError=(props)=>{
    return(
        <div className="text-danger">
            <div>{props.error}</div>
        </div>
    )
}
export default class UnitOfMeasuringEdit extends Component {

    
    // handleSubmit = (e) => {
    //     e.preventDefault();
        
    //     console.log(this.props.unitName,this.props.shortName,this.props.add_unitOfMeasuring)
    //     const data={
    //         id:this.props.item.id,
    //         unitName:this.props.unitName,
    //         shortName:this.props.shortName
    //     }
    //     this.props.add_unitOfMeasuring(data);
    //          const {isAdded} = this.props.status;
    //     console.log("STATUS",this.props.status)

    //     //if (isAdded){
    //         this.props.reset();
    //         this.props.cancel();
    //     //}
    // }
    state=({
        unitError:null,
        shortError:null,
        showAlert: false
    })

  showAlert = () => {
    this.setState({
      showAlert: true
    });
  };
 
  hideAlert = () => {
    this.setState({
      showAlert: false
    });
  };
    
    componentDidMount = () => {
        let obj = JSON.parse('{"unitName":"' + this.props.item.unitName + '","shortName":"' + this.props.item.shortName + '"}')
        this.props.initialize(obj);
        
    }
    // componentDidUpdate=()=>{
    //     this.setErrors();
    // }
    setErrors=()=>{
       
        if(this.props.message){
            // console.log(renderErrorMessage(this.props.message, "ShortName"));
            // console.log(this.props.message)
            let resError=(JSON.parse(this.props.message)).errors; 
            // const{UnitName,ShortName,...other}=resError;
            //     console.log(resError);

            //console.log(resError[''])               
            if(resError){
                console.log(resError);
                const{UnitName='',ShortName='',...other}=resError;
                const errorArray=Object.keys(other).map(key=>
                    other[key]);
                //console.log(errorArray.join(" "));
               
                // console.log(JSON.parse(other))
                this.setState({
                    // ...this.state,
                    unitError:`${UnitName} ${errorArray.join(" ")}`,
                    shortError:ShortName+errorArray.join(" ")
                })
                }
        } 
       
    }
    showAlert=()=>{
        Alert.alert(
            'Alert Title',
            'My Alert Msg',
            [
              {
               text: 'Ask me later', 
               onPress: () => console.log('Ask me later pressed')
              },     
              {       
                text: 'Cancel',       
                onPress: () => console.log('Cancel Pressed'),       
                style: 'cancel',     
              },     
              {
                text: 'OK', 
                onPress: () => console.log('OK Pressed')
              },   
            ],   
            { cancelable: false }, 
          );
    }
    handleSubmit = (e) => {       
        e.preventDefault();  
           
        this.props.callback({unitName:this.props.newUnitName,
                            shortName:this.props.newShortName});
                            this.setErrors();
        // if(this.state.unitError==''&&this.state.shortError==''){
        //     console.log("SAVE")
        //     this.showAlert;

        // }
    }
    // renderShortError(){
    //     if(!this.props.message){
    //                 return null;
    //             } 
        
    //     let resError=(JSON.parse(this.props.message)).errors;        
    //     if(resError.ShortName){
    //         return renderErrorMessage(this.props.message, "ShortName");}
    //     return null;
    // }
    // renderUnitError=()=>{
    //     if(!this.props.message){
    //         return null;
    //     } 
    //     let resError=(JSON.parse(this.props.message)).errors;        
    //     if(resError.UnitName){
    //         return renderErrorMessage(this.props.message, "UnitName");}
    //     return null;
    // }
   
    // renderError() {
    //     //console.log(this.props.message)
    //     if(!this.props.message){
    //         return null;
    //     } 

    //     let resError=(JSON.parse(this.props.message)).errors;
    //     if(resError.UnitName)
    //     return renderErrorMessage(this.props.message, "UnitName");
    //     else if(resError.ShortName)
    //     return renderErrorMessage(this.props.message, "ShortName");
    // }

    render() {
        // console.log(this.props.initialUnitName);
        return <>
        {/* className="align-middle align-items-stretch" width="20%" */}
            {/* <td colSpan="3" className="align-middle" width="75%"> */}
            <td colSpan="3"  className="align-middle">
                <form className="w-100" id="save-form" onSubmit={this.handleSubmit}>
                    <div style={divStyle} className="d-flex flex justify-content-around ">  
                                     
                        <Field
                            className="form-control"
                            autoFocus
                            name="unitName"
                            label="Unit name"
                            defaultValue={this.props.item.unitName}                           
                            component={renderTextField} 
                                                       
                        />
                        {this.state.unitError?
                        <ShowError error={this.state.unitError}/>:
                                            <div></div>}
                        
                           
                        {/* <input aria-invalid="false" 
                        class="MuiInputBase-input MuiInput-input" 
                        name="unitName" placeholder="Unit name" 
                        type="text" value={this.props.item.unitName}></input> */}
                        
                         <Field                            
                            className="form-control"
                            // autoFocus
                            name="shortName"
                            label="Short name"
                            defaultValue={this.props.item.shortName}
                            component={renderTextField}
                        />
                        {/* <input aria-invalid="false" 
                        class="MuiInputBase-input MuiInput-input" 
                        name="unitName" placeholder="Unit name" 
                        type="text" value={this.props.item.shortName}></input> */}
                         
                        {/* {this.state.shortError}     */}
                        {this.state.shortError?
                        <ShowError error={this.state.shortError}/>:
                        <div></div>}
                        
                       
                    </div>
                </form>
                

            </td>
            {/* <td></td> */}
            {/* <td></td> */} 
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-success"  size="small" type="submit" form="save-form">
                        <i className="fa fa-check"></i>
                    </IconButton>   
                </div>
            </td>
            <td className="align-middle align-items-stretch" width="15%">
            <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
            </div>
            </td>
            {/* <Alert severity="error">This is an error alert — check it out!</Alert> */}
                {/* </div>  */}
                
                {/* <Alert iconMapping={{ success: <CheckCircleOutlineIcon fontSize="inherit" /> }}>
                This is a success alert — check it out!
                </Alert> */}
                {/* <AwesomeAlert
          show={showAlert}
          showProgress={false}
          title="AwesomeAlert"
          message="I have a message for you!"
          closeOnTouchOutside={true}
          closeOnHardwareBackPress={false}
          showCancelButton={true}
          showConfirmButton={true}
          cancelText="No, cancel"
          confirmText="Yes, delete it"
          confirmButtonColor="#DD6B55"
          onCancelPressed={() => {
            this.hideAlert();
          }}
          onConfirmPressed={() => {
            this.hideAlert();
          }}
        />       */}
            {/* // </td> */}
        </>
    }

}

const selector = formValueSelector("save-form")

const mapStateToProps = (state, props) => {
    return {
        //status:state.unitsOfMeasuring.isAdded,
        newUnitName: selector(state, "unitName"),
        newShortName: selector(state, "shortName"),
        initialUnitName: props.item.unitName,
        initialShortName:props.item.shortName
    };
};
// const mapDispatchToProps = (dispatch) => {
//     return {
//         add_unitOfMeasuring: () => dispatch(add_unitsOfMeasuring())
//     }
// };

UnitOfMeasuringEdit = connect(mapStateToProps, null)(UnitOfMeasuringEdit);

UnitOfMeasuringEdit = reduxForm({
    form: "save-form",
    enableReinitialize: true
})(UnitOfMeasuringEdit);

