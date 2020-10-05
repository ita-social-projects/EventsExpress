'use strict';

import React, { Component } from 'react';
import TwitterLogin from 'react-twitter-login';
import { connect } from 'react-redux';
import config from '../config';
import { loginTwitter } from '../actions/login';
import './css/Auth.css';
import { render } from 'react-dom';
import oauth from 'oauth-sign';
import uuid from 'uuid';

class LoginTwitter extends Component {
    render() {
        const authHandler = async (err, data) => {
            const authData = {
                email: undefined,
                image_url: undefined,
                name: undefined,
                oauth_token: undefined,
                oauth_token_secret: undefined,
                screen_name: undefined,
                user_id: undefined,
            };

            if (!err) {
                ({
                    oauth_token: authData.oauth_token,
                    oauth_token_secret: authData.oauth_token_secret,
                    screen_name: authData.screen_name,
                    user_id: authData.user_id
                } = data);

                const accountCred = await obtainAccountCredentials(
                    authData.oauth_token, authData.oauth_token_secret
                );

                if (typeof accountCred === 'undefined'
                    || typeof accountCred?.email === 'undefined') {
                    this.props.login.loginError = " Please add email to your twitter account!";
                } else {
                    ({
                        email: authData.email,
                        profile_image_url_https: authData.image_url,
                        name: authData.name
                    } = accountCred);
                }

                authData.image_url = authData.image_url.replace(/_normal/i, '');
            }

            this.props.loginTwitter(authData);
        };

        return (
            <div>
                <TwitterLogin
                    authCallback={authHandler}
                    buttonTheme="light_short"
                    consumerKey={config.TWITTER_CONSUMER_KEY}
                    consumerSecret={config.TWITTER_CONSUMER_SECRET}
                    callbackUrl={`${window.location.origin}${config.TWITTER_CALLBACK_URL}`}
                />
            </div>
        );
    }
};

const mapStateToProps = (state) => {
    return {
        login: state.login
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        loginTwitter: (authData) => dispatch(loginTwitter(authData))
    }
};

const obtainAccountCredentials = async (oauthToken, oauthTokenSecret) => {
    const urlVerifyCredentials = 'https://api.twitter.com/1.1/account/verify_credentials.json';

    const apiTwitterAuthParams = {
        include_email: true,
        skip_status: true,
        oauth_token: oauthToken,
        oauth_consumer_key: config.TWITTER_CONSUMER_KEY,
        oauth_nonce: uuid.v4().replace(/-/g, ''),
        oauth_signature_method: 'HMAC-SHA1',
        oauth_timestamp: (Date.now() / 1000).toFixed(),
        oauth_version: '1.0'
    };

    apiTwitterAuthParams.oauth_signature = oauth.hmacsign(
        'GET',
        urlVerifyCredentials,
        apiTwitterAuthParams,
        config.TWITTER_CONSUMER_SECRET,
        oauthTokenSecret
    );

    const res = await fetch(`https://cors-anywhere.herokuapp.com/${urlVerifyCredentials}?include_email=true&skip_status=true`, {
        method: 'GET',
        headers: {
            'Authorization': getOAuthAuthorizationHeader(apiTwitterAuthParams),
        }
    });

    return res.ok ? await res.json() : undefined;
};

const getOAuthAuthorizationHeader = params => {
    return `OAuth ${Object.keys(params).sort().map(function (key) {
        return `${key}="${oauth.rfc3986(params[key])}"`;
    }).join(', ')}`;
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginTwitter);
