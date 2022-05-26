import React, { Component } from "react";
import { connect } from "react-redux";
import ChangeAvatar from "../../components/profile/editProfile/change-avatar";
import { change_avatar } from "../../actions/redactProfile/avatar-change-action";
import AuthComponent from "../../security/authComponent";

class ChangeAvatarWrapper extends Component {
  submit = (values) => {
    return this.props.change_avatar(values).then(() => {
      this.props.onHide();
    });
  };

  render() {
    return (
      <AuthComponent>
        <ChangeAvatar
          initialValues={{ userId: this.props.userId }}
          onSubmit={this.submit}
        />
      </AuthComponent>
    );
  }
}

const mapStateToProps = (state) => ({
  userId: state.user.id,
});

const mapDispatchToProps = (dispatch) => {
  return {
    change_avatar: (data) => dispatch(change_avatar(data)),
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ChangeAvatarWrapper);
