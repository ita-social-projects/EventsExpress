import React, { Component } from "react";
import Avatar from "@material-ui/core/Avatar";
import { userDefaultImage } from "../../constants/userDefaultImage";
import { connect } from "react-redux";
import PhotoService from "../../services/PhotoService";

const photoService = new PhotoService();

export class CustomAvatar extends Component {
  constructor(props) {
    super(props);

    this.state = {
      avatarImage: null,
    };
  }

  uploadPhoto() {
    photoService.getUserPhoto(this.props.userId).then((avatarImage) => {
      if (avatarImage != null) {
        this.setState({ avatarImage: URL.createObjectURL(avatarImage) });
      } else {
        this.setState({ avatarImage: null });
      }
    });
  }

  componentDidMount() {
    this.uploadPhoto();
  }
  componentDidUpdate(prevProps) {
    if (this.props.changeAvatarCounter !== prevProps.changeAvatarCounter)
      this.uploadPhoto();
  }

  componentWillUnmount() {
    URL.revokeObjectURL(this.state.avatarImage);
  }

  render() {
    const { name, height, width, variant } = this.props;

    return (
      <>
        <Avatar
          alt={name + "avatar"}
          src={this.state.avatarImage}
          variant={variant}
          style={{ height: height, width: width }}
          imgProps={{
            onError: (e) => {
              e.target.onerror = null;
              e.target.src = `${userDefaultImage}`;
            },
          }}
        />
      </>
    );
  }
}
const mapStateToProps = (state) => {
  return {
    changeAvatarCounter: state.change_avatar.Update,
  };
};

export default connect(mapStateToProps, null)(CustomAvatar);
