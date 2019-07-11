import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './Navmenu/NavMenu';

export default props => (
    <div className="container-fluid">
        <Row>
            <div className="col-2 m-0 p-0">
                <NavMenu />
            </div>
        <div className="col-10">
        {props.children}
        </div>
    </Row>
  </div>
);
