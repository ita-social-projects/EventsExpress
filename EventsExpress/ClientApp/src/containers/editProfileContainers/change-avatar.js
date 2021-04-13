import React, { Component } from "react";
import { connect } from "react-redux";
import ChangeAvatar from '../../components/profile/editProfile/change-avatar';
import change_avatar from '../../actions/redactProfile/avatar-change-action';

class ChangeAvatarWrapper extends Component {
    submit = values => {
        return this.props.change_avatar(values);
    };

    render() {
        return <ChangeAvatar
            initialValues={{ image: this.props.current_photo }}
            onSubmit={this.submit} />;
    }
}

const mapStateToProps = (state) => ({
    current_photo: state.user.photoUrl
})

const mapDispatchToProps = dispatch => {
    return {
        change_avatar: (data) => dispatch(change_avatar(data))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ChangeAvatarWrapper);