import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';

const withAuthRedirect = (allowedRoles) =>
    (Component) => {
        class RedirectComponent extends React.Component {
            render() {
                if (!localStorage.getItem('token') && this.props.user.id === null) return <Redirect to='/unauthorized' />

                if (localStorage.getItem('token') && this.props.user.id !== null && !allowedRoles.includes(this.props.user.role)) return <Redirect to='/forbidden' />

                return <Component {...this.props} />
            }
        }

        let mapStateToProps = (state) => ({
            user: state.user
        });

        return connect(mapStateToProps)(RedirectComponent);
    }

export default withAuthRedirect;