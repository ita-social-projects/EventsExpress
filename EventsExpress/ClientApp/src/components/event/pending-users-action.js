import React from 'react';
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/styles";
import { connect } from 'react-redux';
import { promoteToOrganizer, approveUser } from '../../actions/event/event-item-view-action';

const useStyles = makeStyles((theme) => ({
    success: {
        color: '#fff',
        backgroundColor: '#4caf50',
        '&:hover': {
            backgroundColor: '#388e3c'
        }
    },
    danger: {
        color: '#fff',
        backgroundColor: '#f44336',
        '&:hover': {
            backgroundColor: '#d32f2f'
        }
    }
}));

const PendingUsersActions = (props) => {
    const { user, isMyEvent } = props;
    const classes = useStyles();

    return (
        <>
            {(isMyEvent) &&
                <>
                    <Button
                        variant="contained"
                        className={classes.success}
                        onClick={() => props.approveUser(user.id, props.eventId, true)}
                    >
                        Approve
                    </Button>
                    <Button
                        variant="contained"
                        className={classes.danger}
                        onClick={() => props.approveUser(user.id, props.eventId, false)}
                    >
                            Deny
                    </Button>
                </>
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


export default connect(mapStateToProps, mapDispatchToProps)(PendingUsersActions);
