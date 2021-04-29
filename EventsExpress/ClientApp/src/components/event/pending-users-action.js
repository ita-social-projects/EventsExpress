import React from 'react';
import Button from "@material-ui/core/Button";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from '@material-ui/icons/Delete';
import { connect } from 'react-redux';
import { promoteToOwner, approveUser } from '../../actions/event/event-item-view-action';

const PendingUsersActions = (props) => {
    const { user, isMyEvent } = props;
    return (
        <>
            {(isMyEvent) &&
                <div>
                <IconButton aria-label="delete" onClick={() => props.promoteToOwner(user.id, props.eventId)}>
                        <DeleteIcon />
                    </IconButton>
                </div>
            }
            <Button
                variant="outlined"
                color="success"
                onClick={() => props.approveUser(user.id, props.eventId, true)}
            >
                Approve
                        </Button>
            <Button
                onClick={() => props.approveUser(user.id, props.eventId, false)}
                variant="outlined"
                color="danger"
            >
                Deny
        </Button>
        </>
    )
}

const mapStateToProps = (state) => ({
    eventId: state.event.data.id
});

const mapDispatchToProps = (dispatch) => ({
    approveUser: (userId, eventId, buttonAction) => dispatch(approveUser(userId, eventId, buttonAction)),
    promoteToOwner: (userId, eventId) => dispatch(promoteToOwner(userId, eventId))
});


export default connect(mapStateToProps, mapDispatchToProps)(PendingUsersActions);