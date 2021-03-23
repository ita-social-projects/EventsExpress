import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';
import Button from "@material-ui/core/Button";
import InventoryList from '../inventory/InventoryList';

class Part4 extends Component {
    render() {
        return (
            <form onSubmit={this.props.handleSubmit}
                encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">
                    <InventoryList
                        eventId={this.props.initialData.id} />
                </div>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit"
                    >
                        Save
                        </Button>
                </div>
            </form >
        );

    }
}

const mapStateToProps = (state) => ({
    initialData: state.event.data
});

Part4 = connect(
    mapStateToProps
)(Part4);

export default reduxForm({
    form: 'WizardForm',
    enableReinitialize: true
})(Part4);

