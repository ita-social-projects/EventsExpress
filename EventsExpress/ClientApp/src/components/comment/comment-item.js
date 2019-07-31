import React, { Component } from "react";
import { reduxForm } from "redux-form";
import Avatar from '@material-ui/core/Avatar';
import './Comment.css';




export default class commentItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { text, userPhoto, date, userName } = this.props.item;
        return (
            <div>
                <div class="comment-container">
                    <div class="row">
                        <div class="photo-container">
                            <Avatar
                                alt="Тут аватар"
                                src={userPhoto}
                            />
                            <h1 class="text-secondary comment-text">{date}</h1>
                        </div>
                        <div class="">
                            <p>
                                <a class="float-left"><strong class="text-primary">{userName}</strong></a>
                            </p>
                            <div class="clearfix"></div>
                            
                            <p>{text}</p>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}


