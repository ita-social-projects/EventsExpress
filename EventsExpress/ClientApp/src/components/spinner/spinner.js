import React, { Component } from 'react';
import { connect } from 'react-redux';
import './spinner.css';

class Spinner extends Component {

    render() {
        const { counter, showContent } = this.props;

        const spinner = <div id="notfound">
            <div className="notfound">
                <div className="lds-css ng-scopex notfound-404">
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