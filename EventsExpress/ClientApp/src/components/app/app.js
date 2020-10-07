import React, { Component } from 'react';
import Home from '../home';
import Profile from '../profile';
import Category from '../category/categories';
import Comment from '../comment/comment'
import Admin from '../admin';
import UsersWrapper from '../../containers/users';
import UserPWrapper from '../../containers/UsersWrapper';
import EventListWrapper from '../../containers/event-list';
import UserItemViewWrapper from '../../containers/user-item-view';
import EventItemViewWrapper from '../../containers/event-item-view';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import Layout from '../layout';
import SearchUserWrapper from '../../containers/UserSearchWrapper';
import NotFound from '../Route guard/404'
import BadRequest from '../Route guard/400'
import { Switch } from 'react-router-dom';
import { Redirect } from 'react-router';
import EventsForAdmin from '../../components/event/EventsForAdmin'
import Authentication from '../Authentication/authentication';
import Chat from '../chat';
import UserChats from '../chat/user_chats';
import NotificationEvents from '../notification_events';
import ContactUsWrapper from '../../containers/contactUs';
import LoginTwitter from '../../containers/TwitterLogin';

export default class App extends Component {
    render() {
        return (
            <Layout>
                <Switch>
                    <Route path="/home/events" component={Home} />
                    <Route
                        exact
                        path="/"
                        render={() => (
                            <Redirect to="/home/events?page=1" />
                        )}
                    />
                    <Route path="/profile/" component={Profile} />
                    <Route path="/event/:id/:page" component={EventItemViewWrapper} />
                    <Route path="/user/:id" component={UserItemViewWrapper} />
                    <Route path="/admin/categories/" component={Category} />
                    <Route path="/admin/users" component={UserPWrapper} />
                    <Route path="/admin/events" component={EventsForAdmin} />
                    <Route path="/search/users" component={SearchUserWrapper} />
                    <Route path="/admin/users" component={UsersWrapper} />
                    <Route path="/user_chats" component={UserChats} />
                    <Route path="/notification_events" component={NotificationEvents} />
                    <Route path="/authentication/:id/:token" component={Authentication} />
                    <Route path="/Authentication/TwitterLogin" component={LoginTwitter} />
                    <Route path="/chat/:chatId" component={Chat} />
                    <Route path="/contactUs" component={ContactUsWrapper} />
                    <Route component={NotFound} />
                </Switch>
            </Layout>
        );
    }
}
