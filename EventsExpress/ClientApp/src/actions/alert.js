export const _alert={
    SET:"SET_ALERT",
    RESET:"RESET_ALERT",
    SETOPEN:"ALERT_SET_OPEN"

}

export  function setAlert(data){

    return dispatch => { 
        dispatch(set_alert(data));
        dispatch(setAlertOpen(true));
    }
}

export  function ResetAlert(data){

    return dispatch => { 
        dispatch(resetAlert({}))
        dispatch(setAlertOpen(false));
    }
}


function set_alert(data){
    return{
        type:_alert.SET,
        payload:data
    }
}

function resetAlert(){
    return{
        type:_alert.RESET,
    }
}
export function setAlertOpen(data){
    return{
        type:_alert.SETOPEN,
        payload: data
    }
}

