import React from "react";
import CommentForm from '../components/comment/comment-form';
import { connect } from "react-redux";
import add from "../actions/add-comment";
import { reset } from 'redux-form';
import { set2CommentError, setCommentPending, setCommentSuccess } from '../actions/add-comment';


class CommentWrapper extends React.Component {

    componentDidUpdate = () => {
        const { isCommentPending, isCommentSuccess, commentError } = this.props.addCommentStatus
        if (this.props.addCommentStatus && !commentError && isCommentSuccess && !isCommentPending) {
            this.props.reset();
            this.props.resetCommentStatus();
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
        return this.props.userId 
            ? <CommentForm commentError={this.props.addCommentStatus.commentError} onSubmit={this.submit} /> 
            : null
    }
}

const mapStateToProps = state => ({
    addCommentStatus: state.add_comment,
    userId: state.user.id,
    eventId: state.event.data.id
});

const mapDispatchToProps = dispatch => ({
    add: (data) => dispatch(add(data)),
    reset: () => {
        dispatch(reset('add-comment'));
    },
    resetCommentStatus: () => {
        dispatch(setCommentPending(true));
        dispatch(setCommentSuccess(false));
        dispatch(set2CommentError(null));
    }
});


export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CommentWrapper);