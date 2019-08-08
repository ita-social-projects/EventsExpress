import React, {Component} from 'react';
import { connect } from 'react-redux';
import Profile from '../components/profile/user-profile';
import Spinner from '../components/spinner';
import get_user, { setAttitude } from '../actions/user-item-view';
import get_future_events, { get_past_events, get_visited_events, get_events_togo} from '../actions/events-for-profile';


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

    onReset = () => {
        this.props.setAttitude({ userFromId: this.props.current_user, userToId: this.props.profile.data.id, attitude: 2 });
    }

    onFuture = () => {
        this.props.get_future_events(this.props.profile.data.id);
    }

    onPast = () => {
        this.props.get_past_events(this.props.profile.data.id);
    }

    onVisited = () => {
        this.props.get_visited_events(this.props.profile.data.id);
    }

    onToGo = () => {
        this.props.get_events_togo(this.props.profile.data.id);
    }

    render(){   
    
        const {data, isPending, isError} = this.props.profile;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <Profile
            onLike={this.onLike}
            onDislike={this.onDislike}
            onReset={this.onReset}
            events={this.props.events}
            onFuture={this.onFuture}
            onPast={this.onPast}
            onVisited={this.onVisited}
            onToGo={this.onToGo}
            data={data}
            current_user={this.props.current_user}
        /> : null;
    
        return <>
                {spinner}
                {content}
               </>
    }
}

const mapStateToProps = (state) => ({
    profile: state.profile,
    current_user: state.user.id,
    events: state.events_for_profile.data
});

const mapDispatchToProps = (dispatch) => { 
    return {
        get_user: (id) => dispatch(get_user(id)),
        setAttitude: (values) => dispatch(setAttitude(values)),
        get_past_events: (id) => dispatch(get_past_events(id)),
        get_future_events: (id) => dispatch(get_future_events(id)),
        get_visited_events: (id) => dispatch(get_visited_events(id)),
        get_events_togo: (id) => dispatch(get_events_togo(id))
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(UserItemViewWrapper);