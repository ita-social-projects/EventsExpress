import React, { Component } from "react";
import { connect } from 'react-redux';
import { Field, reduxForm, formValueSelector } from "redux-form";

import { renderErrorMessage, renderTextField } from '../helpers/helpers';

import IconButton from "@material-ui/core/IconButton";


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
    componentDidMount = () => {
        let obj = JSON.parse('{"unitName":"' + this.props.item.unitName + '"}')
        this.props.initialize(obj);
        obj = JSON.parse('{"shortName":"' + this.props.item.shortName + '"}')
        this.props.initialize(obj)   
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.add_unitOfMeasuring({unitName:this.props.unitName,
                            shortName:this.props.shortName});
    }
    renderError() {
        if(!this.props.message){
            return null;
        }
        return renderErrorMessage(this.props.message, "Name");
    }

    render() {
        console.log(this.props.initialUnitName);
        return <>
            <td className="align-middle" width="75%">
                <form className="w-100" id="save-form" onSubmit={this.handleSubmit}>
                    <div className="d-flex flex-column justify-content-around ">                       
                        <Field
                            className="form-control"
                            autoFocus
                            name="unitName"
                            label="Unit name"
                            defaultValue={this.props.item.unitName}                           
                            component={renderTextField} 
                                                       
                        />
                        
                        {/* <input aria-invalid="false" 
                        class="MuiInputBase-input MuiInput-input" 
                        name="unitName" placeholder="Unit name" 
                        type="text" value={this.props.item.unitName}></input> */}

                         <Field                            
                            className="form-control"
                            autoFocus
                            name="shortName"
                            label="Short name"
                            defaultValue={this.props.item.shortName}
                            component={renderTextField}
                        />
                        {/* <input aria-invalid="false" 
                        class="MuiInputBase-input MuiInput-input" 
                        name="unitName" placeholder="Unit name" 
                        type="text" value={this.props.item.shortName}></input> */}
                         
                        {this.renderError()}    
                    </div>
                </form>
                

            </td>
            <td></td>
            <td></td>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-success"  size="small" type="submit" form="save-form">
                        <i className="fa fa-check"></i>
                    </IconButton>   

                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
                </div>        
            </td>
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

