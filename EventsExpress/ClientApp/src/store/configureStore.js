import React from 'react';

import { combineReducers } from 'redux';
import { createStore, compose, applyMiddleware } from 'redux';

import { routerReducer, routerMiddleware } from 'react-router-redux';
import thunk from 'redux-thunk';
import rootReducers from '../reducers/root'; 


function configureStore(history, initialState){

    const middleware = [
        thunk,
        routerMiddleware(history)
      ];

    const enhancers = [];
    const isDevelopment = process.env.NODE_ENV === 'development';
      if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
        enhancers.push(window.devToolsExtension());
      }


    return createStore(
        rootReducers,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
      );
}

export default configureStore;