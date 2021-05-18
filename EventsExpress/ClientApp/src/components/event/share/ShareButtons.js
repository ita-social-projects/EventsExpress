import React, { Component } from 'react';
import { FacebookProvider, ShareButton } from 'react-facebook';
import { Telegram, Twitter, Linkedin } from 'react-social-sharing';
import { connect } from 'react-redux';
import './share.css';

export class ShareButtons extends Component {
     
    render() {
        return (
            <>
                <FacebookProvider appId={this.props.config.keys.facebookClientId}>
                    <ShareButton className="btn" href={this.props.href} >
                        <div id="fb-share-button">
                            <i className="fab fa-facebook text-white"></i>
                        </div>
                    </ShareButton>
                </FacebookProvider>

                <Telegram solid small link={this.props.href} />
                <Twitter solid small link={this.props.href} />
                <Linkedin solid small link={this.props.href} />
            </>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        config: state.config
    }
};
const mapDispatchToProps = dispatch => ({
    facebookLoginAdd: email => dispatch(facebookLoginAdd(email)),
    setErrorAlert: msg => dispatch(setErrorAlert(msg)),
});
export default connect(mapStateToProps, mapDispatchToProps)(ShareButtons);
