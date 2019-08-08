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

    render() {
        const { userPhoto, name, email, birthday, gender, categories, id, attitude } = this.props.data;
        const categories_list = this.renderCategories(categories);
        console.log(this.props)

        
        
        
        return <>
            <div className="row box info">
                {!(id === this.props.current_user) && 
                    <div className="col-3">
                    <div className="d-flex align-items-center attitude">
                        <Avatar
                            alt="Тут аватар"
                            src={userPhoto}

                            className='bigAvatar'
                        />
                    </div>
                        {attitude == '2' && <div className="row attitude">
                        <button onClick={this.props.onLike} className="btn btn-info">Like</button>
                        <button onClick={this.props.onDislike} className="btn btn-info">Dislike</button>
                    </div>}
                        {attitude == '1' && <div className="row attitude">
                        <button onClick={this.props.onLike} className="btn btn-info">Like</button>
                        <button className="btn btn-light">Dislike</button>
                        <button onClick={this.props.onReset} className="btn btn-info">Reset</button>
                    </div>}
                        {attitude == '0' && <div className="row attitude">
                        <button className="btn btn-light">Like</button>
                        <button onClick={this.props.onDislike} className="btn btn-info">Dislike</button>
                        <button onClick={this.props.onReset} className="btn btn-info">Reset</button>
                    </div>}
                    </div>
                }
                
                {(id === this.props.current_user) &&
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
            <div className="row events">
                <div class="btn-group" data-toggle="buttons">
  <label class="btn btn-light-blue form-check-label active">
                        <input class="form-check-input" type="radio" name="options" id="option1" autocomplete="off" onClick={this.props.onFuture} checked />
    Preselected
  </label>
  <label class="btn btn-light-blue form-check-label">
                        <input class="form-check-input" type="radio" name="options" id="option2" autocomplete="off" onClick={this.props.onVisited}/> Radio
  </label>
  <label class="btn btn-light-blue form-check-label">
    <input class="form-check-input" type="radio" name="options" id="option3" autocomplete="off"/> Radio
  </label>
</div>
                <button onClick={this.props.onFuture} className="btn btn-outline-info active">Future</button>
                <button onClick={this.props.onPast} className="btn blue-gradient">Past</button>
                <button onClick={this.props.onVisited} className="btn btn-outline-info">Visited</button>
                <button onClick={this.props.onToGo} className="btn btn-outline-info">To Go</button>
            </div>
            <div className="row box ">
                <div className="shadow p-3 mb-5 bg-white rounded">
                    {(this.props.events) ? this.renderEvents(this.props.events) : null}
                </div>
            </div>
        </>
    }
}