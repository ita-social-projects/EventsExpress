import React, { Component } from "react";
import ModalWind from "../modal-wind";
import AuthComponent from "../../security/authComponent";
import Button from "@material-ui/core/Button";
import "./header.css";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import logout from './../../actions/login/logout-action'

class Header extends Component {

  logout_reset = () => {
    this.props.hub.stop();
    this.props.logout();
  };

  render() {
    const { id } = this.props.user.id !== null ? this.props.user : {};

    return (
      <nav className="row" id="bgcolornav">
        <div className="col-md-10">
          <Link to={"/homepage"} className="nav-link" id="EEButton">
            EVENTS EXPRESS
          </Link>
        </div>
        <AuthComponent onlyAnonymous>
          <div className="col-md-1">
            {!id && (
              <ModalWind
                renderButton={(action) => (
                  <Button
                    className="btn btn-light navbtns"
                    variant="contained"
                    onClick={action}
                  >
                    Sign In/Up
                  </Button>
                )}
              />
            )}
          </div>
        </AuthComponent>
        <AuthComponent>
          <div className="col-md-2 text-right">
            <div className="btn btn-light navbtns" onClick={this.logout_reset}>Log out</div>
          </div>
        </AuthComponent>
      </nav>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    user: state.user,
    hub: state.hubConnections.chatHub,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    logout: () => {
      dispatch(logout());
    },
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Header);
