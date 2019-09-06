import React, { Component } from 'react';
import { FacebookProvider, ShareButton } from 'react-facebook';
import './share.css'
import { Telegram, Twitter, Linkedin } from 'react-social-sharing'

export default class Example extends Component {

    render() {
      
        return (<>
            <FacebookProvider appId="418450132121252">
                <ShareButton className="btn" href={this.props.href} >    
                    <div id="fb-share-button">
                        <svg viewBox="0 0 12 12" preserveAspectRatio="xMidYMid meet"><path className="svg-icon-path" d="M9.1,0.1V2H8C7.6,2,7.3,2.1,7.1,2.3C7,2.4,6.9,2.7,6.9,3v1.4H9L8.8,6.5H6.9V12H4.7V6.5H2.9V4.4h1.8V2.8 c0-0.9,0.3-1.6,0.7-2.1C6,0.2,6.6,0,7.5,0C8.2,0,8.7,0,9.1,0.1z"></path>
                        </svg>
                    </div>
                    </ShareButton>   
            </FacebookProvider>

            <Telegram solid small link={this.props.href} />
            <Twitter solid small link={this.props.href} />
            <Linkedin solid small  link={this.props.href}  />
            </>
        );
    }
}