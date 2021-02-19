import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import Profile from '../components/profile/user-profile';
import Spinner from '../components/spinner';
import get_user, { setAttitude, reset_user } from '../actions/user-item-view';
import {
    get_future_events,
    get_past_events,
    get_visited_events,
    get_events_togo
} from '../actions/events-for-profile-action';
import BadRequest from '../components/Route guard/400';
import Forbidden from '../components/Route guard/403';

class UserItemViewWrapper extends Component {
    state = {
        flag: false
    }

    componentWillMount = () => {
        const { id } = this.props.match.params;
        this.props.get_user(id);
    }

    componentWillUnmount() {
        this.props.reset_user();
    }

    componentWillUpdate = (newProps) => {
        if (newProps.match.params.id !== this.props.match.params.id)
            this.props.get_user(newProps.match.params.id);
        if (newProps.current_user != this.props.current_user)
            this.props.get_user(newProps.match.params.id);
    }

    onLike = () => {
        this.props.setAttitude({
            userFromId: this.props.current_user,
            userToId: this.props.profile.data.id, attitude: 0
        });
    }

    onDislike = () => {
        this.props.setAttitude({
            userFromId: this.props.current_user,
            userToId: this.props.profile.data.id, attitude: 1
        });
    }

    onReset = () => {
        this.props.setAttitude({
            userFromId: this.props.current_user,
            userToId: this.props.profile.data.id, attitude: 2
        });
    }

    onFuture = (page) => {
        this.setState({ flag: false });
        this.props.get_future_events(this.props.profile.data.id, page);
    }

    onPast = (page) => {
        this.setState({ flag: false });
        this.props.get_past_events(this.props.profile.data.id, page);
    }

    onVisited = (page) => {
        this.setState({ flag: false });
        this.props.get_visited_events(this.props.profile.data.id, page);
    }

    onToGo = (page) => {
        this.setState({ flag: false });
        this.props.get_events_togo(this.props.profile.data.id, page);
    }

    onAddEvent = () => {
        this.setState({ flag: true });
    }

    render() {
        const { data, isPending, isError } = this.props.profile;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect from="*" to="/home/events" />
                : isError.ErrorCode == '401'
                    ? <Redirect from="*" to="/home/events" />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending && errorMessage === null
            ? <Profile
                onAddEvent={this.onAddEvent}
                add_event_flag={this.state.flag}
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
            />
            : null;

        return <>
            {spinner || errorMessage}
            {content}
        </>
    }
}

const mapStateToProps = (state) => ({
    profile: state.profile,
    current_user: state.user.id,
    events: state.events_for_profile
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_user: (id) => dispatch(get_user(id)),
        setAttitude: (values) => dispatch(setAttitude(values)),
        get_past_events: (id, page) => dispatch(get_past_events(id, page)),
        get_future_events: (id, page) => dispatch(get_future_events(id, page)),
        get_visited_events: (id, page) => dispatch(get_visited_events(id, page)),
        get_events_togo: (id, page) => dispatch(get_events_togo(id, page)),
        reset_user: () => dispatch(reset_user())
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UserItemViewWrapper);
