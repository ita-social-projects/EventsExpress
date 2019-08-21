export const _alert={
    SET:"SET_ALERT",
    RESET:"RESET_ALERT",
    SETOPEN:"ALERT_SET_OPEN"

}

export  function SetAlert(data){

    return dispatch => { 
        dispatch(setAlert(data));
        dispatch(setAlertOpen(true));
    }
}

export  function ResetAlert(data){

    return dispatch => { 
        dispatch(resetAlert())
        dispatch(setAlertOpen(false));
    }
}


function setAlert(data){
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
export function setAlertOpen(){
    return{
        type:_alert.SETOPEN
    }
}

