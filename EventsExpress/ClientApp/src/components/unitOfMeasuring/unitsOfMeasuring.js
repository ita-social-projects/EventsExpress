import React, { Component } from 'react';
import UnitOfMeasuringAddWrapper from '../../containers/unitsOfMeasuring/unitOfMeasuring-add';
//import CategoryListWrapper from '../../containers/categories/category-list';
import Spinner from '../spinner';
import UnitOfMeasuringListWrapper  from '../../containers/unitsOfMeasuring/unitOfMeasuring-list';
import { connect } from 'react-redux';
import get_unitsOfMeasuring from '../../actions/unitOfMeasuring/unitsOfMeasuring-list';

class UnitsOfMeasuring extends Component {

    componentWillMount = () => this.props.get_unitsOfMeasuring();

    render() {
        console.log('unit of measuring', this.props)
        const { unitsOfMeasuring } = this.props;
        console.log('unit', unitsOfMeasuring)
        //return (
            return <div>
                <table className="table w-75 m-auto">
                    <tbody>
                        <UnitOfMeasuringAddWrapper 
                            item={{id: "00000000-0000-0000-0000-000000000000",unitName:"",shortName:""}} 
                        />
                        {!unitsOfMeasuring.isPending ? <UnitOfMeasuringListWrapper data={unitsOfMeasuring.units} /> : null }
                    </tbody>
                </table> 
                {unitsOfMeasuring.isPending ? <Spinner/> : null}
            </div>
            // <>
            // <div>
            //     <table className="table w-75 m-auto">
            //         <tbody>
            //             <UnitOfMeasuringAddWrapper 
            //                 item={{id: "00000000-0000-0000-0000-000000000000",unitName:"",shortName:""}} 
            //             />                        
            //             {/* {!unitsOfMeasuring.isPending ? <UnitOfMeasuringListWrapper data={unitsOfMeasuring.units} /> : <Spinner/> }   */}
            //             {/* {<UnitOfMeasuringListWrapper data={unitsOfMeasuring.units}/>} */}
            //         </tbody>
            //     </table> 
            //     {/* {unitsOfMeasuring.isPending ? <Spinner/> : null} */}
            // </div>
                
            // </>
        //);

    }
}

const mapStateToProps = (state) => ({
    unitsOfMeasuring: state.unitsOfMeasuring
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring())
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UnitsOfMeasuring);
//


// import React, { Component } from 'react';
// import Spinner from '../spinner';
// import UnitOfMeasuringListWrapper  from '../../containers/unitsOfMeasuring/unitOfMeasuring-list';
// import { connect } from 'react-redux';
// import { add_unitOfMeasuring } from '../../actions/unitOfMeasuring/add-unitOfMeasuring';
// class AddUnit extends Component{
//     state=({
//         unitName:"",
//         shortName:"",
//         emptyUnitName:false,
//         emptyShortName:false

//     });
//     changeUnitName=(e)=>{
//         this.setState({
//             unitName:e.target.value
//         });
//     }
//     changeShortName=(e)=>{
//         this.setState({
//             shortName:e.target.value
//         });
//     }
//     setValues=()=>{
      
//         this.setState((prevState)=>{
//             return({...prevState,
//             emptyUnitName:this.state.unitName===''?true:false,
//             emptyShortName:this.state.shortName===''?true:false})
//         });
        
//     }
//     saveUnit=()=>{
//         this.setValues();
//         console.log(this.state)
//         console.log(this.state)
//         let data={
//             unitName:this.state.unitName,
//             shortName:this.state.shortName
//         };
//         console.log(this.props)
//         if(!this.state.emptyUnitName&&!this.state.emptyShortName){
//             this.props.add_unitOfMeasuring(data);
//             alert("data have been saved");       
//         }
       
       
//     }
//    componentDidUpdate(){
//     console.log(this.state)
//     //      let data={
//     //         unitName:this.state.unitName,
//     //         shortName:this.state.shortName
//     //     };
//     //     if(!this.state.emptyUnitName&&!this.state.emptyShortName){
//     //         //this.props.add_unitOfMeasuring(data);
//     //         alert("data have been saved")         
//     //     }
//     //    this.props.add_unitOfMeasuring(data);
//    }
//     render(){
//         return(
//             <div>
//                 {this.state.emptyUnitName?<p>unit can not be empty</p>:null}
//                 unit name<input value={this.state.unitName} type="text" onChange={this.changeUnitName}/>
//                 {this.state.emptyShortName?<p>short name can not be empty</p>:null}
//                 short name<input value={this.state.shortName} type="text" onChange={this.changeShortName}/>
//                 <button onClick={this.saveUnit}>Save Unit</button>
//             </div>
//         )
//     }
// }

// class UnitsOfMeasuring extends Component {

//     //componentWillMount = () => this.props.get_unitsOfMeasuring();
//     state=({
//         addUnit:false
//     })
//     addUnit=()=>{
//         this.setState({addUnit:true})
//         // let data={
//         //     unitName:"example",
//         //     shortName:"ex"
//         // };
//         // this.props.add_unitOfMeasuring(data);

//     }

//     render() {
//         console.log('unit of measuring', this.props)
//         const { unitsOfMeasuring } = this.props;
//         console.log('unit', unitsOfMeasuring)
//         //return (
//             return <div>
//                 <button onClick={this.addUnit}>Add unit</button>
//                 {this.state.addUnit?<AddUnit add_unitOfMeasuring={this.props.add_unitOfMeasuring}/>:null}
//                 {/* </div><table className="table w-75 m-auto"> */}
//                     {/* <tbody>
//                         <UnitOfMeasuringAddWrapper 
//                             item={{id: "00000000-0000-0000-0000-000000000000",unitName:"",shortName:""}} 
//                         /> */}
//                         {!unitsOfMeasuring.isPending ? <UnitOfMeasuringListWrapper   data={unitsOfMeasuring.units} /> : null }
//                     {/* </tbody>
//                 </table> 
//                 {unitsOfMeasuring.isPending ? <Spinner/> : null} */}
//             </div>
//             // <>
//             // <div>
//             //     <table className="table w-75 m-auto">
//             //         <tbody>
//             //             <UnitOfMeasuringAddWrapper 
//             //                 item={{id: "00000000-0000-0000-0000-000000000000",unitName:"",shortName:""}} 
//             //             />                        
//             //             {/* {!unitsOfMeasuring.isPending ? <UnitOfMeasuringListWrapper data={unitsOfMeasuring.units} /> : <Spinner/> }   */}
//             //             {/* {<UnitOfMeasuringListWrapper data={unitsOfMeasuring.units}/>} */}
//             //         </tbody>
//             //     </table> 
//             //     {/* {unitsOfMeasuring.isPending ? <Spinner/> : null} */}
//             // </div>
                
//             // </>
//         //);

//     }
// }

// const mapStateToProps = (state) => ({
//     unitsOfMeasuring: state.unitsOfMeasuring
// });

// const mapDispatchToProps = (dispatch) => {
//     return {
//         add_unitOfMeasuring: (data) => dispatch(add_unitOfMeasuring(data))
//     }
// };

// export default connect(
//     mapStateToProps,
//     mapDispatchToProps
// )(UnitsOfMeasuring);
// //








