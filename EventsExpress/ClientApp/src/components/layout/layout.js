import React from 'react';
import LeftSidebarWrapper from '../../containers/left-sidebar';
import AlertContainer from '../../containers/alert';
import './layout.css';
import './colorlib.css';

const Layout = ({ children }) => {
    return (
        <>
            <LeftSidebarWrapper />
            <AlertContainer />
            <div id="main" className="container-fluid h-100 pl-5">
                {children}
            </div>
        </>
    );
}

export default Layout;
