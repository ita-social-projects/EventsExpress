import React, { Component } from 'react';
import Home from '../home';
import Profile from '../profile';
import Category from '../category/categories';
import Comment from '../comment/comment'
import Admin from '../admin';
import UsersWrapper from '../../containers/users';
import UserPWrapper from '../../containers/UsersWrapper';
import EventItemViewWrapper from '../../containers/event-item-view';
import { BrowserRouter as Router, Route} from 'react-router-dom';
import Layout from '../layout';
import SearchUserWrapper from '../../containers/UserSearchWrapper';

export default class App extends Component {

    render(){
        return (
                <Layout>
                        <Route path="/home/events" component={Home} />
                        <Route path="/profile/" component={Profile} />
                        <Route path="/admin/" component={Admin} />
                        <Route path="/event/:id/:page" component={EventItemViewWrapper} />
                        <Route path="/admin/categories/" component={Category} />
                        <Route path="/admin/users" component={UserPWrapper} />
                        <Route path="/admin/events" component={Home} />
                        <Route path="/search/users" component={SearchUserWrapper} />
                </Layout>
        );
    }
}