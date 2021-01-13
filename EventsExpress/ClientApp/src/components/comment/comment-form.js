import React from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import { renderTextField } from '../helpers/helpers';
import './Comment.css';

let Comment = (props) => {
    return <> 
            <form name="addComment" onSubmit={props.handleSubmit}>
                <Field
                    name="text"
                    component={renderTextField}
                    label="Comment:"
                />
                <DialogActions>
                    <Button type="submit" value="Add" color="primary"> Add </Button>
                </DialogActions>                    
            </form>
        </>
};


Comment = reduxForm({
    form: "add-comment"
})(Comment);

export default Comment;