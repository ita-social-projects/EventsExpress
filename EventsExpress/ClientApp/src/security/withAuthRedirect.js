import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { AuthenticationService } from '../services';

const api_serv = new AuthenticationService();

const usersHaveAnyOfRoles = (userInfo, rolesList) =>
    userInfo.roles.some((role) => rolesList.includes(role));

const withAuthRedirect = (allowedRoles) =>
    (Component) => {
        class RedirectComponent extends React.Component {
            token = api_serv.getCurrentToken();
            render() {
                const { user } = this.props;
                if (!this.token && user.id === null)
                    return <Redirect to='/unauthorized' />

                if (this.token && user.id !== null
                    && !usersHaveAnyOfRoles(user, allowedRoles))
                    return <Redirect to='/forbidden' />

                return <Component {...this.props} />
            }
        }

        let mapStateToProps = (state) => ({
            user: state.user
        });

        return connect(mapStateToProps)(RedirectComponent);
    }

export default withAuthRedirect;