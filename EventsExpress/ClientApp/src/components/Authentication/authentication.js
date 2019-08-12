import React, { Component } from 'react';
import { connect } from 'react-redux'
import _authenticate from '../../actions/authentication';

 class Authentication extends Component {

    componentWillMount = () => {

        console.log("componentWillMount");
        const { id, token } = this.props.match.params;

        console.log({ userId: id, token: token });
        this.props.auth({ userId: id, token: token })
        

    }

    render() {
        return (
            <>
            </>
            )
    }
}

const mapStateToProps = state => (state.user);

const mapDispatchToProps = dispatch => {
    return {
        auth: (data) => dispatch(_authenticate(data))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Authentication);