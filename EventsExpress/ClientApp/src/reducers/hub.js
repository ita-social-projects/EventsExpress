
import initialState from '../store/initialState';
import {
   INITIAL_CONNECTION, RESET_HUB
} from '../actions/chat/chat';

import { EVENT_WAS_CREATED } from '../actions/event/event-add-action';

export const reducer = (
    state = initialState.hubConnection,
    action
  ) => {
    switch (action.type) {
      case INITIAL_CONNECTION:
        return action.payload
      case EVENT_WAS_CREATED:
        state
        .invoke('EventWasCreated', action.payload)
        .catch(err => console.error(err));
        return state;
      case RESET_HUB:
          return null
       default: 
          return state;
    }
}  