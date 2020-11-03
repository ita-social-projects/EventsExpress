import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import get_chats from '../../actions/chats';
import { connect } from 'react-redux';
import ButtonBase from '@material-ui/core/ButtonBase';
import Avatar from '@material-ui/core/Avatar';
import Spinner from '../spinner';
import './user_chats.css';

class UserChats extends Component {
    componentWillMount = () => {
        this.props.getChats();
    }

    renderChats = (arr) => {
        return arr.map(x => {
            const user = x.users.find(y => y.id !== this.props.current_user.id);
            const new_msg = this.props.notification.messages.filter(y => y.chatRoomId === x.id);
            const chatBg = new_msg.length > 0 ? 'new-msgs' : '';

            return <>
                <div key={x.id} className="w-100">
                    <Link to={`/chat/${x.id}`}>
                        <div className={chatBg + " col-12 d-flex"}>
                            <ButtonBase>
                                {user.photoUrl
                                    ? <Avatar className='SmallAvatar' src={user.photoUrl} />
                                    : <Avatar className='SmallAvatar' >{user.username.charAt(0).toUpperCase()}</Avatar>
                                }
                            </ButtonBase>
                            <div className="my-auto ml-5"><h5>{user.username}</h5>
                                {new_msg.length > 0
                                    ? <span className="text-info">You have {new_msg.length} unread messages</span>
                                    : <span className="text-info">{x.lastMessage}</span>
                                }
                            </div>
                        </div>
                        <p> </p>
                    </Link>
                    <hr />
                </div>
            </>
        });
    }

    render() {
        const { isPending } = this.props.chats;
        const data = this.props.chats.data.sort(function (b, a) {
            return new Date(a.lastMessageTime).getTime() - new Date(b.lastMessageTime).getTime();
        });
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending
            ? <div className="row shadow mt-5 p-5 mb-5 bg-white rounded">
                {this.renderChats(data)}
            </div>
            : null;
        return <>
            {spinner}
            {content}
        </>
    }
}

const mapStateToProps = (state) => ({
    chats: state.chats,
    chat: state.chat,
    current_user: state.user,
    notification: state.notification
});

const mapDispatchToProps = (dispatch) => {
    return {
        getChats: () => dispatch(get_chats()),
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(UserChats);
