import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Comment from '../comment/comment';
import EditEventWrapper from '../../containers/edit-event';
import CustomAvatar from '../avatar/custom-avatar';
import RatingWrapper from '../../containers/rating';
import IconButton from "@material-ui/core/IconButton";
import Moment from 'react-moment';
import SimpleModal from './simple-modal';
import 'moment-timezone';
import '../layout/colorlib.css';
import './event-item-view.css';
import Button from "@material-ui/core/Button";
import EventVisitors from './event-visitors';
import DeleteIcon from '@material-ui/icons/Delete';
import EventLeaveModal from './event-leave-modal';
import InventoryList from '../inventory/InventoryList';
import DisplayLocation from './map/display-location';
import Tooltip from '@material-ui/core/Tooltip';
import userStatusEnum from '../../constants/userStatusEnum';
import eventStatusEnum from '../../constants/eventStatusEnum';
import EventChangeStatusModal from './event-change-status-modal';
import Zoom from '@material-ui/core/Zoom';

export default class EventItemView extends Component {
    constructor() {
        super();

        this.state = {
            edit: false
        };
    }

    renderCategories = arr => {
        return arr.map(x => <span key={x.id}>#{x.name}</span>);
    }

    renderOwners = (arr, isMyEvent, current_user_id) => {
        return arr.map((x, key) => (
            <div key={key}>
                <div className="d-flex align-items-center border-bottom">
                    <div className="flex-grow-1">
                        <Link to={'/user/' + x.id} className="btn-custom">
                            <div className="d-flex align-items-center border-bottom">
                                <CustomAvatar size="little" photoUrl={x.photoUrl} name={x.username} />
                                <div>
                                    <h5>{x.username}</h5>
                                    {'Age: ' + this.getAge(x.birthday)}
                                </div>
                            </div>
                        </Link>
                    </div>
                    {(isMyEvent && x.id != current_user_id) &&
                        <div>
                            <SimpleModal
                                action={() => this.props.onDeleteFromOwners(x.id)}
                                data={'Are you sure, that you wanna delete ' + x.username + ' from owners?'}
                                button={
                                    <Tooltip title="Delete from owners">
                                        <IconButton aria-label="delete">
                                            <i className="far fa-trash-alt"></i>
                                        </IconButton>
                                    </Tooltip>
                                }
                            />
                        </div>
                    }
                </div>
            </div>
        ));
    }


    getClassName = (attitude) => {
        switch (attitude) {
            case 0:
                return "attitude-like";
            case 1:
                return "attitude-dislike";
            default:
                return '';
        }
    }

    renderApprovedUsers = (arr, isMyEvent, isMyPrivateEvent) => {
        return arr.map(x => (
            <div>
                <div className={"d-flex align-items-center border-bottom w-100 " + this.getClassName(x.attitude)} >
                    <div className="flex-grow-1" >
                        <Link to={'/user/' + x.id} className="btn-custom">
                            <div className="d-flex align-items-center">
                                <CustomAvatar size="little" photoUrl={x.photoUrl} name={x.username} />
                                <div>
                                    <h5>{x.username}</h5>
                                    {'Age: ' + this.getAge(x.birthday)}
                                </div>
                                {x.attitude === 0 &&
                                    <Tooltip title="You like this user" placement="bottom" TransitionComponent={Zoom}>
                                        <div className="retraet">
                                            <i class="far fa-thumbs-up cancel-text"></i>
                                        </div>
                                    </Tooltip>
                                }
                                {x.attitude === 1 &&
                                    <Tooltip title="You dislike this user" placement="bottom" TransitionComponent={Zoom}>
                                        <div className="retraet">
                                            <i class="far fa-thumbs-down cancel-text"></i>
                                        </div>
                                    </Tooltip>
                                }
                            </div>
                        </Link>
                    </div>
                    
                    {(isMyEvent) &&
                        <div>
                        
                            <SimpleModal
                                id={x.id}
                                action={() => this.props.onPromoteToOwner(x.id)}
                                data={'Are you sure, that you wanna approve ' + x.username + ' to owner?'}
                                button={
                                    <Tooltip title="Approve as an owner">
                                        <IconButton aria-label="delete">
                                            <i className="fas fa-plus-circle" ></i>
                                        </IconButton>
                                    </Tooltip>
                                }
                            />
                        </div>
                    }
                </div>
                {isMyPrivateEvent &&
                    <Button
                        onClick={() => this.props.onApprove(x.id, false)}
                        variant="outlined"
                        color="success"
                    >
                        Delete from event
                        </Button>
                }
            </div>
        ));
    }

    renderPendingUsers = (arr, isMyEvent) => {
        return arr.map(x => (
            <div>
                <div className="flex-grow-1">
                    <Link to={'/user/' + x.id} className="btn-custom">
                        <div className="d-flex align-items-center border-bottom">
                            <CustomAvatar size="little" photoUrl={x.photoUrl} name={x.username} />
                            <div>
                                <h5>{x.username}</h5>
                                {'Age: ' + this.getAge(x.birthday)}
                            </div>
                        </div>
                    </Link>
                </div>
                {(isMyEvent) &&
                    <div>
                        <IconButton aria-label="delete" onClick={() => this.props.onPromoteToOwner(x.id)}>
                            <DeleteIcon />
                        </IconButton>
                    </div>
                }
                <div>
                    <Button
                        variant="outlined"
                        color="success"
                        onClick={() => this.props.onApprove(x.id, true)}
                    >
                        Approve
                        </Button>
                    <Button
                        onClick={() => this.props.onApprove(x.id, false)}
                        variant="outlined"
                        color="danger"
                    >
                        Deny
                        </Button>
                </div>
            </div>)
        );
    }

    renderDeniedUsers = (arr, isMyEvent) => {
        return arr.map(x => (
            <div>
                <div className="flex-grow-1">
                    <Link to={'/user/' + x.id} className="btn-custom">
                        <div className="d-flex align-items-center border-bottom">
                            <CustomAvatar size="little" photoUrl={x.photoUrl} name={x.username} />
                            <div>
                                <h5>{x.username}</h5>
                                {'Age: ' + this.getAge(x.birthday)}
                            </div>
                        </div>
                    </Link>
                </div>
                {(isMyEvent) &&
                    <div>
                        <IconButton aria-label="delete" onClick={() => this.props.onPromoteToOwner(x.id)}>
                            <DeleteIcon />
                        </IconButton>
                    </div>
                }
                <Button
                    onClick={() => this.props.onApprove(x.id, true)}
                    variant="outlined"
                    color="success"
                >
                    Add to event
                </Button>
            </div>)
        );
    }

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

    onEdit = () => {
        this.setState({ edit: true });
    }

    render() {
        const { current_user } = this.props;
        const {
            id,
            photoUrl,
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
        let canCancel = isFutureEvent && current_user.id != null && isMyEvent && !this.state.edit && eventStatus !== eventStatusEnum.Canceled;
        let canUncancel = isFutureEvent && isMyEvent && !this.state.edit && eventStatus === eventStatusEnum.Canceled;
        let isMyPrivateEvent = isMyEvent && !isPublic;

        return <>
            <div className="container-fluid mt-1">
                <div className="row">
                    <div className="col-9">
                        <div className="col-12">
                            <img src={photoUrl} className="w-100" alt="Event" />
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
                                {canEdit && <button onClick={this.onEdit} className="btn btn-edit">Edit</button>}
                                {canCancel && <EventChangeStatusModal
                                    button={<button className="btn btn-edit">Cancel</button>}
                                    submitCallback={this.props.onCancel}           
                                    />}
                                {(canUncancel) && <EventChangeStatusModal
                                    button={<button className="btn btn-edit">Undo cancel</button>}
                                    submitCallback={this.props.onUnCancel}
                                />}
                            </div>
                        </div>
                        {this.state.edit
                            ? <div className="row shadow mt-5 p-5 mb-5 bg-white rounded">
                                <EditEventWrapper
                                    onCancelEditing={() => this.setState({ edit: false })}
                                />
                            </div>
                            : <>
                                {!isFutureEvent &&
                                    <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                        <RatingWrapper
                                            iWillVisitIt={iWillVisitIt}
                                            eventId={id}
                                            userId={current_user.id}
                                        />
                                    </div>
                                }
                                <div className="text-box-big overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
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
                                <div className="shadow p-3 mb-5 mt-2 bg-white rounded">
                                    <InventoryList
                                        eventId={id} />
                                </div>

                                <div className="overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                                    <Comment match={this.props.match} />
                                </div>
                            </>
                        }
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
                            renderOwners={this.renderOwners}
                            visitors={visitorsEnum}
                            renderApprovedUsers={this.renderApprovedUsers}
                            isMyPrivateEvent={isMyPrivateEvent}
                            isMyEvent={isMyEvent}
                            current_user_id={current_user.id}
                            renderPendingUsers={this.renderPendingUsers}
                            renderDeniedUsers={this.renderDeniedUsers}
                        />
                    </div>
                </div>
            </div>
        </>
    }
}

