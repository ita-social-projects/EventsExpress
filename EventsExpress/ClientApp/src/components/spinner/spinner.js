import React, { Component } from 'react';
import { connect } from 'react-redux';
import './spinner.css';

class Spinner extends Component {

    render() {
        const { counter, showContent } = this.props;

        const spinner = <div id="spinner-align">
            <div className="spinner-align">
                <div className="lds-css ng-scopex">
                    <div className="lds-rolling">
                        <div></div>
                    </div>
                </div>
            </div>
        </div>

        return counter > 0 || !showContent
            ? spinner
            : this.props.children
    }
}

const mapStateToProps = (state) => ({
    counter: state.requestCount.counter
});

export default connect(mapStateToProps)(Spinner);