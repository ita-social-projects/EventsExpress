import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import * as moment from 'moment';
import ButtonBase from '@material-ui/core/ButtonBase';
import Avatar from '@material-ui/core/Avatar';
import { deleteSeenMsgNotification } from '../../actions/chat/chat-action';
import './msg.css';
import { getTimeDifferenceFromNull } from '../helpers/TimeHelper';
import {userDefaultImage} from "../../constants/userDefaultImage";
import CustomAvatar from "../avatar/custom-avatar";

class Msg extends Component {
    componentDidUpdate = () => {
        if (this.props.notification.seen_messages.map(x => x.id).includes(this.props.item.id)) {
            this.props.item = this.props.notification.seen_messages.find(x => x.id === this.props.item.id);
            this.props.deleteSeenMsgNotification(this.props.item.id);
        }
    }

    render() {
        const { user, item, seenItem, current_user } = this.props;
        return <>
            {user.id !== current_user.id ?
                <div className="d-flex justify-content-start mb-4">
                    <Link to={'/user/' + user.id}>
                        <ButtonBase>
                            <CustomAvatar size={"Small"}
                                          userId={user.id}
                                          name={user.name}/>
                        </ButtonBase>
                    </Link>
                    <div className="msg_cotainer">
                        {item.text}<br />
                        <span className="msg_time">{getTimeDifferenceFromNull(item.dateCreated)}</span>
                    </div>
                </div>
                :
                <div className="d-flex justify-content-end mb-4">
                    <div className="msg_cotainer_send">
                        {item.text} {seenItem && <i className="fa fa-check"></i>}<br />
                        <span className="msg_time_send text-center">{getTimeDifferenceFromNull(item.dateCreated)}</span>
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
