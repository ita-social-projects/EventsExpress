import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Comment from '../comment/comment';
import EditEventWrapper from '../../containers/edit-event'; 
import CustomAvatar from '../avatar/custom-avatar';
import RatingWrapper from '../../containers/rating';
import IconButton from "@material-ui/core/IconButton";
import Moment from 'react-moment';
import EventCancelModal from './event-cancel-modal';
import 'moment-timezone';
import '../layout/colorlib.css';
import './event-item-view.css';


export default class EventItemView extends Component {

    state = { edit: false }

    renderCategories = arr => {
        return arr.map(x => <span key={x.id}>#{x.name}</span>);
    }

    renderUsers = arr => {
        return arr.map(x => (
            <Link to={'/user/' + x.id} className="btn-custom">
                <div className="d-flex align-items-center border-bottom">
                    <CustomAvatar size="little" photoUrl={x.photoUrl} name={x.username} />                    
                    <div>
                        <h5>{x.username}</h5>
                        {'Age: ' + this.getAge(x.birthday)}
                    </div>
                </div>
            </Link>)
        );
    }

    renderOwner = user => (
        <Link to={'/user/' + user.id} className="btn-custom">
            <div className="d-flex align-items-center border-bottom">
                <div className='d-flex flex-column'>
                    <IconButton  className="text-warning"  size="small" disabled > 
                        <i class="fas fa-crown"></i>
                    </IconButton>
                    <CustomAvatar size="little" photoUrl={user.photoUrl} name={user.username} />
                </div>
                <div>
                    <h5>{user.username}</h5>
                    {'Age: ' + this.getAge(user.birthday)}
                </div>
            </div>
        </Link>
    )

    getAge = birthday => {
        let today = new Date();
        let birthDate = new Date(birthday);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        if (age >= 100) {
            age = "---";
        }
        return age;
    }

    onEdit = () => {
        this.setState({edit: true});
    }

    render() {
        const { current_user } = this.props;
        const { photoUrl, categories, title, dateFrom, dateTo, description, user, visitors, country, city} = this.props.event.data;

        const categories_list = this.renderCategories(categories);

        let iWillVisitIt = visitors.find(x => x.id == current_user.id) != null;
        let isFutureEvent = new Date(dateFrom) >= new Date().setHours(0,0,0,0);
        let isMyEvent = current_user.id === user.id;

        return <>
        <div className="container-fluid mt-1">
            <div className="row">
                <div className="col-9">
                    <div className="col-12">
                        <img src={photoUrl} className="w-100" />
                        <div className="text-block"> 
                            <span className="title">{title}</span><br/>
                            <span><Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment> {dateTo != dateFrom && <>- <Moment format="D MMM YYYY" withTitle>{dateTo}</Moment></>}</span><br/>
                            <span>{country} {city}</span><br/>
                            {categories_list}
                        </div>
                        <div className="button-block">
                            {(isFutureEvent && current_user.id != null)
                                ? isMyEvent   
                                        ? !this.state.edit ? <><button onClick={this.onEdit} className="btn btn-join">Edit</button>
                                            <EventCancelModal
                                                submitCallback={this.props.onCancel}
                                                cancelationStatus={this.props.event.cancelation} /></> : null
                                    : iWillVisitIt
                                        ? <button onClick={this.props.onLeave} className="btn btn-join">Leave</button>
                                        : <button onClick={this.props.onJoin} className="btn btn-join">Join</button>
                                : null            
                            }
                        </div>
                    </div>
                    {this.state.edit 
                        ? <div className="row shadow mt-5 p-5 mb-5 bg-white rounded">
                            <EditEventWrapper />
                        </div>
                        : <>
                            {!isFutureEvent &&
                                <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                <RatingWrapper 
                                    iWillVisitIt={iWillVisitIt}
                                    eventId={this.props.data.id}
                                    userId={current_user.id}
                                />                                
                            </div>
                            }
                            
                            
                            <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                {description}
                            </div>
                            <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                <Comment match={this.props.match}/>
                            </div>
                        </>
                    }
                </div>
                <div className="col-3 overflow-auto shadow p-3 mb-5 bg-white rounded">
                    {this.renderOwner(user)}
                    {this.renderUsers(visitors)}
                </div>
            </div>
        </div>
        </>
    }
}