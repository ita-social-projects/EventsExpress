import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Header from '../shared/Header';


const actionCreators = {
    login: ( email, password ) => ({ type: "LOGIN", payload: { email, password } })
}


const mapDispatchToProps = dispatch => {
    return {
        // dispatching plain actions
        login: (email, password) => dispatch(actionCreators.login(email, password)),
    };
}

const mapStateToProps = (state) => ({
  ...state })


class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: "",
            token: ""
        }
    }



    updateLoginValue = (event) => {
        this.setState({ login: event.target.value });
    }

    updatePasswordValue = (event) => {
        this.setState({ password: event.target.value });
    }


    

    render = () =>

        <div className="signup-form">
            <form method='post'>
                <h2>Login</h2>
                <hr />
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i class="fa fa-paper-plane"></i></span>
                        <input type="email" value={this.state.login} onChange={this.updateLoginValue} className="form-control" name="email" placeholder="Email Address" required="required" />
			        </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i class="fa fa-lock"></i></span>
                        <input type="password" value={this.state.password} onChange={this.updatePasswordValue} className="form-control" name="password" placeholder="Password" required="required" />
			        </div>
                </div>
                <LinkContainer to={'/account/register'} exact>
                    <NavItem>
                        <Glyphicon /> Register
                    </NavItem>
                </LinkContainer>
            </form>

            <button onClick={() => this.props.login(this.state.login, this.state.password)}>Send</button>
        </div>



}

export default connect(mapStateToProps,
    mapDispatchToProps)(LoginForm);