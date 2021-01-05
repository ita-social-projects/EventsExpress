import React, { Component } from "react";
import IconButton from "@material-ui/core/IconButton";
import unitOfMeasuringItem from "../../containers/unitsOfMeasuring/unitOfMeasuring-item";
import delete_unitOfMeasuring from '../../actions/unitOfMeasuring/delete-unitOfMeasuring';
import { connect } from 'react-redux';
import { add_unitOfMeasuring } from '../../actions/unitOfMeasuring/add-unitOfMeasuring';
class EditUnit extends Component{
    state=({
        id:this.props.item.id,
        unitName:this.props.item.unitName,
        shortName:this.props.item.shortName
    });
    setUnitName=(e)=>{
        this.setState({
            unitName:e.target.value
        })
    }
    setShortName=(e)=>{
        this.setState({
            shortName:e.target.value
        })
    }
    saveChange=()=>{
        const data={
            id:this.state.id,
            unitName:this.state.unitName,
            shortName:this.state.shortName
        };
        console.log(this.props)
        this.props.add_unitOfMeasuring(data);
    }
    render(){
        const {add_unitOfMeasuring,item}=this.props;
        
        return(
            <>
           
            <input onChange={this.setUnitName} value={this.state.unitName}/>
                {/* <i className="fas fa-hashtag mr-1"></i>
                {this.props.item.unitName} */}
            
            {/* <td className="d-flex align-items-center justify-content-center">
                {this.props.item.shortName}
            </td> */}
            <input onChange={this.setShortName} value={this.state.shortName}/>
            <button onClick={this.saveChange}>Save change</button>
            </>
        )
    }
}
const mapStateToProps = (state) => ({
    //unitsOfMeasuring: state.unitsOfMeasuring
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_unitOfMeasuring: (data) => dispatch(add_unitOfMeasuring(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditUnit);