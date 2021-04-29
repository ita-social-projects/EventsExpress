import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';
import { userDefaultImage } from "../../constants/userDefaultImage";
export default class CustomAvatar extends Component {


    render() {

        const { userId, name } = this.props;

        let size = `${this.props.size}Avatar`;

        return (
            <>
                <Avatar
                    alt={name + "avatar"}
                    src={`api/photo/GetUserPhoto?id=${userId}`}
                    className={size}
                    imgProps={{ onError: (e) => { e.target.onerror = null; e.target.src = `${userDefaultImage}` } }}/>
            </>
        );
    }
}