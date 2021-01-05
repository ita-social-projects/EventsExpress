export const _dialog={
    SET:"SET_DIALOG",
    RESET:"RESET_DIALOG",
    SETOPEN:"DIALOG_SET_OPEN"
}

export function set_dialog(data){
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

export function setDialogOpen(data){
    return{
        type:_dialog.SETOPEN,
        payload: data
    }
}