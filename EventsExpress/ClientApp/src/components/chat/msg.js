import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import * as moment from 'moment';
import ButtonBase from '@material-ui/core/ButtonBase';
import Avatar from '@material-ui/core/Avatar';
import { deleteSeenMsgNotification } from '../../actions/chat';
import './msg.css';

class Msg extends Component {
    componentDidUpdate = () => {
        if (this.props.notification.seen_messages.map(x => x.id).includes(this.props.item.id)) {
            this.props.item = this.props.notification.seen_messages.find(x => x.id == this.props.item.id);
            this.props.deleteSeenMsgNotification(this.props.item.id);
        }
    }

    getTime = (value) => {
        return moment.utc(value).fromNow();
    }

    render() {
        const { user, item, seenItem, current_user } = this.props;
        return <>
            {user.id != current_user.id
                ?
                <div className="d-flex justify-content-start mb-4">
                    <Link to={'/user/' + user.id}>
                        <ButtonBase>
                            {user.photoUrl
                                ? <Avatar className='SmallAvatar' src={user.photoUrl} />
                                : <Avatar className='SmallAvatar' >{user.username.charAt(0).toUpperCase()}</Avatar>}
                        </ButtonBase>
                    </Link>
                    <div className="msg_cotainer">
                        {item.text}<br />
                        <span className="msg_time">{this.getTime(item.dateCreated)}</span>
                    </div>
                </div>
                :
                <div className="d-flex justify-content-end mb-4">
                    <div className="msg_cotainer_send">
                        {item.text} {seenItem && <i className="fa fa-check"></i>}<br />
                        <span className="msg_time_send text-center">{this.getTime(item.dateCreated)}</span>
                    </div>
                </div>
            }
        </>
    }
}

const mapStateToProps = (state) => ({
    current_user: state.user,
    notification: state.notification
});

const mapDispatchToProps = (dispatch) => {
    return {
        deleteSeenMsgNotification: (id) => dispatch(deleteSeenMsgNotification(id))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Msg);
