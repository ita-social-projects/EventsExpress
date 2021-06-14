import React, { Component } from 'react';
import TwitterLogin from 'react-twitter-login';
import oauth from 'oauth-sign';
import uuid from 'uuid';
import { connect } from 'react-redux';
import '../css/Auth.css';

export class TwitterLoginBase extends Component {
    authHandler = async (err, data) => {
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

            const accountCred = await this.obtainAccountCredentials(
                authData.oauth_token, authData.oauth_token_secret
            );
            this.doWork(accountCred, authData);
        }
    };

    doWork = (accountCred, authData) => {
        // override when implement
        return;
    }

    obtainAccountCredentials = async (oauthToken, oauthTokenSecret) => {
        const urlVerifyCredentials = 'https://api.twitter.com/1.1/account/verify_credentials.json';

        const apiTwitterAuthParams = {
            include_email: true,
            skip_status: true,
            oauth_token: oauthToken,
            oauth_consumer_key: this.props.config.keys.twitterConsumerKey,
            oauth_nonce: uuid.v4().replace(/-/g, ''),
            oauth_signature_method: 'HMAC-SHA1',
            oauth_timestamp: (Date.now() / 1000).toFixed(),
            oauth_version: '1.0'
        };

        apiTwitterAuthParams.oauth_signature = oauth.hmacsign(
            'GET',
            urlVerifyCredentials,
            apiTwitterAuthParams,
            this.props.config.keys.twitterConsumerSecret,
            oauthTokenSecret
        );

        const res = await fetch(`https://cors-anywhere.herokuapp.com/${urlVerifyCredentials}?include_email=true&skip_status=true`, {
            method: 'GET',
            headers: {
                'Authorization': this.getOAuthAuthorizationHeader(apiTwitterAuthParams),
            }
        });

        return res.ok ? res.json() : undefined;
    };

    getOAuthAuthorizationHeader = params => {
        return `OAuth ${Object.keys(params).sort().map(function (key) {
            return `${key}="${oauth.rfc3986(params[key])}"`;
        }).join(', ')}`;
    }


    render() {
        return (
            <div>
                <TwitterLogin
                    authCallback={this.authHandler}
                    buttonTheme="light_short"
                    consumerKey={this.props.config.keys.twitterConsumerKey}
                    consumerSecret={this.props.config.keys.twitterConsumerSecret}
                    callbackUrl={`${window.location.origin}${this.props.config.keys.twitterCallbackUrl}`}
                />
            </div>
        );
    }


}

const mapStateToProps = (state) => {
    return {
        config: state.config
    }
};

export default connect(mapStateToProps, null)(TwitterLoginBase);
