import React, { Component } from 'react';
import { connect } from 'react-redux';
import Spinner from '../components/spinner';

class LocalSpinnerWrapper extends Component {

    render() {
        const { localCounter, showContent } = this.props;
        console.log(this.props);
        return localCounter > 0 || !showContent
            ? <Spinner />
            : this.props.children
    }
}

const mapStateToProps = (state) => ({
    localCounter: state.requestLocalCount.localCounter
});

export default connect(mapStateToProps)(LocalSpinnerWrapper);