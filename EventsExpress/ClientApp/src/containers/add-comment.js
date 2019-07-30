import React from "react";
import CommentForm from '../components/comment/comment-form';
import { connect } from "react-redux";
import add from "../actions/add-comment";
import { reset } from 'redux-form';
import { setCommentError, setCommentPending, setCommentSuccess } from '../actions/add-comment';

class CommentWrapper extends React.Component {
    componentDidUpdate = () => {

    }
    submit = values => {
        console.log(values);
        this.props.add({ ...values, userId: this.props.userId, eventId: this.props.eventId });
    };
    render() {
        return (
            <div>
                <CommentForm commentError={this.props.commentError} onSubmit={this.submit} />
            </div>
        );
    }
}
const mapStateToProps = state => ({
    commentError: state.add_comment.commentError,
    userId: state.user.id,
    eventId: 'd3994f53-1e0d-4eda-d0e8-08d70c4f9464'
});

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add(data)),
        reset: () => {
            dispatch(reset('add-comment'));
            dispatch(setCommentPending(true));
            dispatch(setCommentSuccess(false));
            dispatch(setCommentError(null));
        }
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CommentWrapper);