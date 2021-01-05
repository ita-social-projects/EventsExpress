import React from 'react';

import './spinner.css';

const Spinner = () =>{
    return (
        <div id="notfound">
            <div className="notfound">
        <div className="lds-css ng-scopex notfound-404">
            <div className="lds-rolling">
                <div></div>
            </div>
        </div>
        </div>
        </div>
    );
}

export default Spinner;