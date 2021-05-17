import React, { Component } from 'react';
import { connect } from 'react-redux';
import { usersHaveAnyOfRoles } from './auth-utils';

class AuthComponent extends Component {
    render() {
        const {id, roles, rolesMatch, children} = this.props;

        if( id && (rolesMatch ? usersHaveAnyOfRoles(roles, rolesMatch) : true )){
            return children;
        }
        else if (this.props.onlyAnonymous) {
            if (!!this.props.onlyAnonymous && !this.props.id) {
                return this.props.children;
            }
        }

        else {
            if (this.props.id) {
                return this.props.children;
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