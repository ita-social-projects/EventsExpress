import React from "react";
import CommentForm from '../components/comment/comment-form';
import { connect } from "react-redux";
import add from "../actions/add-comment";
import { reset } from 'redux-form';
import { set2CommentError, setCommentPending, setCommentSuccess } from '../actions/add-comment';


class CommentWrapper extends React.Component {
    componentDidUpdate = () => {

    }
    componentDidUpdate = () => {
        if (!this.props.add_event_status.errorComment && this.props.add_event_status.isCommentSuccess) {
            this.props.reset();
            this.props.resetEventStatus();
        }
    }
    componentWillUnmount = () => {
        this.props.reset();
        this.props.resetCommentStatus();
    }
    submit = values => {
        this.props.add({ ...values, userId: this.props.userId, eventId: this.props.eventId, commentsId: this.props.parentId });
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
    eventId: state.event.data.id
});

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add(data)),
        reset: () => {
            dispatch(reset('add-comment'));
        },
        resetCommentStatus: () => {
            dispatch(setCommentPending(true));
            dispatch(setCommentSuccess(false));
            dispatch(set2CommentError(null));
        }
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CommentWrapper);