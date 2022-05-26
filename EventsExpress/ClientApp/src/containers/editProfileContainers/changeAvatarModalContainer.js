import React, { Fragment } from "react";
import { connect } from "react-redux";
import {
  ChangeAvatarModal,
  DeleteAvatarModal,
  EditAvatarPreviewModal,
} from "../../components/profile/editProfile/editAvatarModals";
import { delete_avatar } from "../../actions/redactProfile/avatar-change-action";

const EditAvatarModalContainer = (props) => {
  const { show, onHide, onOpen, userId, userName, deleteAvatar } = props;

  const [showChangeAvatarModal, setShowChangeAvatarModal] =
    React.useState(false);
  const [showDeleteAvatarModal, setShowDeleteAvatarModal] =
    React.useState(false);

  const handleChangeButtonClick = () => {
    onHide();
    setShowChangeAvatarModal(true);
  };

  const handleChangeAvatarModalClose = () => {
    setShowChangeAvatarModal(false);
  };

  const handleReturnButtonInChangeModalClick = () => {
    handleChangeAvatarModalClose();
    onOpen();
  };

  const handleDeleteButtonClick = () => {
    onHide();
    setShowDeleteAvatarModal(true);
  };

  const handleDeleteAvatarModalClose = () => {
    setShowDeleteAvatarModal(false);
  };

  const handleReturnButtonInDeleteModalClick = () => {
    handleDeleteAvatarModalClose();
    onOpen();
  };

  const handleSubmitDeletePhoto = () => {
    deleteAvatar(userId);
    handleDeleteAvatarModalClose();
  };

  return (
    <Fragment>
      <EditAvatarPreviewModal
        open={show}
        onClose={onHide}
        id={userId}
        name={userName}
        onChangeButtonClick={handleChangeButtonClick}
        onDeleteButtonClick={handleDeleteButtonClick}
      />

      <ChangeAvatarModal
        open={showChangeAvatarModal}
        onClose={handleChangeAvatarModalClose}
        onReturn={handleReturnButtonInChangeModalClick}
      />

      <DeleteAvatarModal
        open={showDeleteAvatarModal}
        onClose={handleDeleteAvatarModalClose}
        onReturn={handleReturnButtonInDeleteModalClick}
        onSubmit={handleSubmitDeletePhoto}
      />
    </Fragment>
  );
};

const mapStateToProps = (state) => ({
  userId: state.user.id,
  userName: state.user.name,
});

const mapDispatchToProps = (dispatch) => {
  return {
    deleteAvatar: (data) => dispatch(delete_avatar(data)),
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EditAvatarModalContainer);
