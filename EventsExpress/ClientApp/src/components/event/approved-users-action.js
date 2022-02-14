import React from 'react';
import SimpleModal from './simple-modal';
import Button from "@material-ui/core/Button";
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from "@material-ui/core/IconButton";
import { connect } from 'react-redux';
import { promoteToOrganizer, approveUser } from '../../actions/event/event-item-view-action';

const ApprovedUsersActions = (props) => {
    const { user, isMyEvent, isMyPrivateEvent } = props;

    return (
        <>
            {(isMyEvent) &&
                <div>
                    <SimpleModal
                        id={user.id}
                        action={() => props.promoteToOrganizer(user.id, props.eventId)}
                        data={'Are you sure, that you wanna approve ' + user.username + ' to organizer?'}
                        button={
                            <Tooltip title="Approve as an organizer">
                                <IconButton aria-label="delete">
                                    <i className="fas fa-plus-circle" />
                                </IconButton>
                            </Tooltip>
                        }
                    />
                </div>
            }
            {(isMyPrivateEvent) &&
                <Button
                    onClick={() => props.approveUser(user.id, props.eventId, false)}
                    variant="outlined"
                    color="success"
                >
                    Delete from event
        </Button>
            }
        </>
    )
}

const mapStateToProps = (state) => ({
    eventId: state.event.data.id
});

const mapDispatchToProps = (dispatch) => ({
    approveUser: (userId, eventId, buttonAction) => dispatch(approveUser(userId, eventId, buttonAction)),
    promoteToOrganizer: (userId, eventId) => dispatch(promoteToOrganizer(userId, eventId))
});

export default connect(mapStateToProps, mapDispatchToProps)(ApprovedUsersActions);
