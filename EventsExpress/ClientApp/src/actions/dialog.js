import { _alert } from "./alert";

export const _dialog={
    SET:"SET_DIALOG",
    RESET:"RESET_DIALOG",
    SETOPEN:"DIALOG_SET_OPEN"
}

export function SetDialog(data){
    return dispatch=>{
        dispatch(setDialog(data));
        dispatch(setDialogOpen(true));
    }
}

function setDialog(data){
    return{
        type:_dialog.SET,
        payload:data
    }
}

function resetDialog(){
    return{
        type:_alert.RESET
    }
}

export function setDialogOpen(data){
    return{
        type:_alert.SETOPEN,
        payload: data
    }
}