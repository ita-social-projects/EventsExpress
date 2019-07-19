import React from 'react';

import './spinner.css';

const Spinner = () =>{
    return (
        <center>
        <div className="lds-css ng-scope">
            <div className="lds-rolling">
                <div></div>
            </div>
        </div>
        </center>
    );
}

export default Spinner;