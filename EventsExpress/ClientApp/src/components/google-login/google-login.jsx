import React, { Component } from "react";
import { GoogleLogin } from "react-google-login";

export default class Login extends Component {

    render() {
        const { googleClientId, googleResponseHandler } = this.props;

        return (
            <div>
                <GoogleLogin
                    clientId={googleClientId}
                    render={renderProps => (
                        <button className="btnGoogle" onClick={renderProps.onClick} disabled={renderProps.disabled}>
                            <i className="fab fa-google blue fa-lg" />
                            <span>Log in</span>
                        </button>
                    )}
                    onSuccess={googleResponseHandler}
                    version="3.2"
                />
            </div>
        );
    }
}
