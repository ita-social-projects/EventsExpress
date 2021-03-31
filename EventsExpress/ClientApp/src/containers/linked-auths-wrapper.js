import React, { Component } from 'react';
import { connect } from 'react-redux';
import LinkedAuths from '../components/profile/editProfile/linked-auths';
import getLinkedAuths from '../actions/EditProfile/linked-auths-action';
import GoogleLoginAdd from './linked-auths/google-login-add-wrapper';
import FacebookLoginAdd from './linked-auths/facebook-login-add-wrapper';
import TwitterLoginAdd from './linked-auths/twitter-login-add-wrapper';

import './css/linked-auths.css';

class LinkedAuthsWrapper extends Component {
    componentDidMount() {
        this.props.loadData();
    }

    render() {
        const { linkedAuths } = this.props.data;
        return <>
            {linkedAuths.map(item => <LinkedAuths  item={item} />)
            }
             <h6><span>Add more:</span></h6>
            <div className="d-flex justify-content-around mb-3">
               <GoogleLoginAdd/> 
               <FacebookLoginAdd/>
               <TwitterLoginAdd/>
            </div>
        </>
    }
}

const mapStateToProps = (state) => ({
    data : state.account,
});

const mapDispatchToProps = (dispatch) => ({
    loadData: () => dispatch(getLinkedAuths())
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LinkedAuthsWrapper);
