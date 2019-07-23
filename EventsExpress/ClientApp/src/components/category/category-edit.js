import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import { renderTextField } from '../helpers/helpers';
import Fab from '@material-ui/core/Fab';
import './Category.css';



class categoryItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            
            <form className="w-100" name="saveCategory" onSubmit={this.props.handleSubmit}>
                <div className="d-flex justify-content-around ">
                    <Field
                        name="category"
                        component={renderTextField}
                        label={this.props.item.name}
                        type="category"
                    />
                    <Fab 
                        type="submit"
                        size="small"
                        color="primary"
                        onClick={this.props.save}
                        aria-label="Edit">
                        <i className="fa fa-check-circle"></i>
                    </Fab>
                </div>
            </form>
               
            );
        }

};

categoryItem = reduxForm({
    // a unique name for the form
    form: "save-form"
})(categoryItem);

export default categoryItem;