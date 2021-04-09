import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';

const withAuthRedirect = (allowedRoles) =>
    ({ Component }) => {

        let mapStateToPropsRedirect = (state) => ({
            user: state.user,
            login: state.login
        });

        class RedirectComponent extends React.Component {
            render() {
                console.log("my hoc");
                if (this.props.login.isLoginSuccess) return <Redirect to='/unauthorized' />
                else if (!allowedRoles.includes(this.props.user.role)) return <Redirect to='/forbidden' />

                return <Component {...this.props} />
            }
        }

        let ConnectedRedirectComponent = connect(mapStateToPropsRedirect)(RedirectComponent);

        return ConnectedRedirectComponent;
    }

export default withAuthRedirect;