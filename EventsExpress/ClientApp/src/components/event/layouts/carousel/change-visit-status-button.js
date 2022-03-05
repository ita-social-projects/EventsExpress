import React from 'react';
import moment from 'moment';
import eventStatusEnum from '../../../../constants/eventStatusEnum';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import { connect } from 'react-redux';
import { join, leave } from '../../../../actions/event/event-item-view-action';
import EventLeaveModal from '../../event-leave-modal';
import { useFilterActions } from '../../filter/filter-hooks';
import { get_events } from '../../../../actions/event/event-list-action';

const ChangeVisitStatusButton = ({ event, currentUser, ...props }) => {
    const { id, dateFrom, members, organizers, maxParticipants, isOnlyForAdults, eventStatus } = event;
    const { getQueryWithRequestFilters } = useFilterActions();

    const refreshEvents = () => {
        const filterQuery = getQueryWithRequestFilters();
        props.getEvents(filterQuery);
    };

    const visitorsEnum = {
        approvedUsers: members.filter(x => x.userStatusEvent === 0),
        deniedUsers: members.filter(x => x.userStatusEvent === 1),
        pendingUsers: members.filter(x => x.userStatusEvent === 2)
    };

    const today = moment().startOf('day');
    const iWillVisitIt = members.find(x => x.id === currentUser.id);
    const isFutureEvent = new Date(dateFrom) >= new Date().setHours(0, 0, 0, 0);
    const isMyEvent = organizers.find(x => x.id === currentUser.id) !== undefined;
    const isFreePlace = visitorsEnum.approvedUsers.length < maxParticipants;
    const isAdult = moment.duration(today.diff(moment(currentUser.birthday))).asYears() >= 18;
    const isAppropriateAge = !isOnlyForAdults || isAdult;

    const canJoin = isFutureEvent && isFreePlace && !iWillVisitIt && !isMyEvent
        && eventStatus === eventStatusEnum.Active && isAppropriateAge && currentUser.id;
    const canLeave = isFutureEvent && !isMyEvent && iWillVisitIt
        && visitorsEnum.deniedUsers.find(x => x.id === currentUser.id) == null
        && eventStatus === eventStatusEnum.Active;

    return (
        <>
            {canJoin && (
                <Tooltip title="Join event">
                    <IconButton
                        key={+canJoin}
                        onClick={() => {
                            props.join(currentUser.id, id);
                            refreshEvents();
                        }}
                    >
                        <i className="fas fa-plus-square" />
                    </IconButton>
                </Tooltip>
            )}
            {canLeave && (
                <EventLeaveModal
                    submitLeave={() => {
                        props.leave(currentUser.id, id);
                        refreshEvents();
                    }}
                    openButton={handleClickOpen => (
                        <Tooltip title="Leave event">
                            <IconButton key={+canLeave} onClick={handleClickOpen}>
                                <i className="fas fa-check-square" />
                            </IconButton>
                        </Tooltip>
                    )}
                />
            )}
        </>
    );
};

const mapStateToProps = state => ({
    currentUser: state.user
});

const mapDispatchToProps = dispatch => ({
    getEvents: query => dispatch(get_events(query)),
    join: (userId, eventId) => dispatch(join(userId, eventId)),
    leave: (userId, eventId) => dispatch(leave(userId, eventId))
});

export default connect(mapStateToProps, mapDispatchToProps)(ChangeVisitStatusButton );
