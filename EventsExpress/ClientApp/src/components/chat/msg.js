import React, { Component} from 'react';
import {Link} from 'react-router-dom';
 import { connect } from 'react-redux';
 import ButtonBase from '@material-ui/core/ButtonBase';
 import Avatar from '@material-ui/core/Avatar';
 import { deleteSeenMsgNotification } from '../../actions/chat';
import './msg.css';
class Msg extends Component{

    componentDidUpdate = () => {
        if(this.props.notification.seen_messages.map(x => x.id).includes(this.props.item.id)){
            this.props.item = this.props.notification.seen_messages.find(x => x.id == this.props.item.id);
            this.props.deleteSeenMsgNotification(this.props.item.id);
        }
    }

    getTime = (time) => {
        let today = new Date();
        let times = new Date(time);
        var age = today.getFullYear() - times.getFullYear();
        if (age != 0) return `${age} years ago`;
        if ((today.getMonth() - times.getMonth()) != 0) return `${today.getMonth() - times.getMonth()} months ago`;
        if ((today.getDate() - times.getDate()) != 0) return `${today.getDate() - times.getDate()} days ago`;
        if ((today.getHours() - times.getHours()) != 0) return `${today.getHours() - times.getHours()} hours ago`;
        if ((today.getMinutes() - times.getMinutes()) != 0) return `${today.getMinutes() - times.getMinutes()} minutes ago`;
        return `right now`;
    }

   render(){
        const { user, item, seenItem ,current_user } = this.props;
        console.log(item);
        return <>

            {user.id != current_user.id ? 
            <div className="d-flex justify-content-start mb-4">
                <Link to={'/user/'+user.id}>
                <ButtonBase>
                {user.photoUrl
                        ? <Avatar className='SmallAvatar' src={user.photoUrl} />
                        : <Avatar className='SmallAvatar' >{user.username.charAt(0).toUpperCase()}</Avatar>}
                </ButtonBase>
                </Link>
                <div className="msg_cotainer">
                    {item.text}<br/>
                    <span className="msg_time">{this.getTime(item.dateCreated)}</span>
                </div>
            </div>
            :
            <div className="d-flex justify-content-end mb-4">
                <div className="msg_cotainer_send">
                    {item.text} {seenItem && <i className="fa fa-check"></i>}<br/>
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

