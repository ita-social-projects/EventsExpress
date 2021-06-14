import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';
import { userDefaultImage } from "../../constants/userDefaultImage";
import { connect } from 'react-redux';
import PhotoService from '../../services/PhotoService';

const photoService = new PhotoService();

export class CustomAvatar extends Component {
    constructor(props) {
        super(props);

        this.state = {
            avatarImage: null
        }
    }

    uploadPhoto() {
        photoService.getUserPhoto(this.props.userId).then(
            avatarImage => {
                if (avatarImage != null) {
                    this.setState({ avatarImage: URL.createObjectURL(avatarImage) });
                }
            }
        );
    }

    componentDidMount() {
        this.uploadPhoto()
    }
    componentDidUpdate(prevProps) {
        if (this.props.changeAvatarCounter !== prevProps.changeAvatarCounter)
            this.uploadPhoto();
    }

    componentWillUnmount() {
        URL.revokeObjectURL(this.state.avatarImage);
    }
    

    render() {

        const { name } = this.props;

        let size = `${this.props.size}Avatar`;
         


        return (
            <>
                <Avatar
                    alt={name + "avatar"}
                    src={this.state.avatarImage}
                    className={size}
                    imgProps={{ onError: (e) => { e.target.onerror = null; e.target.src = `${userDefaultImage}` } }} />
            </>
        );
    }
}
const mapStateToProps = (state) => {
    return {
        changeAvatarCounter: state.change_avatar.Update
    }
};


export default connect(mapStateToProps, null)(CustomAvatar);