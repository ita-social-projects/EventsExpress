import React, {Component} from 'react';
import { connect } from 'react-redux';
import Profile from '../components/profile/user-profile';
import Spinner from '../components/spinner';
import get_user, { setAttitude } from '../actions/user-item-view';


class UserItemViewWrapper extends Component{
    
    componentDidMount(){    
        const { id } = this.props.match.params;
        console.log(id);
        this.props.get_user(id);
    }

    onLike = () => {
        this.props.setAttitude({ userFromId: this.props.current_user, userToId: this.props.profile.data.id, attitude: 0 });
    }

    onDislike = () => {
        this.props.setAttitude({ userFromId: this.props.current_user, userToId: this.props.profile.data.id, attitude: 1 });
    }

    render(){   
    
        const {data, isPending, isError} = this.props.profile;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <Profile onLike={this.onLike} onDislike={this.onDislike} data={data} current_user={this.props.current_user} /> : null;
    
        return <>
                {spinner}
                {content}
               </>
    }
}

const mapStateToProps = (state) => ({ profile: state.profile, current_user: state.user.id });

const mapDispatchToProps = (dispatch) => { 
    return {
        get_user: (id) => dispatch(get_user(id)),
        setAttitude: (userId, profileId, attitude) => dispatch(setAttitude(userId, profileId, attitude))
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(UserItemViewWrapper);