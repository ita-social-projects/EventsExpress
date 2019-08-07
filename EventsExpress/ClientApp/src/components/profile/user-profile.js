import React, { Component } from 'react';
import Moment from 'react-moment';
import 'moment-timezone';
import { Link } from 'react-router-dom';
import Avatar from '@material-ui/core/Avatar';
import genders from '../../constants/GenderConstants';
import Event from '../event/event-item';
import './User-profile.css';


export default class UsertemView extends Component {

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

    renderCategories = (arr) => {
        return arr.map((x) => (<span key={x.id}>#{x.name}</span>)
        );
    } 

    renderEvents = (arr) => {
        return arr.map((x) => (<Event key={x.id} item={x} />)
        );
    }

    check = (value) => {
        if (value === this.props.current_user) return false;
        return true;
    }

    render() {
        const { userPhoto, name, email, birthday, gender, categories, events, id } = this.props.data;
        const categories_list = this.renderCategories(categories);
        const event_list = this.renderEvents(events);
        const current_user = this.props.current_user;
        return <>
            <div className="row box info">
                {this.check(id) && 
                    <div className="col-3">
                    <div className="d-flex align-items-center">
                        <Avatar
                            alt="Тут аватар"
                            src={userPhoto}

                            className='bigAvatar'
                        />
                    </div>
                    <button onClick={this.props.onLike} className="btn btn-info">Like</button>
                    <button onClick={this.props.onDislike} className="btn btn-info">Dislike</button>
                    </div>
                }
                {!this.check(id) &&
                    <div className="col-2">
                    </div>
                }
                <div className="col-3">
                    <h5><strong><p className="font-weight-bolder" >User Name:</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >Age:</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >Gender:</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >Email:</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >Interests:</p></strong></h5>
                </div>
                <div className="col-3">
                    <h5><strong><p className="font-weight-bolder" >{name}</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >{this.getAge(birthday)}</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >{genders[gender]}</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >{email}</p></strong></h5>
                    <h5><strong><p className="font-weight-bolder" >{categories_list}</p></strong></h5>
                </div>

            </div>
            <div className="row box ">
                <div className="shadow p-3 mb-5 bg-white rounded">
                    {event_list}
                </div>
            </div>
        </>
    }
}