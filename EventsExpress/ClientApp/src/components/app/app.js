import React, { Component } from 'react';
import {
    BrowserRouter,
    Route,
    Redirect,
    Switch
} from 'react-router-dom';
import './app.css'

import LandingWrapper from '../../containers/landing-wrapper';
import Home from '../home';
import Issues from '../contactAdmin/issues';
import Profile from '../profile';
import UserItemViewWrapper from '../../containers/user-item-view';
import EventItemViewWrapper from '../../containers/event-item-view';
import EventScheduleViewWrapper from '../../containers/event-Schedule-item-view';
import EventSchedulesListWrapper from '../../containers/eventSchedules-list';
import Layout from '../layout';
import SearchUserWrapper from '../../containers/UserSearchWrapper';
import NotFound from '../Route guard/404';
import Authentication from '../Authentication/authentication';
import Chat from '../chat';
import UserChats from '../chat/user_chats';
import NotificationEvents from '../notification_events';
import ContactAdminWrapper from '../../containers/contactAdmin/contactAdmin-container';
import ContactAdminDetails from '../../containers/contactAdmin/contactAdmin-details-container';
import Admin from '../admin';
import RegisterSuccess from '../register/register-success';
import EventEditWrapper from '../../containers/event-edit-wrapper';
import EventDraftListWrapper from '../../containers/event-draft-list';
import Unauthorized from '../Route guard/401';
import Forbidden from '../Route guard/403';
import withAuthRedirect from '../../security/withAuthRedirect';
import { connect } from 'react-redux';
import AuthUser from '../../actions/login/auth-user-action';
import getConfig from '../../actions/config/get-config-action';
import RegistrationForm from '../registration-form';
import MainLayout from '../MainLayout';
import privacy from '../privacy/privacy.js';
import About from "../about/about.js";
import Terms from '../terms/terms.js';
import Scroller from "../scroller"


class App extends Component {
    constructor(props) {
        super(props);
        this.props.authUser();
        this.props.getConfig();
    }

    UserRoleSecurity = withAuthRedirect(['User']);
    AdminRoleSecurity = withAuthRedirect(['Admin']);
    AdminAndUserRoleSecurity = withAuthRedirect(['Admin', 'User']);

    render() {
        return (
            <BrowserRouter>
            <Scroller>
            <MainLayout>
                <Switch>
                    <Route path="/landing" component={LandingWrapper} />
                    <Route
                        exact
                        path="/"
                        render={() => (
                            <Redirect to="/landing" />
                        )}
                    />
                    <Route path='/register/complete' component={this.AdminAndUserRoleSecurity(RegistrationForm)} />
                    <Route>
                        <Layout>
                            <Switch>
                                <Route path="/home/events" component={Home} />
                                <Route
                                    exact
                                    path="/home"
                                    render={() => (
                                        <Redirect to="/home/events" />
                                    )}
                                />
                                <Route path="/editProfile/" component={this.AdminAndUserRoleSecurity(Profile)} />
                                <Route path="/event/:id/:page" component={EventItemViewWrapper} />
                                <Route path="/eventSchedules" component={EventSchedulesListWrapper} />
                                <Route path="/eventSchedule/:id" component={EventScheduleViewWrapper} />
                                <Route path="/user/:id" component={this.AdminAndUserRoleSecurity(UserItemViewWrapper)} />
                                <Route path="/admin" component={this.AdminRoleSecurity(Admin)} />
                                <Route path="/search/users" component={this.UserRoleSecurity(SearchUserWrapper)} />
                                <Route path="/user_chats" component={this.AdminAndUserRoleSecurity(UserChats)} />
                                <Route path="/contactAdmin/issues" component={Issues} />
                                <Route path="/contactAdmin/:id/UpdateStatus" component={this.AdminRoleSecurity(ContactAdminDetails)} />
                                <Route path="/notification_events" component={this.AdminAndUserRoleSecurity(NotificationEvents)} />
                                <Route path="/authentication/:id/:token" component={Authentication} />
                                <Route path="/chat/:chatId" component={this.AdminAndUserRoleSecurity(Chat)} />
                                <Route path="/contactAdmin" component={ContactAdminWrapper} />
                                <Route path='/registerSuccess' component={RegisterSuccess} />
                                <Route path='/editEvent/:id' component={this.UserRoleSecurity(EventEditWrapper)} />
                                <Route path='/drafts' component={this.UserRoleSecurity(EventDraftListWrapper)} />
                                <Route path='/about' component={About} />
                                <Route path='/unauthorized' component={Unauthorized} />
                                <Route path='/forbidden' component={Forbidden} />
                                <Route path ='/privacy' component ={privacy}/>
                                <Route path='/terms' component={Terms}/>
                                <Route component={NotFound} />
                            </Switch>
                        </Layout>
                    </Route>
                </Switch>
                </MainLayout>
                </Scroller>
            </BrowserRouter>
        );
    }
}

let mapDispatchToProps = (dispatch) => {
    return {
        authUser: () => dispatch(AuthUser()),
        getConfig: () => dispatch(getConfig())
    }
}

export default connect(null, mapDispatchToProps)(App);
