import React from "react";
import { Field, reduxForm } from "redux-form";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import { renderTextField } from '../helpers/helpers';

class Comment extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="comment">
                <form name="addComment" onSubmit={this.props.handleSubmit}>
                    <div>
                        <Field
                            name="comment"
                            component={renderTextField}
                            label="Comment:"
                            type="comment"
                        />
                    </div>
                    <div>
                        {this.props.commentError && <div className="alert alert-danger">{this.props.commentError}</div>}

                        <DialogActions>
                            <Button type="submit" value="Add" color="primary">
                                Add
                            </Button>
                        </DialogActions>

                    </div>
                </form>
            </div>
        );
    }
}

Comment = reduxForm({
    // a unique name for the form
    form: "add-comment"
})(Comment);

export default Comment;