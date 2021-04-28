import React, { Component } from 'react';
import getChat, { initialConnection, reset, concatNewMsg, deleteOldNotififcation } from '../../actions/chat/chat-action';
import { connect } from 'react-redux';
import Button from "@material-ui/core/Button";
import { renderTextArea } from '../helpers/helpers';
import { reduxForm, Field, reset as resetForm } from 'redux-form';
import ButtonBase from '@material-ui/core/ButtonBase';
import Avatar from '@material-ui/core/Avatar';
import Msg from './msg';
import Spinner from '../spinner';
import {userDefaultImage} from "../../constants/userDefaultImage";
import CustomAvatar from "../avatar/custom-avatar";

class Chat extends Component {

    componentWillMount = () => {
        this.props.getChat(this.props.match.params.chatId);
    }

    componentDidUpdate = () => {
        if (!this.props.chat.isPending) {
            document.getElementsByClassName('card-body')[0].scrollTop = document.getElementsByClassName('card-body')[0].scrollHeight;
        }

        var newMsg = this.props.notification.messages.filter(x => x.chatRoomId == this.props.chat.data.id && !this.props.chat.data.messages.map(y => y.id).includes(x.id));

        if (newMsg.length > 0) {
            this.props.concatNewMsg(newMsg);
            this.props.deleteOldNotififcation(newMsg.map(x => x.id));
        }

        var msgIds = this.props.chat.data.messages.filter(x => (!x.seen && x.senderId != this.props.current_user.id)).map(x => x.id);

        if (msgIds.length > 0) {
            this.props.hubConnection
                .invoke('seen', msgIds)
                .catch(err => { console.log('error'); console.error(err) });
        }

        var deleteMsg = this.props.notification.messages.filter(x => x.chatRoomId == this.props.chat.data.id && this.props.chat.data.messages.map(y => y.id).includes(x.id));

        if (deleteMsg.length > 0) {
            this.props.deleteOldNotififcation(deleteMsg.map(x => x.id));
        }
    }

    componentWillUnmount = () => {
        this.props.resetChat();
    }

    Send = (e) => {
        e.preventDefault();
        if (e.target.msg.value != "") {
            this.props.hubConnection
                .invoke('send', this.props.chat.data.id, e.target.msg.value)
                .catch(err => console.error(err));
        }
        this.props.resetForm();
    }

    renderMessages = (arr) => {
        if (this.props.chat.isSuccess) {
            return arr.messages.map(x => {
                var sender = arr.users.find(y => y.id == x.senderId);
                if (arr.id == x.chatRoomId) {
                    return <>
                        <Msg key={x.id + x.seen} user={sender} seenItem={x.seen} item={x} />
                    </>
                }
                else {
                    return <>
                        <Msg />
                    </>
                }
            });
        }
    }

    render() {
        var sender = this.props.chat.data.users.find(y => y.id != this.props.current_user.id);
        const { isPending } = this.props.chat;
        return isPending ? <Spinner /> : <>
            <div className="row justify-content-center h-100 mt-2">
                <div className="col-md-8 col-xl-8 chat">
                    <div className="card">
                        <div className="card-header msg_head">
                            <div className="d-flex bd-highlight">
                                {sender != null &&
                                    <ButtonBase>
                                        <CustomAvatar size={"Small"}
                                            userId={sender.id}
                                            name={sender.name}/>
                                    </ButtonBase>
                                }
                                <div className="user_info">
                                    <span>Chat with {sender != null && sender.username}</span>
                                    <p>{this.props.chat.data.messages.length} Messages</p>
                                </div>
                            </div>
                        </div>

                        <div className="card-body msg_card_body">
                            {this.renderMessages(this.props.chat.data)}
                        </div>
                        <div className="card-footer">
                            <form className="w-100 d-flex" autocomplete="off" onSubmit={this.Send}>
                                <Field name='msg' component={renderTextArea} type="input" autocomplete="off" label="Send message..." />
                                <Button fullWidth={true} type="submit" color="primary" className="w-25">
                                    Send
                        </Button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </>
    }
}

const mapStateToProps = (state) => ({
    current_user: state.user,
    hubConnection: state.hubConnection,
    chat: state.chat,
    notification: state.notification
});

const mapDispatchToProps = (dispatch) => {
    return {
        initialConnection: () => dispatch(initialConnection()),
        resetChat: () => { dispatch(reset()) },
        getChat: (chatId) => dispatch(getChat(chatId)),
        concatNewMsg: (data) => dispatch(concatNewMsg(data)),
        resetForm: () => dispatch(resetForm('chat-form')),
        deleteOldNotififcation: (data) => dispatch(deleteOldNotififcation(data))
    }
};

Chat = reduxForm({
    form: 'chat-form'
})(Chat);

export default connect(mapStateToProps, mapDispatchToProps)(Chat);
