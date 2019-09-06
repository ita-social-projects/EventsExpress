import React from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import { renderTextField } from '../helpers/helpers';
import './Comment.css';

let Comment = (props) => (
    <form name="addComment" onSubmit={props.handleSubmit}>
        <Field
            name="comment"
            component={renderTextField}
            label="Comment:"
        />
    
        {props.commentError && 
            <div className="alert alert-danger">{props.commentError}</div>
        }

        <DialogActions>
            <Button type="submit" value="Add" color="primary"> Add </Button>
        </DialogActions>                    
    </form>      
);


Comment = reduxForm({
    form: "add-comment"
})(Comment);

export default Comment;