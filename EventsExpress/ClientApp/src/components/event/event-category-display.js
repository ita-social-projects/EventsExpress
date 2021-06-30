import React, { Component } from "react";
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Dialog from '@material-ui/core/Dialog';

const emails = ['username@gmail.com', 'user02@gmail.com'];

function SimpleDialog(props) {
    const { onClose,  open } = props;



    const handleListItemClick = (value) => {
        onClose(value);
    };

    return (
        <Dialog  aria-labelledby="simple-dialog-title" open={open}>
            <DialogTitle id="simple-dialog-title">Set backup account</DialogTitle>
            <List>
                {emails.map((email) => (
                    <ListItem button onClick={() => handleListItemClick(email)} key={email}>
                        <ListItemText primary={email} />
                    </ListItem>
                ))}

                <ListItem autoFocus button onClick={() => handleListItemClick('addAccount')}>
                    <ListItemText primary="Add account" />
                </ListItem>
            </List>
        </Dialog>
    );
}

SimpleDialog.propTypes = {
    onClose: PropTypes.func.isRequired,
    open: PropTypes.bool.isRequired,
};

export default class AllCategoryModal extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isOpen: false,
        }
    }
    onclick = () => {
        this.setState({ isOpen: true });
    }
    onClose = () => {
        this.setState({ isOpen: false });
    }
    render() {
        return (
            <div>
                <Button variant="outlined" color="primary" onClick={this.onclick}>
                    ...
      </Button>
                <SimpleDialog  open={this.state.isOpen} onClose={this.onClose} />
            </div>
        );
    }
}