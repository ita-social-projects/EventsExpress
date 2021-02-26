import React, { Component } from 'react';
import {
    BrowserRouter,
    Route,
    Redirect,
    Switch
} from 'react-router-dom';
import Home from '../home';
import Profile from '../profile';
import Category from '../category/categories';
import UsersWrapper from '../../containers/users';
import UserPWrapper from '../../containers/UsersWrapper';
import UserItemViewWrapper from '../../containers/user-item-view';
import EventItemViewWrapper from '../../containers/event-item-view';
import EventScheduleViewWrapper from '../../containers/event-Schedule-item-view';
import EventSchedulesListWrapper from '../../containers/eventSchedules-list';
import Layout from '../layout';
import SearchUserWrapper from '../../containers/UserSearchWrapper';
import NotFound from '../Route guard/404';
import EventsForAdmin from '../../components/event/EventsForAdmin';
import Authentication from '../Authentication/authentication';
import Chat from '../chat';
import UserChats from '../chat/user_chats';
import NotificationEvents from '../notification_events';
import ContactUsWrapper from '../../containers/contactUs';
import LoginTwitter from '../../containers/TwitterLogin';
import UnitOfMeasuring from '../unitOfMeasuring/unitsOfMeasuring';
import AddEventWrapper from '../../containers/add-event';
import EventItemViewWrapperNew from '../../containers/event-item-view-new';
import Draft from '../Draft/Draft';
export default class App extends Component {
    render() {
        return (
            <BrowserRouter>
                <Layout>
                    <Switch>
                        <Route path="/home/events" component={Home} />
                        <Route
                            exact
                            path="/"
                            render={() => (
                                <Redirect to="/home/events" />
                            )}
                        />
                         <Route
                            exact
                            path="/home"
                            render={() => (
                                <Redirect to="/home/events" />
                            )}
                        />
                        <Route path="/profile/" component={Profile} />
                        <Route path="/event/:id/:page" component={EventItemViewWrapper} />
                        <Route path="/eventSchedules" component={EventSchedulesListWrapper} />
                        <Route path="/eventSchedule/:id" component={EventScheduleViewWrapper} />
                        <Route path="/user/:id" component={UserItemViewWrapper} />
                        <Route path="/admin/categories/" component={Category} />
                        <Route path="/admin/users" component={UserPWrapper} />
                        <Route path="/search/users" component={SearchUserWrapper} />
                        <Route path="/admin/users" component={UsersWrapper} />
                        <Route path="/user_chats" component={UserChats} />
                        <Route path="/notification_events" component={NotificationEvents} />
                        <Route path="/authentication/:id/:token" component={Authentication} />
                        <Route path="/Authentication/TwitterLogin" component={LoginTwitter} />
                        <Route path="/chat/:chatId" component={Chat} />
                        <Route path="/contactUs" component={ContactUsWrapper} />
                        <Route path='/admin/unitsOfMeasuring' component={UnitOfMeasuring} />
                        <Route path='/event/createEvent' component={AddEventWrapper} />
                        <Route path='/editEvent/:id' component={EventItemViewWrapperNew} />
                        <Route path='/editEvent/' component={Draft} />
                        <Route component={NotFound} />
                    </Switch>
                </Layout>
            </BrowserRouter>
        );
    }
}
