import React from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import { renderTextField } from '../helpers/helpers';
import './Category.css';

class Category extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="category">
                <form name="addCategory" onSubmit={this.props.handleSubmit}>
                    <div>
                        <Field
                            name="category"
                            component={renderTextField}
                            label="Category:"
                            type="category"
                        />
                    </div>
                    <div>
                        {this.props.categoryError && <div className="alert alert-danger">{this.props.categoryError}</div>}

                        <DialogActions>
                            <Button  type="submit" value="Add" color="primary">
                                Add
                            </Button>
                        </DialogActions>

                    </div>
                </form>
            </div>
        );
    }
}

Category = reduxForm({
    // a unique name for the form
    form: "add-form"
})(Category);

export default Category;