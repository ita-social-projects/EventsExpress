import React from 'react';
import { connect } from "react-redux";
import AlertDialog from '../components/helpers/Dialog';
import{setDialogOpen}from '../actions/dialog';

class DialogContainer extends React.Component{

    render() {
        return <AlertDialog
                    dialog={this.props.dialog}
                    open={this.props.open}
                    //callback={this.props.callback}
                />;
    }
}

const mapStateToProps=state=>{
    return{
        dialog:state.dialog
    }
};

const mapDispatchToProps=dispatch=>{
    return{
    open:()=>dispatch(setDialogOpen(true))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(DialogContainer)