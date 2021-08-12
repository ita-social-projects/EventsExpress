import React, { Component } from 'react';
import { connect } from 'react-redux';

import Landing from '../components/landing';

class LandingWrapper extends Component {
    render() {
        return <Landing user={this.props.user} />
    }
}

const mapStateToProps = (state) => ({
    user: state.user
});

export default connect(mapStateToProps)(Landing)