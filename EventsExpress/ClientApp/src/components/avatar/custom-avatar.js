import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';

export default class CustomAvatar extends Component {


    render() {

        const  photoUrl  = this.props.photoUrl;
        const name  = this.props.name;
        var size = `${this.props.size}Avatar`;
        return (
            <>
                {photoUrl
                    ? <Avatar
                        src={photoUrl}
                        className={size}
                    />
                    : <Avatar className={size}>
                        <h1 className="display-1 text-light">
                            {name.charAt(0).toUpperCase()}
                        </h1>
                    </Avatar>}
            </>
        );
    }
}