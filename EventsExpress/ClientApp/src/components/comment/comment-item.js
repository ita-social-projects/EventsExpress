import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';
import { Link } from 'react-router-dom';
import './Comment.css';




export default class commentItem extends Component {

    getTime = (value) => {
        let today = new Date();
        let times = new Date(value);
        var time = today - times;
        var years = Math.floor(time / 52 / 7 / 24 / 60 / 60 / 1000);
        if (years !== 0) return `${years} years ago`;
        time -= years * 52 * 7 * 24 * 60 * 60 * 1000;
        var weeks = Math.floor(time / 7 / 24 / 60 / 60 / 1000);
        if (weeks !== 0) return `${weeks} weeks ago`;
        time -= weeks * 7 * 24 * 60 * 60 *  1000;
        var days = Math.floor(time / 24 / 60 / 60 / 1000);
        if (days !== 0) return `${days} days ago`;
        time -= days * 24 * 60 * 60 * 1000;
        var hours = Math.floor(time / 60 / 60 / 1000);
        if (hours !== 0) return `${hours} hours ago`;
        time -= hours * 60 * 60 * 1000;
        var minutes = Math.floor(time / 60 / 1000);
        if (minutes !== 0) return `${minutes} minutes ago`;
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
                                <Link to={'/user/' + userId} className="btn-custom"><a className="float-left" href={'/user/' + userId}><strong className="text-primary">{userName}</strong></a></Link>
                            </p>
                            <div className="clearfix"></div>
                            
                            <p>{text}</p>
                        </div>
                        {(user === userId) && <div className="photo-container">
                            <Avatar
                                alt="Тут аватар"
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


