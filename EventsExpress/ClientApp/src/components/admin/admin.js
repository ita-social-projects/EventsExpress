import React, { Component } from 'react';
import { Redirect, Route, Switch } from "react-router-dom";
import './admin.css';
import { NavItem } from '../NavItem/NavItem';
import Category from '../category/categories';
import UsersWrapper from '../../containers/users';
import UnitOfMeasuring from '../unitOfMeasuring/unitsOfMeasuring';
import NotificationTemplateWrapper from "../../containers/notification-template/notification-template";
import NotificationInfoWrapper from "../../containers/notification-template/notification-info";
import Track from '../tracks/track';
export default class Admin extends Component {
    render() {
        return (
            <>
                <div className="admin-panel row">
                    <h3>Admin Panel</h3>
                </div>
                <div className="row h-100">
                    <div className="admin-sidebar col-sm-2">
                        <ul className="list-unstyled">
                            <nav id="sub-nav">
                                <div>
                                    <NavItem
                                        to={'/admin/categories/'}
                                        icon={'fa fa-hashtag'}
                                        text={"Categories"}
                                    />
                                </div>
                                <div>
                                    <NavItem
                                        to={'/admin/unitsOfMeasuring/'}
                                        icon={'fa fa-plus'}
                                        text={"UnitsOfMeasuring"}
                                    />
                                </div>
                                <div>
                                    <NavItem
                                        to={'/admin/users?page=1'}
                                        icon={'fa fa-users'}
                                        text={"Users"}
                                    />
                                </div>
                                <div>
                                    <NavItem
                                        to={'/admin/notificationTemplates'}
                                        icon={'fas fa-comment-alt'}
                                        text={"Notification Templates"}
                                    />
                                </div>
                                <div>
                                    <NavItem
                                        to={'/admin/tracks/'}
                                        icon={'fa fa-server'}
                                        text={"Tracks"}
                                    />
                                </div>
                            </nav>
                        </ul>
                    </div>
                    <div className="col-sm-10">
                        <Switch>
                            <Route
                                exact
                                path='/admin'
                                render={() =>
                                    <Redirect to={`/admin/categories`} />} />
                            <Route path="/admin/categories/" component={Category} />
                            <Route path='/admin/unitsOfMeasuring' component={UnitOfMeasuring} />
                            <Route path="/admin/users" component={UsersWrapper} />
                            <Route path='/admin/notificationTemplates' component={NotificationTemplateWrapper} />
                            <Route path='/admin/notificationTemplate/:id' component={NotificationInfoWrapper} />
                        </Switch>
                        <Route path="/admin/tracks" component={Track} />
                    </div>
                </div>
            </>
        );
    }
}