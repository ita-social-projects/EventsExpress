import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import { renderTextField } from '../helpers/form-helpers';
import IconButton from "@material-ui/core/IconButton";
import ErrorMessages from '../shared/errorMessage';

const divStyle = {
    width: "90wh"
};

class UnitOfMeasuringEdit extends Component {
    state = ({
        unitError: null,
        shortError: null,
        showAlert: false
    })

    showAlert = () => {
        this.setState({
            showAlert: true
        });
    };

    hideAlert = () => {
        this.setState({
            showAlert: false
        });
    };

    render() {
        return <>
            <td colSpan="3" className="align-middle">
                <form className="w-100" id="save-form" onSubmit={this.props.handleSubmit}>
                    <div style={divStyle} className="d-flex flex justify-content-around ">

                        <Field
                            className="form-control"
                            name="unitName"
                            label="Unit name"
                            component={renderTextField}
                        />
                        <Field
                            className="form-control"
                            name="shortName"
                            label="Short name"
                            component={renderTextField}
                        />
                        {
                            this.props.error &&
                            <ErrorMessages error={this.props.error} className="text-center" />
                        }
                    </div>
                </form>
            </td>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-success" size="small" type="submit" form="save-form">
                        <i className="fa fa-check"></i>
                    </IconButton>
                </div>
            </td>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
                </div>
            </td>
        </>
    }
}

export default reduxForm({
    form: "save-form",
    enableReinitialize: true
})(UnitOfMeasuringEdit);