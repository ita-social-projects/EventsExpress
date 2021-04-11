import React from 'react';
import { connect } from 'react-redux';
import { submit } from 'redux-form';
import Button from '@material-ui/core/Button';

const style = {
    color: 'white',
    background: 'blue'
};
 function submitForms(dispatch) {
     dispatch(submit("Part1"));
     dispatch(submit("Part3"));
     dispatch(submit("Part5"));
}
const RemoteSubmitButton = ({ dispatch, callBack }) => (
    <Button
        variant="contained"
        color="primary"
        style={style}
        onClick={async () => { await  submitForms(dispatch); callBack();  }}
    >
        Next
    </Button>
);

export default connect()(RemoteSubmitButton);
