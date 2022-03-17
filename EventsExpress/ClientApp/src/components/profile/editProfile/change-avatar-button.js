import React from 'react';
import MyVerticallyCenteredModal from './change-avatar-modalWindow';
import Button from '@material-ui/core/Button';

export default function ChangeAvatarButton() {
    const [modalShow, setModalShow] = React.useState(false);
  
    return (
      <>
        <Button variant="primary" onClick={() => setModalShow(true)}>
          Update Avatar
        </Button>
  
        <MyVerticallyCenteredModal
          show={modalShow}
          onHide={() => setModalShow(false)}
        />
      </>
    );
  }
  