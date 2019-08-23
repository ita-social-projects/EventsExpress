import React, { Component } from 'react';
import 'moment-timezone';
import Avatar from '@material-ui/core/Avatar';
import genders from '../../constants/GenderConstants';
import Event from '../event/event-item';
import AddEventWrapper from '../../containers/add-event';
import './User-profile.css';
import EventList from '../event/event-list';
import Spinner from '../spinner';
import { Link } from 'react-router-dom'
export default class UserItemView extends Component {

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
    renderEvents = arr => arr.map(item => <div className="col-4"><Event key={item.id} item={item} /></div>)
        
    render() {
        const { userPhoto, name, email, birthday, gender, categories, id, attitude } = this.props.data;
        const { isPending, data } = this.props.events;

        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventList  data_list={data} /> : null;
       
        const categories_list = this.renderCategories(categories);
                
        return <>
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
                {(id !== this.props.current_user) &&
                    <div className="col-3">
                        <div className="d-flex align-items-center attitude">
                            <Avatar
                                alt="Тут аватар"
                                src={userPhoto}

                                className='bigAvatar'
                            />
                        </div>
                        <center>
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
                        <Link to={`/chat/${id}`}><button className="btn btn-info mt-1">Message</button></Link>
                        </center>
                    </div>
                }

                {(id === this.props.current_user) &&
                    <div className="col-2">
                    </div>
                }
            </div>
                    <div className="funkyradio d-flex">
                        <div className="funkyradio-primary mr-2">
                            <input type="radio" name="radio" id="radio2" onChange={this.props.onFuture}/>
                            <label htmlFor="radio2" className="pr-2">Future events</label>
                        </div>
                        <div className="funkyradio-primary mr-2">
                            <input type="radio" name="radio" id="radio3" onChange={this.props.onPast}/>
                            <label htmlFor="radio3" className="pr-2">Archive of events</label>
                        </div>
                        <div className="funkyradio-primary mr-2">
                            <input type="radio" name="radio" id="radio4" onChange={this.props.onVisited}/>
                            <label htmlFor="radio4" className="pr-2">Visited events</label>
                        </div>
                        <div className="funkyradio-primary mr-2">
                            <input type="radio" name="radio" id="radio5" onChange={this.props.onToGo}/>
                            <label htmlFor="radio5" className="pr-2">Events to go</label>
                        </div>
                        {(id === this.props.current_user) &&
                        <div className="funkyradio-primary mr-2">
                            <input type="radio" name="radio" id="radio6" onChange={this.props.onAddEvent}/>
                            <label htmlFor="radio6" className="pr-2">Add Event</label>
                        </div>
                        }
                    </div>
                    {this.props.add_event_flag ? 
                    <div className="row shadow p-5 mb-5 bg-white rounded">
                        <AddEventWrapper /> 
                     </div>
                    :
                    <div className="shadow p-5 mb-5 bg-white rounded">
                        {(data && data.length > 0) ? <>{spinner}{content}</> : <h4><strong><p className="font-weight-bold p-9" align="center">No events yet!</p></strong></h4>}
                    </div>
                    }
        </>
    }
}

