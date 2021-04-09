import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';

let mapStateToPropsRedirect = (state) => ({
    user: state.user
});

export default function withAuthRedirect (Component) {

    class RedirectComponent extends React.Component {
        render() {
            if (this.props.user.id === null && this.props.user.role === null) return <Redirect to='/unauthorized' />
            else if (this.props.user.role !== "Admin") return <Redirect to='/forbidden' />

            return <Component {...this.props} />
        }
    }

    let ConnectedRedirectComponent = connect(mapStateToPropsRedirect)(RedirectComponent);

    return ConnectedRedirectComponent;
}