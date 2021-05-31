import React, { Component } from 'react';
import Comment from '../comment/comment';
import { Link } from 'react-router-dom'
import RatingWrapper from '../../containers/rating';
import Moment from 'react-moment';
import 'moment-timezone';
import '../layout/colorlib.css';
import './event-item-view.css';
import EventVisitors from './event-visitors';
import EventLeaveModal from './event-leave-modal';
import InventoryList from '../inventory/InventoryList';
import DisplayLocation from './map/display-location';
import userStatusEnum from '../../constants/userStatusEnum';
import eventStatusEnum from '../../constants/eventStatusEnum';
import SimpleModalWithDetails from '../helpers/simple-modal-with-details';
import { eventDefaultImage } from "../../constants/eventDefaultImage";
import PhotoService from "../../services/PhotoService";

const photoService = new PhotoService();

export default class EventItemView extends Component {
    constructor() {
        super();

        this.state = {
            eventImage: eventDefaultImage
        };
    }

    componentDidMount() {
        photoService.getFullEventPhoto(this.props.event.data.id).then(
            eventFullImage => {
                if (eventFullImage != null) {
                    this.setState({ eventImage: URL.createObjectURL(eventFullImage) });
                }
            }
        );
    }

    componentWillUnmount() {
        URL.revokeObjectURL(this.state.eventImage);
    }

