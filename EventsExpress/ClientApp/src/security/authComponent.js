import React, { Component } from 'react';
import { connect } from 'react-redux';

class AuthComponent extends Component {
    render() {
        const {id, roles, roleMatch, children} = this.props;

        if( id && (roleMatch ? roles.includes(roleMatch) : true )){
            return children;
        }

        return <> </>
    }
}

let mapStateToProps = (state) => (
{
    id: state.user.id,
    roles: state.user.roles,
});

export default connect(mapStateToProps)(AuthComponent);