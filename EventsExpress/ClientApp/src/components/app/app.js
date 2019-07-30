import React, { Component } from 'react';
import Home from '../home';
import Profile from '../profile';
import Category from '../category/category';
import Comment from '../comment/comment'
import Admin from '../admin';
import UsersWrapper from '../../containers/users';
import EventListWrapper from '../../containers/event-list';
import { BrowserRouter as Router, Route} from 'react-router-dom';
import Layout from '../layout';


export default class App extends Component {
    
    render(){
        return (
                <Layout>
                        <Route exact path="/" component={Home} />
                        <Route path="/profile/" component={Profile} />
                        <Route path="/admin/" component={Admin} />
                        <Route path="/admin/categories/" component={Category} />
                        <Route path="/admin/users/" component={UsersWrapper} />
                <Route path="/admin/events/" component={EventListWrapper} />
                <Route path="/comment/" component={Comment} />

                </Layout>
        );
    }
}