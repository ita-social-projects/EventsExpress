import React from 'react';
import ReactDOM from 'react-dom';

import { Provider } from 'react-redux';
import App from './components/app';
import configureStore from './store/configureStore';
import { createBrowserHistory } from 'history';

import { ConnectedRouter } from 'react-router-redux';

const initialState = window.initialReduxState;
// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const store = configureStore(history, initialState);

const rootElement = document.getElementById('root');

console.log(store.getState());

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  rootElement);