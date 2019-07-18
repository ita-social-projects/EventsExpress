import React from 'react';
import LeftSidebar from '../left-sidebar';
import RightSidebar from '../right-sidebar';
import './layout.css';

import './colorlib.css';

const Layout = ({ children }) => {
    return (
        <>
            <div className="container-fluid">
                <div className="row">
                    <div className="col-3 position-fixed">

                    <LeftSidebar />
                    </div>
                    <div className="col-9 offset-3">
                        <div className="row">
                            <div className="col-9">
                                {children}
                            </div>
                            <div className="col-3">
                                <RightSidebar />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Layout;