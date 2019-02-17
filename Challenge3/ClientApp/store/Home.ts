import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface HomeState {
    isLoading: boolean;
    serverTime: string;
}

interface RequestServerTimeAction {
    type: 'REQUEST_SERVER_TIME';
}

interface ReceiveServerTimeAction {
    type: 'RECEIVE_SERVER_TIME';
    serverTime: string;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestServerTimeAction | ReceiveServerTimeAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestServerTime: (): AppThunkAction<KnownAction> => (dispatch, getState) =>
    {
        let fetchTask = fetch(`api/ServerTime/LongTimeString`)
            .then(response =>
            {
                return response.text() as Promise<string>;;
            })
            .then(data =>
            {
                dispatch({ type: 'RECEIVE_SERVER_TIME', serverTime: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_SERVER_TIME' });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: HomeState = { serverTime: "Loading...", isLoading: false };

export const reducer: Reducer<HomeState> = (state: HomeState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_SERVER_TIME':
            return {
                serverTime: state.serverTime,
                isLoading: true
            };
        case 'RECEIVE_SERVER_TIME':
                return {
                    serverTime: action.serverTime,
                    isLoading: false
                };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
