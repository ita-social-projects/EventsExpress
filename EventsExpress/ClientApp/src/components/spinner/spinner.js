import React, { Component } from 'react';
import './spinner.css';

export default class Spinner extends Component {

    render() {

        return <div id="spinner-align">
            <div className="spinner-align">
                <div className="lds-css ng-scope">
                    <div className="lds-rolling">
                        <div />
                    </div>
                </div>
            </div>
        </div>
    }
}
