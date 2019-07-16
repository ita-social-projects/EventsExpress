import React, { Component } from 'react';
import Home from '../home';
import Profile from '../profile';
import { BrowserRouter as Router, Route} from 'react-router-dom';
import Layout from '../layout';

export default class App extends Component {
    
    render(){
        return (
            <Router>
                <Layout>
                        <Route exact path="/" component={Home} />
                        <Route path="/profile/" component={Profile} />
                </Layout>
            </Router>
        );
    }
}