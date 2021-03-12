import React, { Component } from 'react';
import { connect } from 'react-redux';
import { TogleOpenWind } from '../../actions/modalWind';
import logout from '../../actions/login/logout-action';
import { isOpen } from '../../actions/modalWind'
import { setRegisterError } from '../../actions/register'
import { setEditUsernameError } from '../../actions/editProfile/editUsername'
import { setEditGenderError } from '../../actions/editProfile/editGender'
import { setEditBirthdayError } from '../../actions/editProfile/editBirthday'
import { getUsersError } from '../../actions/users/users'
import { getEventError } from '../../actions/event/event-item-view'
import { setRolesError } from '../../actions/roles'


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
        setStatus: (data) => dispatch(TogleOpenWind(data)),
        resetError: () => {
            dispatch(isOpen(false));
            dispatch(setRegisterError(null));
           dispatch(setEditUsernameError(null));
           dispatch(setEditGenderError(null));
           dispatch(setEditBirthdayError(null));
           dispatch(getUsersError(false));
           dispatch(getEventError(false));
           dispatch(setRolesError(false));
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Unauthorized);