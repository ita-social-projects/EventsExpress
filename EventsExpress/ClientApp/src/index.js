import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import configureStore from './store/configureStore';
import App from './components/app';
import registerServiceWorker from './registerServiceWorker';
import { setLoginPending, setLoginSuccess, setUser } from './actions/login/login-action';
import { initialConnection } from './actions/chat/chat-action';
import { getUnreadMessages } from './actions/chat/chats-action';
import { updateEventsFilters } from './actions/event/event-list-action';
import eventHelper from '../src/components/helpers/eventHelper';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

const rootElement = document.getElementById('root');

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App/>
        </ConnectedRouter>
    </Provider>,
    rootElement);

registerServiceWorker();
