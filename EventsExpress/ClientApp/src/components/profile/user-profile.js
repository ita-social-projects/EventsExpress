import React, { Component } from 'react';
import Moment from 'react-moment';
import 'moment-timezone';
import { Link } from 'react-router-dom';
import Avatar from '@material-ui/core/Avatar';
import genders from '../../constants/GenderConstants';
import Event from '../event/event-item';
import { AddComponent } from '../home/home';
import './User-profile.css';


export default class UsertemView extends Component {

    getAge = birthday => {
        let today = new Date();
        let birthDate = new Date(birthday);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        return age;
    }

    

    renderCategories = arr => arr.map(item => <span key={item.id}>#{item.name}</span>)
    renderEvents = arr => arr.map(item => <Event key={item.id} item={item} />)
        
    render() {
        const { userPhoto, name, email, birthday, gender, categories, id, attitude } = this.props.data;
        const categories_list = this.renderCategories(categories);
                
        return <>
            {(id === this.props.current_user) && <AddComponent />}
            <div className="row box info">
                
                <div className="col-3">
                    <h6><strong><p className="font-weight-bolder" >User Name:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Age:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Gender:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Email:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Interests:</p></strong></h6>
                </div>
                <div className="col-3">
                    <h6><strong><p className="font-weight-bolder" >{name}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{this.getAge(birthday)}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{genders[gender]}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{email}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{categories_list}</p></strong></h6>
                </div>
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
            </div>
            <div className="row">
                <div className="col-2 check">
                    <div class="funkyradio">
                        <div class="funkyradio-primary">
                            <input type="radio" name="radio" id="radio2" onChange={this.props.onFuture}/>
                            <label for="radio2">Future events</label>
                        </div>
                        <div class="funkyradio-primary">
                            <input type="radio" name="radio" id="radio3" onChange={this.props.onPast}/>
                            <label for="radio3">Archive of events</label>
                        </div>
                        <div class="funkyradio-primary">
                            <input type="radio" name="radio" id="radio4" onChange={this.props.onVisited}/>
                            <label for="radio4">Visited events</label>
                        </div>
                        <div class="funkyradio-primary">
                            <input type="radio" name="radio" id="radio5" onChange={this.props.onToGo}/>
                            <label for="radio5">Events to go</label>
                        </div>
                    </div>
                </div>
                <div className="col-9">
                    <div className="shadow p-5 mb-5 bg-white rounded">
                        {(this.props.events && this.props.events.length > 0) ? this.renderEvents(this.props.events) : <h4><strong><p className="font-weight-bold p-9" align="center">No events yet!</p></strong></h4>}
                    </div>
                </div>
            </div>
        </>
    }
}

