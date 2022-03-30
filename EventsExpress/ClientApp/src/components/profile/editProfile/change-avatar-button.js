import React from 'react';
import ChangeAvatarModal from './change-avatar-modalWindow';
import Button from '@material-ui/core/Button';

export default function ChangeAvatarButton(props) {
    const [modalShow, setModalShow] = React.useState(false);
  
    return (
      <>
        <Button variant="contained"  onClick={() => setModalShow(true)}>
          Update Avatar
        </Button>
  
        <ChangeAvatarModal
          show={modalShow}
          onHide={() => setModalShow(false)}
        />
      </>
    );
  }
  