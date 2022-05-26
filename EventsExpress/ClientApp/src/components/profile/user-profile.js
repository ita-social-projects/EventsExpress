import React, { Component } from 'react';
import { Link, Redirect, Route, Switch, withRouter } from 'react-router-dom';
import 'moment-timezone';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import genders from '../../constants/GenderConstants';
import Event from '../event/event-item';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average';
import './User-profile.css';
import Events from './events';
import AuthComponent from "../../security/authComponent";
import { getAge } from '../helpers/get-age-string';

class UserItemView extends Component {

    splitPath(path) {
        var n = path.toLowerCase().split("/");
        return n[n.length - 1];
    }

    indexToTabName = {
        "futureevents": 0,
        "archiveevents": 1,
        "visitedevents": 2,
        "eventstogo": 3
    };

    state = {
        value: this.indexToTabName[this.splitPath(this.props.history.location.pathname)]
    };

    componentDidMount = () => {
        this.state.value = this.indexToTabName[this.splitPath(this.props.history.location.pathname)]
    }

    componentDidUpdate = () => {
        this.state.value = this.indexToTabName[this.splitPath(this.props.history.location.pathname)]
    }

    renderCategories = arr => arr.map(item => <div key={item.id}>#{item.name}</div>)
    renderEvents = arr => arr.map(item => <div className="col-4"><Event key={item.id} item={item} /></div>)

    handleChange = (event, value) => {
        this.setState({ value });
        value === 0 && (this.props.onFuture())
        value === 1 && (this.props.onPast())
        value === 2 && (this.props.onVisited())
        value === 3 && (this.props.onToGo())
    };

    render() {
        const {
            firstName,
            lastName,
            email,
            birthday,
            gender,
            categories,
            id,
            attitude,
            rating
        } = this.props.data;
        const userId = this.props.data.id;
        const categories_list = this.renderCategories(categories);
        const render_prop = (propName, value) => (
            <div className='row mb-3 font-weight-bold'>
                <div className='col-4'>{propName + ':'}</div>
                <div className='col-8'>
                    {value ? value : ''}
                </div>
            </div>
        )

        return <>
            <div className="info">
                <AuthComponent>
                    <div className="col-4 user">
                        <div className='d-flex flex-column justify-content-center align-items-center'>
                            <div className="user-profile-avatar">
                                <CustomAvatar size="big"
                                              name={firstName}
                                              userId={id}/>
                            </div>
                            <RatingAverage value={rating} direction='row'/>

                            <div className="row justify-content-center">
                                <Tooltip title="Like this user" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton
                                        className={attitude == '0' ? 'text-success' : ''}
                                        onClick={attitude != '0' ? this.props.onLike : this.props.onReset}
                                    >
                                        <i className="fas fa-thumbs-up" />
                                    </IconButton>
                                </Tooltip>
                                <Tooltip title="Dislike this user" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton
                                        className={attitude == '1' ? 'text-danger' : ''}
                                        onClick={attitude != '1' ? this.props.onDislike : this.props.onReset}
                                    >
                                        <i className="fas fa-thumbs-down" />
                                    </IconButton>
                                </Tooltip>
                                <Tooltip title="Start chat!" placement="bottom" TransitionComponent={Zoom}>
                                    <Link to={`/chat/${id}`}>
                                        <IconButton>
                                            <i className="far fa-comments" />
                                        </IconButton>
                                    </Link>
                                </Tooltip>
                            </div>
                        </div>
                    </div>
                </AuthComponent>
                <div className='col-sm-12  col-md-6'>
                    {render_prop('First Name', firstName)}
                    {render_prop('Last Name', lastName)}
                    {render_prop('Age', getAge(birthday))}
                    {render_prop('Gender', genders[gender])}
                    {render_prop('Email', email)}
                    {render_prop('Interests', categories_list)}
                </div>
            </div>
            <div className='mt-2'>
                <AppBar position="static" color="inherit">
                    <Tabs
                        className='w-100'
                        value={this.state.value}
                        onChange={this.handleChange}
                        variant="fullWidth"
                        scrollButtons="on"
                        indicatorColor="primary"
                        textColor="primary" >
                        <Tab
                            label="Future events"
                            icon={
                                <IconButton
                                    color={this.state.value === 0 ? '' : 'primary'}>
                                    <i className="far fa-calendar-alt" />
                                </IconButton>}
                            component={Link}
                            to={`/user/${userId}/FutureEvents`} />
                        <Tab
                            label="Archive events"
                            icon={
                                <IconButton
                                    color={this.state.value === 1 ? '' : 'primary'}>
                                    <i className="fas fa-archive" />
                                </IconButton>}
                            component={Link}
                            to={`/user/${userId}/ArchiveEvents`} />
                        <Tab
                            label="Visited events"
                            icon={
                                <IconButton
                                    color={this.state.value === 2 ? '' : 'primary'}>
                                    <i className="fas fa-history" />
                                </IconButton>}
                            component={Link}
                            to={`/user/${userId}/VisitedEvents`} />
                        <Tab
                            label="Events to go"
                            icon={
                                <IconButton
                                    color={this.state.value === 3 ? '' : 'primary'}>
                                    <i className="fas fa-map-marker-alt" />
                                </IconButton>}
                            component={Link}
                            to={`/user/${userId}/EventsToGo`} />
                    </Tabs>
                </AppBar>
                
                <Switch>
                    <Route
                        exact
                        path='/user/:id'
                        render={() =>
                            <Redirect to={`/user/${userId}/FutureEvents`} />} />
                                            

                    <Route
                        path='/user/:id/FutureEvents'
                        render={() =>
                            <Events
                                events={this.props.events}
                                current_user={this.props.current_user}
                                typeOfEvents={this.props.onFuture} />}/>
                        
                                     
                    <Route path='/user/:id/ArchiveEvents'
                        render={() => <Events
                            events={this.props.events}
                            current_user={this.props.current_user}
                                typeOfEvents={this.props.onPast} />} />
                    
                    
                    <Route
                        path='/user/:id/VisitedEvents'
                        render={() => <Events
                            events={this.props.events}
                            current_user={this.props.current_user}
                            typeOfEvents={this.props.onVisited} />} />
                    
                    <Route
                        path='/user/:id/EventsToGo'
                        render={() => <Events
                            events={this.props.events}
                            current_user={this.props.current_user}
                            typeOfEvents={this.props.onToGo} />} />
                </Switch>
            </div>
        </>
    }
}

export default withRouter(UserItemView);
