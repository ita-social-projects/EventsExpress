import React, { Component } from "react";
import { reduxForm } from "redux-form";
import Avatar from '@material-ui/core/Avatar';
import { Link } from 'react-router-dom';
import './Comment.css';




export default class commentItem extends Component {
    constructor(props) {
        super(props);
    }

    getTime = (time) => {
        let today = new Date();
        let times = new Date(time);
        var age = today.getFullYear() - times.getFullYear();
        if (age != 0) return `${age} years ago`;
        if ((today.getMonth() - times.getMonth()) != 0) return `${today.getMonth() - times.getMonth()} months ago`;
        if ((today.getDate() - times.getDate()) != 0) return `${today.getDate() - times.getDate()} days ago`;
        if ((today.getHours() - times.getHours()) != 0) return `${today.getHours() - times.getHours()} hours ago`;
        if ((today.getMinutes() - times.getMinutes()) != 0) return `${today.getMinutes() - times.getMinutes()} minutes ago`;
        return `right now`;
    }

    render() {
        const { text, userPhoto, date, userName, userId } = this.props.item;
        return (
            <div>
                <div class="comment-container">
                    <div class="row">
                        <div class="photo-container">
                            <Avatar
                                alt="Тут аватар"
                                src={userPhoto}
                            />
                            <h1 class="text-secondary comment-text"> {this.getTime(date)}</h1>
                        </div>
                        <div class="">
                            <p>
                                <Link to={'/user/' + userId} className="btn-custom"><a class="float-left"><strong class="text-primary">{userName}</strong></a></Link>
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


