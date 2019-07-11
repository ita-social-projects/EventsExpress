import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { actionCreators } from '../../reducers/login';


export default class Login extends React.Component {
    constructor(props) {
        super(props);
    }

    render = () =>
        <div className="signup-form">
            <form  method='post'>
                <h2>Login</h2>
                <hr />
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i className="fa fa-paper-plane"></i></span>
                        <input type="email" className="form-control" name="email" placeholder="Email Address" required="required" />
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i className="fa fa-lock"></i></span>
                        <input type="password" className="form-control" name="password" placeholder="Password" required="required" />
                    </div>
                </div>
                <LinkContainer to={'/account/register'} exact>
                    <NavItem>
                        <Glyphicon glyph='th-reg' /> Register
                    </NavItem>
                </LinkContainer>

            </form>

            <button onClick={() => this.props.login()}>Send</button>
        </div>
}