import React, { Component } from 'react';
import { connect } from 'react-redux';
import { toggleLoginModalState, isOpen } from '../../actions/login-modal';
import logout from '../../actions/login/logout-action';

class Unauthorized extends Component {
     componentWillMount = () => {
         this.props.resetError();
         this.props.logout();
         this.props.setStatus(true);
    }
    render() {
        return <div id="notfound">
            <div className="notfound">
                <div className="notfound-404">
                    <h1>Oops!</h1>
                </div>
                <h2>You have to be authorized!</h2>
            </div>
        </div>;
    }
}
const mapStateToProps = (state) => ({
    users: state.users
});

const mapDispatchToProps = (dispatch) => {
    return {
        logout: () => { dispatch(logout()) },
        setStatus: (data) => dispatch(toggleLoginModalState(data)),
        resetError: () => {
            dispatch(isOpen(false));
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Unauthorized);
