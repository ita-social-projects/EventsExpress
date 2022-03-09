import React from 'react';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import { connect } from 'react-redux';
import { join } from '../../../../actions/event/event-item-view-action';
import { get_events } from '../../../../actions/event/event-list-action';
import { eventCanBeJoined, isUserLoggedIn, userCanAttend } from './event-checks';

const JoinButton = ({ event, currentUser, onJoin, ...props }) => {
    const canJoin = isUserLoggedIn(currentUser) && eventCanBeJoined(event) && userCanAttend(event, currentUser);

    return (
        <>
            {canJoin && (
                <Tooltip title="Join event">
                    <IconButton
                        key={+canJoin}
                        onClick={() => {
                            props.join(currentUser.id, event.id);
                            onJoin();
                        }}
                    >
                        <i className="fas fa-plus-square" />
                    </IconButton>
                </Tooltip>
            )}
        </>
    );
};

const mapStateToProps = state => ({
    currentUser: state.user
});

const mapDispatchToProps = dispatch => ({
    getEvents: query => dispatch(get_events(query)),
    join: (userId, eventId) => dispatch(join(userId, eventId))
});

export default connect(mapStateToProps, mapDispatchToProps)(JoinButton);
