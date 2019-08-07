import React, { Component } from 'react';
import AddComment from '../../containers/add-comment';
import CommentListWrapper from '../../containers/comment-list';
import './Comment.css';


export default class Comment extends Component {

    render() {

        return (
            <div>
                <div className="text-box overflow-auto "><CommentListWrapper /></div>
                
                            
                <AddComment />
            </div>

        );
    }
}