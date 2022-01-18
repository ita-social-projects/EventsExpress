import React from 'react';
import SimpleModal from './simple-modal';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from "@material-ui/core/IconButton";
import { connect } from 'react-redux';
import { deleteFromOrganizers } from '../../actions/event/event-item-view-action';

const OwnersActions = (props) => {
    const { user, isMyEvent } = props;
    return (
        <>
            {(isMyEvent && user.id != props.currentUserId) &&
                <div>
                    <SimpleModal
                        action={() => props.deleteFromOrganizers(user.id, props.eventId)}
                        data={'Are you sure, that you wanna delete ' + user.username + ' from organizers?'}
                        button={
                            <Tooltip title="Delete from organizers">
                                <IconButton aria-label="delete">
                                    <i className="far fa-trash-alt" />
                                </IconButton>
                            </Tooltip>
                        }
                    />
                </div>
            }
        </>
    )
}

const mapStateToProps = (state) => ({
    eventId: state.event.data.id,
    currentUserId: state.user.id
});

const mapDispatchToProps = (dispatch) => ({
    deleteFromOrganizers: (userId, eventId) => dispatch(deleteFromOrganizers(userId, eventId)),
});

export default connect(mapStateToProps, mapDispatchToProps)(OwnersActions);
