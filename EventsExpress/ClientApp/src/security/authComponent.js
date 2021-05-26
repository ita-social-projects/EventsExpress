import React, { Component } from 'react';
import { connect } from 'react-redux';

class AuthComponent extends Component {
    render() {
        const { id, roles, rolesMatch, children, onlyAnonymous} = this.props;

        if (rolesMatch) {
            if (id && roles == rolesMatch) {
                return children;
            }
        }
        else if (onlyAnonymous) {
            if (!id) {
                return children;
            }
        }
        else {
           if (id) {
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