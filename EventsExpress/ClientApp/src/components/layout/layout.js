import React from 'react';
import LeftSidebarWrapper from '../../containers/left-sidebar';

import RightSidebar from '../right-sidebar';
import './layout.css';

import './colorlib.css';

const Layout = ({ children }) => {
    return (
        <>
            <div className="container-fluid">
                <div className="row">
                    <div className="col-3 position-fixed">

                    <LeftSidebarWrapper />
                    </div>
                    <div className="col-9 offset-3">
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