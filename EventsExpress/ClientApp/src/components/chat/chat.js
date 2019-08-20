import React, { Component} from 'react';
import getChat, {initialConnection, reset} from '../../actions/chat';
 import { connect } from 'react-redux';
 import Button from "@material-ui/core/Button";
 import { renderTextArea } from '../helpers/helpers';
 import { reduxForm, Field, reset as resetForm } from 'redux-form';
 import ButtonBase from '@material-ui/core/ButtonBase';
 import Avatar from '@material-ui/core/Avatar';
 import Msg from './msg';
 import {
 RECEIVE_MESSAGE
}from '../../actions/chat';

class Chat extends Component{

    componentWillMount = () =>{
      this.props.getChat(this.props.match.params.chatId);
    }

    componentDidUpdate = () => {
        document.getElementsByClassName('card-body')[0].scrollTop = document.getElementsByClassName('card-body')[0].scrollHeight;
    }

    componentWillUnmount = () =>{ 
        console.log('Unmount', this.props);
        this.props.resetChat();
    }

    Send = (e) =>{
        e.preventDefault();
        this.props.hubConnection
        .invoke('send', this.props.chat.data.id, e.target.msg.value)
        .catch(err => console.error(err));
        this.props.resetForm();
    }

    renderMessages = (arr) =>{
        if(this.props.chat.isSuccess)
            return arr.messages.map(x => {
                var sender = arr.users.find(y => y.id == x.senderId);
                return <>
                    <Msg key={x.id} user={sender} item={x} />
                </>
        })
    }

    render(){
        var sender = this.props.chat.data.users.find(y => y.id != this.props.current_user.id);
        return <>
			<div className="row justify-content-center h-100 mt-2">
                <div className="col-md-8 col-xl-8 chat">
                <div className="card">
						<div className="card-header msg_head">
							<div className="d-flex bd-highlight">
                                {sender != null &&
                            <ButtonBase>
                            {sender.photoUrl
                                    ? <Avatar className='SmallAvatar' src={sender.photoUrl} />
                                    : <Avatar className='SmallAvatar' >{sender.username.charAt(0).toUpperCase()}</Avatar>}
                            </ButtonBase>
                                }
								<div className="user_info">
									<span>Chat with {sender != null && sender.username}</span>
									<p>1767 Messages</p>
								</div>
							</div>
						</div>
                        
						<div className="card-body msg_card_body">
                            {this.renderMessages(this.props.chat.data)}
                        </div>
                    <div className="card-footer">
                    <form className="w-100 d-flex" onSubmit={this.Send}>
                        <Field name='msg' component={renderTextArea} rows="1" type="input" label="Send message..." />
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
    chat: state.chat
});

const mapDispatchToProps = (dispatch) => { 
   return {
    initialConnection: () => dispatch(initialConnection()),
    resetChat: () => { dispatch(reset())},
    getChat: (chatId) => dispatch(getChat(chatId)),
    resetForm: () => dispatch(resetForm('chat-form'))
   } 
};

Chat = reduxForm({
    form: 'chat-form'
  })(Chat);

export default connect(mapStateToProps, mapDispatchToProps)(Chat);

