import React from 'react';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import { connect } from 'react-redux';
import { leave } from '../../../../actions/event/event-item-view-action';
import EventLeaveModal from '../../event-leave-modal';
import { eventCanBeLeaved, userCanLeave } from './event-checks';

const LeaveButton = ({ event, currentUser, onLeave, ...props }) => {
    const canLeave = eventCanBeLeaved(event) && userCanLeave(event, currentUser);

    return (
        <>
            {canLeave && (
                <EventLeaveModal
                    submitLeave={() => {
                        props.leave(currentUser.id, event.id);
                        onLeave();
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
    leave: (userId, eventId) => dispatch(leave(userId, eventId))
});

export default connect(mapStateToProps, mapDispatchToProps)(LeaveButton);
