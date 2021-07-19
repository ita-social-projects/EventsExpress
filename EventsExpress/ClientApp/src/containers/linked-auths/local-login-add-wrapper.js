import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dialog from "@material-ui/core/Dialog";
import Paper from "@material-ui/core/Paper";
import Button from "@material-ui/core/Button";
import Register from "../../components/register";
import DialogTitle from '@material-ui/core/DialogTitle';
import { localLoginAdd } from '../../actions/redactProfile/linked-auths-add-action';
import '../css/Auth.css';

class LocalLoginAdd extends Component {

    constructor(props){
        super(props);
        this.state = {isOpen:false};
    }

    handleClick = () => {
        this.setState({isOpen:true})
    }

    handleClose = () => {
        this.setState({isOpen:false})
    }

    submit = async values => {
        await this.props.localLoginAdd(values.email, values.password);
        this.handleClose();
    }

    render() {
        return (
            <div>
                <button className="btnGoogle" onClick={this.handleClick} disabled={false}>
                    <i className="fas fa-at fa-lg blue" />
                    <span>Log in</span>
                </button>
                <Dialog
                    open={this.state.isOpen}
                    onClose={this.handleClose}
                    fullWidth={true}
                    maxWidth="xs"
                >
                    <DialogTitle>Subscribe</DialogTitle>
                    <Paper square className="p-3" >
                    <Register onSubmit={this.submit} />
                        <Button fullWidth onClick={this.handleClose} color="primary">
                            Cancel
                        </Button>
                    </Paper>
                </Dialog>
            </div>
        );
    }
}

const mapDispatchToProps = dispatch => ({
    localLoginAdd: (email, password) => dispatch(localLoginAdd(email, password))
});

export default connect(null, mapDispatchToProps)(LocalLoginAdd);
