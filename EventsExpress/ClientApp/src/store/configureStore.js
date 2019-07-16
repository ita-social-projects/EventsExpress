import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import { reducer as formReducer } from 'redux-form';
import * as Login from '../reducers/login';
import * as User from '../reducers/user';
import * as Register from '../reducers/register'

export default function configureStore(history, initialState) {
    const reducers = {
        user: User.reducer,
        login: Login.reducer,
        register: Register.reducer, 
        form: formReducer
};

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
    }

  const rootReducer = combineReducers({
    ...reducers,
      routing: routerReducer
      
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
