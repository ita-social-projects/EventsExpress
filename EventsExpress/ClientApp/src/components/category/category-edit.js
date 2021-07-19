import React, {Component} from "react";
import {Field, reduxForm} from "redux-form";
import { renderTextField } from '../helpers/form-helpers';
import ErrorMessages from '../shared/errorMessage';
import IconButton from "@material-ui/core/IconButton";
import { fieldIsRequired } from '../helpers/validators/required-fields-validator';

const validate = values => {
    const requiredFields = [
        'name'
    ];
    return {
        ...fieldIsRequired(values, requiredFields)
    }
}

class CategoryEdit extends Component {

    render() {
        return <>
            <td className="align-middle" width="75%">
                <form className="w-100" id="save-form" onSubmit={this.props.handleSubmit}>
                    <div className="d-flex flex-column justify-content-around ">
                        <Field
                            className="form-control"
                            name="name"
                            label="Name"
                            component={renderTextField}
                        />
                        {
                            this.props.error &&
                            <ErrorMessages error={this.props.error} className="text-center"/>
                        }
                    </div>
                </form>
            </td>
            <td/>
            <td/>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-success" size="small" type="submit" form="save-form">
                        <i className="fa fa-check" />
                    </IconButton>

                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times" />
                    </IconButton>
                </div>
            </td>
        </>
    }
}

CategoryEdit = reduxForm({
    form: "save-form",
    validate,
    enableReinitialize: true
})(CategoryEdit);

export default CategoryEdit;
