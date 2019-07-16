import React from 'react';
import LeftSidebar from '../left-sidebar';
import RightSidebar from '../right-sidebar';
import './layout.css';

const Layout = ({ children }) => {
    return (
        <div className="container-fluid">
            <div className="row">
                <div className="col-3">
                    <LeftSidebar />
                </div>
                <div className="col-6">
                    {children}
                </div>
                <div className="col-3">
                    <RightSidebar />
                </div>
            </div>
        </div>
    );
}

export default Layout;