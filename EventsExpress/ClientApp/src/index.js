import 'bootstrap/dist/css/bootstrap.css';
import './index.css';

import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import configureStore from './store/configureStore';
import App from './components/app';
import registerServiceWorker from './registerServiceWorker';
import { setUser } from './actions/login';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

async function AuthUser(token){
    const res = await fetch('http://localhost:64144/api/Authentication/login_token', {
    method: 'post',  
    headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    }),
    });
    if(res.ok){
      console.log(res);
      store.dispatch(setUser(await res.json()));
    }
}

const token = localStorage.getItem('token');
if (token) {
  AuthUser(token);
}

const rootElement = document.getElementById('root');

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  rootElement);

registerServiceWorker();
