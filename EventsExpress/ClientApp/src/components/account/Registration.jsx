import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';
import '../css/Register.css'

export default class RegistrationForm extends React.Component {
    constructor(props) {
        super(props);

    }

    render = () =>

        <div className="signup-form">
            <form  method="post">
                <h2>Registration</h2>
                <p>Please fill in this form to create an account!</p>
                <hr />
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i class="fa fa-paper-plane"></i></span>
                        <input type="email" className="form-control" name="email" placeholder="Email Address" required="required" />
			        </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><i class="fa fa-lock"></i></span>
                        <input type="password" className="form-control" name="password" placeholder="Password" required="required" />
			        </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon">
                            <i className="fa fa-lock"></i>
                            <i className="fa fa-check"></i>
                        </span>
                        <input type="password" className="form-control" name="confirm_password" placeholder="Confirm Password" required="required" />
			        </div>
                </div>
                <div className="form-group">
                    <button type="submit" class="btn btn-primary btn-lg">Register</button>
                </div>
            </form>
        </div>
}

