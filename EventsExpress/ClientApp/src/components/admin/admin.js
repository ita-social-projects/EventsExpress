import React, { Component } from 'react';
import { Route } from "react-router-dom";
import './admin.css';
import { NavItem } from '../NavItem/NavItem';
import Category from '../category/categories';
import UserPWrapper from '../../containers/UsersWrapper';
import UnitOfMeasuring from '../unitOfMeasuring/unitsOfMeasuring';

export default class Admin extends Component {

    render() {

        return (
            <>
                <div className="wrapper-content">
                    <div className="extra-sidebar">
                        <ul className="list-unstyled">
                            <nav id="sub-nav">
                                <div className="item">
                                    <NavItem
                                        to={'/admin/categories/'}
                                        icon={'fa fa-hashtag'}
                                        text={"Categories"}
                                    />
                                </div>
                                <div className="item">
                                    <NavItem
                                        to={'/admin/unitsOfMeasuring/'}
                                        icon={'fa fa-plus'}
                                        text={"UnitsOfMeasuring"}
                                    />
                                </div>
                                <div className="item">
                                    <NavItem
                                        to={'/admin/users?page=1'}
                                        icon={'fa fa-users'}
                                        text={"Users"}
                                    />
                                </div>
                            </nav>
                        </ul>
                    </div>
                    <main>
                        <Route path="/admin/categories/" component={Category} />
                        <Route path='/admin/unitsOfMeasuring' component={UnitOfMeasuring} />
                        <Route path="/admin/users" component={UserPWrapper} />
                    </main>
                </div>
            </>
        );
    }
}
