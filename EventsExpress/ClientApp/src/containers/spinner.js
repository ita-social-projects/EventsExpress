import React, { Component } from 'react';
import { connect } from 'react-redux';
import Spinner from '../components/spinner';

class SpinnerWrapper extends Component {

    render() {
        const { counter, showContent } = this.props;

        return counter > 0 || !showContent
            ? <Spinner />
            : this.props.children
    }
}

const mapStateToProps = (state) => ({
    counter: state.requestCount.counter
});

export default connect(mapStateToProps)(SpinnerWrapper);