import React, { Component } from 'react';
import { connect } from 'react-redux';

class AuthComponent extends Component {
    render() {
        if(this.props.roleMatch){
            if (this.props.id && this.props.role == this.props.roleMatch) {
                return this.props.children;
            }
        }
        else{
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
    role: state.user.role,
});

export default connect(mapStateToProps)(AuthComponent);