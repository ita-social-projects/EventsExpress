
// import React, { Component } from "react";
// import { connect } from "react-redux";
// import IconButton from "@material-ui/core/IconButton";
// import UnitOfMeasuringItem  from '../../components/unitOfMeasuring/unitOfMeasuring-item'
// import { delete_unitOfMeasuring } from "../../actions/unitOfMeasuring/delete-unitOfMeasuring";
// import { add_unitOfMeasuring, set_edited_unitOfMeasuring } from "../../actions/unitOfMeasuring/add-unitOfMeasuring";
// import UnitOfMeasuringEdit from "../../components/unitOfMeasuring/unitOfMeasuring-edit";
// class UnitOfMeasuringItemWrapper extends Component {
//     render() {
//         const { delete_unitOfMeasuring, set_unitOfMeasuring_edited, edit_cansel} = this.props;
        
//         return <tr>
//                        <UnitOfMeasuringItem 
//                         item={this.props.item} 
//                         add_unitOfMeasuring={this.props.status}
                    
//                         //callback={set_unitOfMeasuring_edited} 
//                     />
//                {/* } */}
//                 <td className="align-middle align-items-stretch">
//                     <div className="d-flex align-items-center justify-content-center">
//                         <IconButton className="text-danger" size="small" onClick={delete_unitOfMeasuring}>
//                             <i className="fas fa-trash"></i>
//                         </IconButton>
//                     </div>
//                 </td>
               
                
//             </tr>
//     };
// }

// const mapStateToProps = state => { 
//     return{        
//         status: state.add_unitOfMeasuring,
//         editedUnitOfMeasuring: state.unitsOfMeasuring.editedUnitOfMeasuring
//     }
    
// };

// const mapDispatchToProps = (dispatch, props) => {
//     return {
//         delete_unitOfMeasuring: () => dispatch(delete_unitOfMeasuring(props.item.id)),
//         save_unitOdMeasuring: (data) => dispatch(add_unitOfMeasuring(data)),
//         set_unitOfMeasuring_edited: () => dispatch(set_edited_unitOfMeasuring(props.item.id)),
//         edit_cansel: () => dispatch(set_edited_unitOfMeasuring(null))
//     };
// };

// export default connect(
//     mapStateToProps,
//     mapDispatchToProps
// )(UnitOfMeasuringItemWrapper);
import React, { Component } from "react";
import { connect } from "react-redux";

import IconButton from "@material-ui/core/IconButton";
import UnitOfMeasuringItem from "../../components/unitOfMeasuring/unitOfMeasuring-item";
import UnitOfMeasuringEdit from "../../components/unitOfMeasuring/unitOfMeasuring-edit";

import { add_unitOfMeasuring } from "../../actions/unitOfMeasuring/add-unitOfMeasuring";
import { delete_unitOfMeasuring } from "../../actions/unitOfMeasuring/delete-unitOfMeasuring";
import { set_edited_unitOfMeasuring } from "../../actions/unitOfMeasuring/add-unitOfMeasuring";
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css'; // Import css


class UnitOfMeasuringItemWrapper extends Component {

    save = values => {
        if (values.unitName === this.props.item.unitName&&
            values.shortName===this.props.item.shortName) {
            this.props.edit_cansel();
        } else {
            this.props.save_unitOfMeasuring({ ...values, id: this.props.item.id });
        }
    };

    componentWillUpdate = () => {
        const {unitOfMeasuringError, isUnitOfMeasuringSuccess } = this.props.status;
        
        if (!unitOfMeasuringError && isUnitOfMeasuringSuccess){
            this.props.edit_cansel();
        }
    }
    isDeleteConfirm=()=>{
        const {unitName,shortName,id}=this.props.item;
        // let isDelete= window.confirm(`Do you really want to remove this Unit Of Measuring: Unit name is ${unitName} Short name is ${shortName}`);
        // if(isDelete){
        //     this.props.delete_unitOfMeasuring(id);
        // }
        confirmAlert({
            title: 'Deleting Unit',
            message: `Do you really want to remove this Unit Of Measuring: Unit name is ${unitName} Short name is ${shortName}`,
            buttons: [
              {
                label: 'Yes',
                onClick: () => {this.props.delete_unitOfMeasuring(id);}
                //                 alert('Unit has been deleted')}
              },
              {
                label: 'No',
                // onClick: () => alert('Click')
              }
            ]
          });
        
    }
    render() {
        const { delete_unitOfMeasuring, set_unitOfMeasuring_edited, edit_cansel} = this.props;
        
        return <tr>
                {(this.props.item.id === this.props.editedUnitOfMeasuring)
                    ? <UnitOfMeasuringEdit 
                        key={this.props.item.id + this.props.editedUnitOfMeasuring}
                        item={this.props.item} 
                        callback={this.save} 
                        cancel={edit_cansel} 
                        //add_unitOfMeasuring={this.props.save_unitOfMeasuring}
                        
                        message={this.props.status.unitOfMeasuringError}
                    />
                    : <UnitOfMeasuringItem 
                        item={this.props.item} 
                        callback={set_unitOfMeasuring_edited} 
                    />
                }
                <td className="align-middle align-items-stretch">
                    <div className="d-flex align-items-center justify-content-center" width="15%">
                        <IconButton className="text-danger" size="small" onClick={this.isDeleteConfirm}>
                            <i className="fas fa-trash"></i>
                        </IconButton>
                    </div>
                </td>
                
            </tr>
    };
}

const mapStateToProps = state => { 
    return{        
        status: state.add_unitOfMeasuring,
        editedUnitOfMeasuring: state.unitsOfMeasuring.editedUnitOfMeasuring
    }
    
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        delete_unitOfMeasuring: () => dispatch(delete_unitOfMeasuring(props.item.id)),
        save_unitOfMeasuring: (data) => dispatch(add_unitOfMeasuring(data)),
        set_unitOfMeasuring_edited: () => dispatch(set_edited_unitOfMeasuring(props.item.id)),
        edit_cansel: () => dispatch(set_edited_unitOfMeasuring(null))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UnitOfMeasuringItemWrapper);