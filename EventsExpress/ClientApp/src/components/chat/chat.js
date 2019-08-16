import React, { Component} from 'react';
import getChat, {initialConnection} from '../../actions/chat';
 import { connect } from 'react-redux';

class Chat extends Component{

    componentDidMount = () =>{
      this.props.initialConnection();
      this.props.getChat(this.props.match.params.chatId);
    }

    Send = () =>{
        this.props.chat.hubConnection
        .invoke('send', 1)
        .catch(err => console.error(err));
    }

    render(){
        return <>
        <span onClick={this.Send}>chat room</span>
        </>
    }
}


const mapStateToProps = (state) => ({
    current_user: state.user,
    chat: state.chat
});

const mapDispatchToProps = (dispatch) => { 
   return {
        initialConnection: () => dispatch(initialConnection()),
        getChat: (chatId) => dispatch(getChat(chatId))
   } 
};

export default connect(mapStateToProps, mapDispatchToProps)(Chat);