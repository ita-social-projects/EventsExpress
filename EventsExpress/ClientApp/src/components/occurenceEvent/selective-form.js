import React, { Component } from 'react';
import Button from "@material-ui/core/Button";
import './selective-form.css'
import { Link } from 'react-router-dom';


export class SelectiveForm extends Component {

    componentDidMount = () => {
        let values = this.props.form_values || this.props.initialValues;
    }

    componentWillUnmount() {
        this.resetForm();
    }

    isCreateButtonDisabled = false;
    disableCreateButton = () => {
        if (this.props.valid) {
            this.isCreateButtonDisabled = true;
        }
    }

    isEditButtonDisabled = false;
    disableEditButton = () => {
        if (this.props.valid) {
            this.isEditButtonDisabled = true;
        }
    }

    isCancelOnceButtonDisabled = false;
    disableCancelOnceButton = () => {
        if (this.props.valid) {
            this.isCancelOnceButtonDisabled = true;
        }
    }

    isCancelButtonDisabled = false;
    disableCancelButton = () => {
        if (this.props.valid) {
            this.isCancelButtonDisabled = true;
        }
    }

    resetForm = () => {
        this.isSaveButtonDisabled = false;
        this.setState({ imagefile: [] });
    }

    render() {
        return (
            <div className="custom-form shadow-lg p-3 mb-5 bg-white rounded">
                <form onSubmit={this.props.handleSubmit} encType="multipart/form-data">
                    <p className="shadow-sm p-2 mb-3 bg-white rounded">
                        Click Create to create the reccurent event. To edit the event, click Edit.
                        Click Cancel Once to cancel the reccurent event. To cancel all reccurent events, click Cancel.
                    </p>
                    <Link to={'/home/events'}>
                        <Button fullWidth={true} type="submit" color="primary" onClick={this.disableCreateButton} disabled={this.isCreateButtonDisabled}>
                            Create
                        </Button>
                    </Link>
                    <Link to={'/home/events'}>
                        <Button fullWidth={true} type="submit" color="primary" onClick={this.disableEditButton} disabled={this.isEditButtonDisabled}>
                            Edit
                        </Button>
                    </Link>
                    <Button fullWidth={true} type="submit" color="primary" onClick={this.disableCancelOnceButton} disabled={this.isCancelOnceButtonDisabled}>
                        Cancel Once
                    </Button>
                    <Button fullWidth={true} type="submit" color="primary" onClick={this.disableCancelButton} disabled={this.isCancelButtonDisabled}>
                        Cancel
                    </Button>
                </form>
            </div>
        );
    }
}

export default SelectiveForm