 import React, {Component} from 'react';
import get_chats from '../../actions/chats';
 import { connect } from 'react-redux';
 import { Link } from 'react-router-dom';

class UserChats extends Component{

    componentWillMount = () => {
        this.props.getChats();
    }

    renderChats = (arr) =>{
        return arr.map(x => (<Link to={`/chat/${x.id}`}>{x.users[0].username}</Link>));
    }

    render(){
        return <>
            {this.renderChats(this.props.chats.data)}
        </>
    }

 }


 
const mapStateToProps = (state) => ({
    chats: state.chats
});

const mapDispatchToProps = (dispatch) => { 
   return {
        getChats: () => dispatch(get_chats())
   } 
};

export default connect(mapStateToProps, mapDispatchToProps)(UserChats);