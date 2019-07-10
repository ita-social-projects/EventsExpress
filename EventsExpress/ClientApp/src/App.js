import React from 'react';
import { Route } from 'react-router';
import Layout from './components/shared/Layout';
import Home from './components/Home';

import LoginForm from './components/account/LoginForm';
import Registration from './components/account/Registration';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/account/login' component={LoginForm} />
    <Route path='/account/register' component={Registration} />
  </Layout>
);
