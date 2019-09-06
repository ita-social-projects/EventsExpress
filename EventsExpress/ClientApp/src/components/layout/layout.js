import React from 'react';
import LeftSidebarWrapper from '../../containers/left-sidebar';
import AlertContainer from '../../containers/alert';
import DialogContainer from '../../containers/dialog';
import './layout.css';

import './colorlib.css';


const Layout = ({ children }) => {
    return (
        <>
            <div className="container-fluid">
                <div className="row">
                    <div className="col-3">
                        <LeftSidebarWrapper />
                        <AlertContainer />
                    </div>
                    <div className="col-9">
                        <div className="row">
                            <div className="col-12">  
                            {children}
                            </div> 
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Layout;