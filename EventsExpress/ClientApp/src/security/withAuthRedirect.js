import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { AuthenticationService } from '../services';

const api_serv = new AuthenticationService();

const withAuthRedirect = (allowedRoles) =>
    (Component) => {
        class RedirectComponent extends React.Component {
            token = api_serv.getCurrentToken();
            render() {
                if (!this.token && this.props.user.id === null) return <Redirect to='/unauthorized' />

                if (this.token && this.props.user.id !== null && !allowedRoles.includes(this.props.user.role)) return <Redirect to='/forbidden' />

                return <Component {...this.props} />
            }
        }

        let mapStateToProps = (state) => ({
            user: state.user
        });

        return connect(mapStateToProps)(RedirectComponent);
    }

export default withAuthRedirect;