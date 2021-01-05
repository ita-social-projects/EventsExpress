import React from 'react';
import AddComment from '../../containers/add-comment';
import CommentListWrapper from '../../containers/comment-list';
import './Comment.css';


export default Comment = (props) => (
    <div>
        <AddComment />
        <CommentListWrapper match={props.match} />
    </div>
);
