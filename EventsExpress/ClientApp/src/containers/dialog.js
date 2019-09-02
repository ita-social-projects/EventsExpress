import React from 'react';
import { connect } from "react-redux";
import AlertDialog from '../components/helpers/Dialog';
import { setDialogOpen } from '../actions/dialog';

class DialogContainer extends React.Component{
    
    
    render() {
        console.log('props');
        console.log(this.props)
        return <AlertDialog
                    dialog={this.props.dialog}
                    setOpen={this.props.setOpen}
                    
                    callback={this.props.callback}
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
    setOpen:(data)=>dispatch(setDialogOpen(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(DialogContainer)