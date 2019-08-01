import React, { Component } from 'react';
import AddComment from '../../containers/add-comment';
import CommentListWrapper from '../../containers/comment-list';
import './Comment.css';


export default class Comment extends Component {

    render() {

        return (
            <div>
                <AddComment />
                <CommentListWrapper match={this.props.match} /> 

            </div>

        );
    }
}