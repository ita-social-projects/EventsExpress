import React, { Component } from 'react';
import { connect } from 'react-redux'
import { loginAfterEmailConfirmation } from '../../actions/login-action';

class Authentication extends Component {
    componentWillMount = () => {
        const { id, token } = this.props.match.params;
        this.props.auth({ userId: id, token: token })
    }

    render() {
        return (
            <div className="mt-5 b-inline-block">
                <div className='h3 text-center alert alert-success'>
                    Our congratulation, Your registration was successful!
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => (state.user);

const mapDispatchToProps = dispatch => {
    return {
        auth: (data) => dispatch(loginAfterEmailConfirmation(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Authentication);
