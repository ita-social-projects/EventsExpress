// import React, { Component } from "react";
// import IconButton from "@material-ui/core/IconButton";
// import unitOfMeasuringItem from "../../containers/unitsOfMeasuring/unitOfMeasuring-item";
// import delete_unitOfMeasuring from '../../actions/unitOfMeasuring/delete-unitOfMeasuring';
// import { connect } from 'react-redux';
// import { add_unitOfMeasuring } from '../../actions/unitOfMeasuring/add-unitOfMeasuring';
// import EditUnit from './edit-unit'



// // const EditUnit = ( ) => {
// //     return (
// //      <div className="Question">
// //       <p>KKKK</p>
// //      </div>
// //     )
// //    };
// export default class UnitOfMeasuringItem extends Component {
//     state=({
//         editUnit:false 
//     })
//     displayEditedUnit = () => {
//         this.setState({
//             editUnit: !this.state.editUnit
//         });
       
//     }
    
//     render() {
//         const { item,callback} = this.props;
        
        
//         return (<>
//             <td>
//                 <i className="fas fa-hashtag mr-1"></i>
//                 {item.unitName}
//             </td>
//             <td className="d-flex align-items-center justify-content-center">
//                 {item.shortName}
//             </td>
//             <td className="align-middle align-items-stretch">
//                 <div className="d-flex align-items-center justify-content-center">
//                     <IconButton  className="text-info"  size="small" onClick={this.displayEditedUnit}>
//                         <i className="fas fa-edit"></i>
//                     </IconButton>
//                 </div>
//                 {this.state.editUnit?<EditUnit item={item}/>:null}
//             </td>
//             {/* <td className="align-middle align-items-stretch">
//                 <div className="d-flex align-items-center justify-content-center">
//                     <IconButton  className="text-info"  size="small" onClick={callback}>
//                         <i className="fas fa-edit"></i>
//                     </IconButton>
//                 </div>
//             </td> */}
//         </>);
//     }
// }

import React, { Component } from "react";
import IconButton from "@material-ui/core/IconButton";
import unitOfMeasuringItem from "../../containers/unitsOfMeasuring/unitOfMeasuring-item";
import delete_unitOfMeasuring from '../../actions/unitOfMeasuring/delete-unitOfMeasuring';
import { connect } from 'react-redux';
import { add_unitOfMeasuring } from '../../actions/unitOfMeasuring/add-unitOfMeasuring';
import EditUnit from './edit-unit'



// const EditUnit = ( ) => {
//     return (
//      <div className="Question">
//       <p>KKKK</p>
//      </div>
//     )
//    };
export default class UnitOfMeasuringItem extends Component {
    state=({
        editUnit:false 
    })
    displayEditedUnit = () => {
        this.setState({
            editUnit: !this.state.editUnit
        });
       
    }
    
    render() {
        const { item,callback} = this.props;
        //console.log(this.props)
        
        
        return (<>
            <td>
                <i className="fas fa-hashtag mr-1"></i>
                {item.unitName}
            </td>
            <td className="d-flex align-items-center justify-content-left">
                {item.shortName}
            </td>
           
            {/* <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-info"  size="small" onClick={this.displayEditedUnit}>
                        <i className="fas fa-edit"></i>
                    </IconButton>
                </div>
                {this.state.editUnit?<EditUnit item={item}/>:null}
            </td> */}
            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-info"  size="small" onClick={()=>this.props.callback(item.id)}>
                        <i className="fas fa-edit"></i>
                    </IconButton>
                </div>
            </td>
        </>);
    }
}
