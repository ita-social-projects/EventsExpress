 import React, {Component} from 'react';
import get_chats from '../../actions/chats';
 import { connect } from 'react-redux';
 import { Link } from 'react-router-dom';
 import ButtonBase from '@material-ui/core/ButtonBase';
 import Avatar from '@material-ui/core/Avatar';
 import './user_chats.css';
class UserChats extends Component{

    componentWillMount = () => {
        this.props.getChats();
    }

    renderChats = (arr) =>{
        
        return arr.map(x => {
        
            const user = x.users.find(y => y.id != this.props.current_user.id);
            const new_msg = this.props.notification.messages.filter(y => y.chatRoomId == x.id);
        return <>
        <div key={x.id} className="w-100">
        <Link to={`/chat/${x.id}`}>   
            <div className="col-12 d-flex">                                        
            <ButtonBase>
            {user.photoUrl
                        ? <Avatar className='SmallAvatar' src={user.photoUrl} />
                        : <Avatar className='SmallAvatar' >{user.username.charAt(0).toUpperCase()}</Avatar>}
            </ButtonBase><div className="my-auto ml-5"><h5>{user.username}</h5>
                {new_msg.length > 0 ? <span className="text-info">You have {new_msg.length} unread messages</span> : <span className="text-info">{x.lastMessage}</span>}</div>
            </div>
                <p> </p> 
        </Link>
        <hr/>
        </div>
        </>
        });
    }

    render(){
        return <>
        <div className="row shadow mt-5 p-5 mb-5 bg-white rounded">
    
            {this.renderChats(this.props.chats.data)}
        </div>
        
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