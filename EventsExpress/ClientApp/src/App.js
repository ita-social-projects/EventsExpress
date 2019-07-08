import React from 'react';
import { Route } from 'react-router';
import Layout from './components/shared/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import LoginForm from './components/account/LoginForm';
import Registration from './components/account/Registration';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/counter' component={Counter} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
        <Route path='/account/login' component={LoginForm} />
        <Route path='/account/register' component={Registration} />
  </Layout>
);
