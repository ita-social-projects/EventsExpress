import React, { Component } from 'react';
import { connect } from 'react-redux';
import { usersHaveAnyOfRoles } from './auth-utils';

class AuthComponent extends Component {
    render() {
        const { id, roles, rolesMatch, children, onlyAnonymous} = this.props;

        if( id && (rolesMatch ? usersHaveAnyOfRoles(roles, rolesMatch) : true )){
            return children;
        }
        if (onlyAnonymous) {
            if (!!onlyAnonymous && !id) {
                return children;
            }
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