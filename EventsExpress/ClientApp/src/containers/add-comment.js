import React from "react";
import CommentForm from '../components/comment/comment-form';
import { connect } from "react-redux";
import addComment,
    { setCommentPending, setCommentSuccess } from "../actions/comment-add-action";
import { reset } from 'redux-form';

class CommentWrapper extends React.Component {

    submit = values => {
        return this.props.add({ ...values, userId: this.props.userId, eventId: this.props.eventId, commentsId: this.props.parentId });
    };
    
    componentWillUnmount = () => {
        this.props.reset();
        this.props.resetCommentStatus();
    }

    render() {

        return this.props.userId
            ? <CommentForm onSubmit={this.submit} />
            : null
    }
}

const mapStateToProps = state => ({
    addCommentStatus: state.add_comment,
    userId: state.user.id,
    eventId: state.event.data.id
});

const mapDispatchToProps = dispatch => ({
    add: (data) => dispatch(addComment(data)),
    reset: () => {
        dispatch(reset('add-comment'));
    },
    resetCommentStatus: () => {
        dispatch(setCommentPending(true));
        dispatch(setCommentSuccess(false));
    }
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CommentWrapper);
