import React, { Component } from 'react';
import Moment from 'react-moment';
import 'moment-timezone';
import ReactRoterDOM from 'react-router-dom';
import Event from './event-item';
import '../layout/colorlib.css';

import { Link } from 'react-router-dom';
import Avatar from '@material-ui/core/Avatar';
import Comment from '../comment/comment';
export default class EventItemView extends Component {

    renderCategories = (arr) => {
        return arr.map((x) => (<span key={x.id}>#{x.name}</span>)
        );
    }

    renderUsers = (arr) => {
        return arr.map(
            (x) => (<div className="d-flex align-items-center">
                <Avatar
                    alt="Тут аватар"
                    src={x.photoUrl}

                    className='littleAvatar'
                /><h4>{x.username} {this.getAge(x.birthday)}</h4>
            </div>)
        );
    }

    getAge = (birthday) => {
        let today = new Date();
        let birthDate = new Date(birthday);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        return age;
    }

    render() {
        const { photoUrl, categories, title, dateFrom, dateTo, description, user, visitors } = this.props.data;

        const { current_user } = this.props;

        const { country, city } = this.props.data.location;

        const categories_list = this.renderCategories(categories);



        let i_join = visitors.find(
            x => x.id == current_user.id
        );

        let flag = i_join == null;
        return <>
            <div className="row box">
                <div className="col-6">
                    <img src={photoUrl} className="img-thumbnail" />
                    <p>{categories_list}</p>
                    {current_user.id != user.id && current_user.id != null &&
                        <>
                            {flag == true &&
                                <button onClick={this.props.onJoin} className="btn btn-info">Join</button>
                            }
                            {!flag &&
                                <button onClick={this.props.onLeave} className="btn btn-info">Leave</button>
                            }
                        </>
                    }

                    <h4>Created by:</h4>
                    <hr />
                    <div className="d-flex align-items-center">
                        <Avatar
                            alt="Тут аватар"
                            src={user.photoUrl}

                            className='littleAvatar'
                        /><h4>{user.username} {this.getAge(user.birthday)}</h4>
                    </div>

                    <h4>Visitors:</h4>
                    <hr />
                    {this.renderUsers(visitors)}
                </div>
                <div className="col-6">
                    <h3><strong><p className="text-center font-weight-bolder" >{title}</p></strong></h3>

                    <h3><p className="text-center"><Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment></p></h3>

                    <h3><p className="text-center">{country} {city}</p></h3>
                    <div className="text-box overflow-auto scrollbar-near-moon shadow p-3 mb-5 bg-white rounded">
                        <p>{description}</p>
                    </div>

                    <h2><p className="text-center">Comments</p></h2>

                    <div className="text-box overflow-auto shadow p-3 mb-5 bg-white rounded">
                        <Comment />
                    </div>
                </div>
            </div>
        </>
    }
}