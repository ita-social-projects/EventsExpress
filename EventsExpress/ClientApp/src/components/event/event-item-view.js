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

    constructor() {
        super();

        this.state = {
            isOpen: true,
            edit: false
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    handleOnClickCaret() {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

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
                    <IconButton className="text-warning" size="small" disabled >
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
        let age = today.getFullYear() - birthDate.getFullYear();
        let m = today.getMonth() - birthDate.getMonth();

        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }

        if (age >= 100) {
            age = "---";
        }

        return age;
    }

    onEdit = () => {
        this.setState({ edit: true });
    }

    render() {
        const { current_user } = this.props;
        const {
            photoUrl,
            categories,
            title,
            dateFrom,
            dateTo,
            description,
            maxParticipants,
            user,
            visitors,
            country,
            city,
            inventories
        } = this.props.event.data;
        console.log(this.props);
        const categories_list = this.renderCategories(categories);
        const INT32_MAX_VALUE = 2147483647;

        let iWillVisitIt = visitors.find(x => x.id === current_user.id) !== null;
        let isFutureEvent = new Date(dateFrom) >= new Date().setHours(0, 0, 0, 0);
        let isMyEvent = current_user.id === user.id;
        let isFreePlace = visitors.length < maxParticipants;
        let canEdit = isFutureEvent && isMyEvent;
        let canJoin = isFutureEvent && isFreePlace && !iWillVisitIt && !isMyEvent;
        let canLeave = isFutureEvent && !isMyEvent && iWillVisitIt;
        let canCancel = isFutureEvent && current_user.id != null && isMyEvent && !this.state.edit;

        return <>
            <div className="container-fluid mt-1">
                <div className="row">
                    <div className="col-9">
                        <div className="col-12">
                            <img src={photoUrl} className="w-100" />
                            <div className="text-block">
                                <span className="title">{title}</span>
                                <br />
                                {(maxParticipants < INT32_MAX_VALUE)
                                    ? <span className="maxParticipants">{visitors.length}/{maxParticipants} Participants</span>
                                    : <span className="maxParticipants">{visitors.length} Participants</span>
                                }
                                <br />
                                <span>
                                    <Moment format="D MMM YYYY" withTitle>
                                        {dateFrom}
                                    </Moment>
                                    {dateTo != dateFrom &&
                                        <>-
                                            <Moment format="D MMM YYYY" withTitle>
                                                {dateTo}
                                            </Moment>
                                        </>
                                    }
                                </span>
                                <br />
                                <span>{country} {city}</span>
                                <br />
                                {categories_list}
                            </div>
                            <div className="button-block">
                                {canEdit && <button onClick={this.onEdit} className="btn btn-join">Edit</button>}
                                {canJoin && <button onClick={this.props.onJoin} className="btn btn-join">Join</button>}
                                {canLeave && <button onClick={this.props.onLeave} className="btn btn-join">Leave</button>}
                                {canCancel && <EventCancelModal submitCallback={this.props.onCancel} cancelationStatus={this.props.event.cancelation} />}
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
                                    <div className="d-flex justify-content-start align-items-center">
                                        <h2>Inventory</h2>
                                        {this.state.isOpen
                                                ? <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={this.handleOnClickCaret}>
                                                    <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                                        <path fillRule="evenodd" d="M3.204 5L8 10.481 12.796 5H3.204zm-.753.659l4.796 5.48a1 1 0 0 0 1.506 0l4.796-5.48c.566-.647.106-1.659-.753-1.659H3.204a1 1 0 0 0-.753 1.659z"/>
                                                    </svg>
                                                </button>
                                            :  <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={this.handleOnClickCaret}>
                                                <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">                            
                                                    <path d="M7.247 11.14L2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z"/>
                                                </svg>
                                            </button>
                                        }
                                    </div>
                                    { this.state.isOpen &&
                                    <div className="table-responsive">
                                        <div className="table-wrapper">
                                            <div className="table-title">
                                            </div>
                                            <table className="table">
                                                <thead>
                                                    <tr>
                                                        <th>Item name</th>
                                                        <th>Count</th>
                                                        <th>Measuring unit</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                {inventories.map(item => {
                                                    return (
                                                        <tr>
                                                            <td>{item.itemName}</td>
                                                            <td>{item.needQuantity}</td>
                                                            <td>{item.unitOfMeasuring.shortName}</td>
                                                        </tr>
                                                    )
                                                })}
                                                    
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    }
                                </div>
                                
                                <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                    <Comment match={this.props.match} />
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