    renderCategories = arr => {
        return arr.map(x => <span key={x.id}>#{x.name}</span>);
    }

    getUserEventStatus = visitor => {
        if (visitor !== undefined) {
            switch (visitor.userStatusEvent) {
                case userStatusEnum.APPROVED:
                    return (
                        <span className="alert alert-success shadow" role="alert">
                            You are gonna visit.
                        </span>
                    );
                case userStatusEnum.DENIED:
                    return (
                        <span className="alert alert-danger shadow" role="alert">
                            Denied participation.
                        </span>
                    );
                case userStatusEnum.PENDING:
                    return (
                        <span className="alert alert-warning shadow" role="alert">
                            Wait until admin approve your request.
                        </span>
                    );
            }
        }
        return (
            <span className="alert alert-secondary shadow" role="alert">
                You are not in event yet.
            </span>
        );
    }

    render() {
        const { current_user } = this.props;
        const {
            id,
            categories,
            title,
            dateFrom,
            dateTo,
            description,
            isPublic,
            eventStatus,
            maxParticipants,
            visitors,
            owners,
        } = this.props.event.data;
        const categories_list = this.renderCategories(categories);
        const INT32_MAX_VALUE = 2147483647;
        const visitorsEnum = {
            approvedUsers: visitors.filter(x => x.userStatusEvent == 0),
            deniedUsers: visitors.filter(x => x.userStatusEvent == 1),
            pendingUsers: visitors.filter(x => x.userStatusEvent == 2)
        };

        let iWillVisitIt = visitors.find(x => x.id === current_user.id);
        let isFutureEvent = new Date(dateFrom) >= new Date().setHours(0, 0, 0, 0);
        let isMyEvent = owners.find(x => x.id === current_user.id) != undefined;
        let isFreePlace = visitorsEnum.approvedUsers.length < maxParticipants;
        let canEdit = isFutureEvent && isMyEvent;
        let canJoin = isFutureEvent && isFreePlace && !iWillVisitIt && !isMyEvent && eventStatus === eventStatusEnum.Active;
        let canLeave = isFutureEvent && !isMyEvent && iWillVisitIt && visitorsEnum.deniedUsers.find(x => x.id === current_user.id) == null && eventStatus === eventStatusEnum.Active;
        let canCancel = isFutureEvent && current_user.id != null && isMyEvent && eventStatus !== eventStatusEnum.Canceled;
        let canUncancel = isFutureEvent && isMyEvent && eventStatus === eventStatusEnum.Canceled;
        let isMyPrivateEvent = isMyEvent && !isPublic;

        return <>
            <div className="container-fluid mt-1">
                <div className="row">
                    <div className="col-9">
                        <div className="col-12">
                            <img src={this.state.eventImage}
                                id={"eventFullPhotoImg" + id} alt="Event"
                                className="w-100" />
                            <div className="text-block">
                                <span className="title">{title}</span>
                                <br />
                                {(isPublic)
                                    ? <span>Public event</span>
                                    : <span>Private event</span>
                                }
                                <br />
                                {(maxParticipants < INT32_MAX_VALUE)
                                    ? <span className="maxParticipants">
                                        {visitorsEnum.approvedUsers.length}/{maxParticipants}
                                        <span className="pl-2">Participants</span>
                                    </span>
                                    : <span className="maxParticipants">
                                        {visitorsEnum.approvedUsers.length}
                                        <span className="pl-2">Participants</span>
                                    </span>
                                }
                                <br />
                                <span>
                                    <Moment format="D MMM YYYY" withTitle>
                                        {dateFrom}
                                    </Moment>
                                    {dateTo !== dateFrom &&
                                        <>-
                                            <Moment format="D MMM YYYY" withTitle>
                                                {dateTo}
                                            </Moment>
                                        </>
                                    }
                                </span>
                                <br />
                                {this.props.event.data.location &&
                                    <DisplayLocation
                                        location={this.props.event.data.location}
                                    />
                                }
                                {categories_list}
                            </div>
                            <div className="button-block">
                                {canEdit &&
                                    <Link to={`/editEvent/${id}`}>
                                        <button className="btn btn-edit mb-1">Edit</button>
                                    </Link>
                                }
                                {canCancel && <SimpleModalWithDetails
                                    button={<button className="btn btn-edit">Cancel</button>}
                                    submitCallback={this.props.onCancel}
                                    data="Are you sure?"
                                />}
                                {(canUncancel) && <SimpleModalWithDetails
                                    button={<button className="btn btn-edit">Undo cancel</button>}
                                    submitCallback={this.props.onUnCancel}
                                    data="Are you sure?"
                                />}
                            </div>
                        </div>

                        {!isFutureEvent &&
                            <div className="text-box overflow-auto shadow p-3 mx-3 mb-5 mt-2 bg-white rounded">
                                <RatingWrapper
                                    iWillVisitIt={iWillVisitIt}
                                    eventId={id}
                                    userId={current_user.id}
                                />
                            </div>
                        }
                        <div className="text-box-big overflow-auto shadow p-3 mx-3 mb-5 mt-2 bg-white rounded">
                            {(eventStatus === eventStatusEnum.Canceled) &&
                                <div className="text-center text-uppercase cancel-text">
                                    <i className="fas fa-exclamation-triangle text-warning"></i>
                                    <span> This event is canceled </span>
                                    <i className="fas fa-exclamation-triangle text-warning"></i>
                                    <br />
                                </div>
                            }
                            {description}
                        </div>
                        <div className="shadow p-3 mx-3 mb-5 mt-2 bg-white rounded">
                            <InventoryList
                                eventId={id} />
                        </div>

                        <div className="overflow-auto shadow p-3 mx-3 mb-5 mt-2 bg-white rounded">
                            <Comment match={this.props.match} />
                        </div>
                    </div>

                    <div className="col-3 overflow-auto shadow p-3 mb-5 bg-white rounded">
                        {(!isMyEvent) &&
                            <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                <div className="d-flex justify-content-center">
                                    {this.getUserEventStatus(visitors.find(x => x.id === current_user.id))}
                                </div>
                                <br />
                                {canJoin &&
                                    <button onClick={this.props.onJoin}
                                        type="button"
                                        className="btn btn-success join-leave"
                                        variant="contained"
                                    >
                                        Join
                                    </button>}
                                {canLeave &&
                                    <EventLeaveModal data={{}}
                                        submitLeave={this.props.onLeave}
                                        status={false} />}
                            </div>
                        }
                        <EventVisitors data={{}}
                            admins={owners}
                            visitors={visitorsEnum}
                            isMyPrivateEvent={isMyPrivateEvent}
                            isMyEvent={isMyEvent}
                            current_user_id={current_user.id}
                        />
                    </div>
                </div>
            </div>
        </>
    }
}

