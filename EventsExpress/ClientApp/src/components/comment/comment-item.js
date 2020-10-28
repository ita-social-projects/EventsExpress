import React, { Component } from "react";
import { Link } from 'react-router-dom';
import { reduxForm } from "redux-form";
import Avatar from '@material-ui/core/Avatar';
import './Comment.css';
import CustomAvatar from '../avatar/custom-avatar';

export default class commentItem extends Component {
    constructor(props) {
        super(props);
    }

    getTime = (value) => {
        let today = new Date();
        let times = new Date(value);
        let time = today - times;
        let years = Math.floor(time / 52 / 7 / 24 / 60 / 60 / 1000);

        if (years != 0) return `${years} years ago`;

        time -= years * 52 * 7 * 24 * 60 * 60 * 1000;
        let weeks = Math.floor(time / 7 / 24 / 60 / 60 / 1000);

        if (weeks != 0) return `${weeks} weeks ago`;

        time -= weeks * 7 * 24 * 60 * 60 * 1000;
        let days = Math.floor(time / 24 / 60 / 60 / 1000);

        if (days != 0) return `${days} days ago`;

        time -= days * 24 * 60 * 60 * 1000;
        let hours = Math.floor(time / 60 / 60 / 1000);

        if (hours != 0) return `${hours} hours ago`;

        time -= hours * 60 * 60 * 1000;
        let minutes = Math.floor(time / 60 / 1000);

        if (minutes != 0) return `${minutes} minutes ago`;

        return `right now`;
    }

    render() {
        const { text, userPhoto, date, userName, userId } = this.props.item;
        const { user } = this.props;
        return (
            <div>
                <div>
                    <div className="row">
                        {!(user === userId) && <div className="photo-container">
                            <Avatar
                                alt="Тут аватар"
                                src={userPhoto}
                            />
                            <h1 className="text-secondary comment-text"> {this.getTime(date)}</h1>
                        </div>}
                        <div className="mybutton">
                            <p>
                                <Link to={'/user/' + userId} lassName="btn-custom" >
                                    <a className="float-left"><strong className="text-primary">{userName}</strong></a>
                                </Link>
                            </p>
                            <div className="clearfix"></div>
                            <p>{text}</p>
                        </div>
                        {(user === userId) && <div className="photo-container">
                            <Avatar
                                alt="Avatar"
                                src={userPhoto}
                            />
                            <h1 className="text-secondary comment-text"> {this.getTime(date)}</h1>
                        </div>}
                    </div>
                </div>
            </div>
        );
    }
}
