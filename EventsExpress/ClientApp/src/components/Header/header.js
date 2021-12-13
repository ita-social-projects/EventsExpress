import React, { Component } from "react";
import ModalWind from "../modal-wind";
import AuthComponent from "../../security/authComponent";
import Button from "@material-ui/core/Button";
import "./header.css";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import logout from "./../../actions/login/logout-action";
import CustomAvatar from "../avatar/custom-avatar";
import { Roles } from "../../constants/userRoles";

class Header extends Component {
  logout_reset = () => {
    this.props.hub.stop();
    this.props.logout();
  };

  render() {
    const { id, name } = this.props.user.id !== null ? this.props.user : {};

    return (
      <nav class="navbar navbar-expand-lg navbar-light extraHeaderStyles" id="bgcolornav">
        <div class="navbar-brand">
          <Link to={"/homepage"} className="nav-link" id="EEButton">
            EVENTS EXPRESS
          </Link>
        </div>
        <ul class="navbar-nav mr-auto"></ul>
        <span class="form-inline my-2 my-lg-0">
        <AuthComponent rolesMatch={Roles.User}>
        <div className="my-2 my-sm-0">
          <div className="btn btn-light" id="headbtn">
            Create Event
          </div>
        </div>
        </AuthComponent>
        <AuthComponent onlyAnonymous>
          <div className="my-2 my-sm-0">
            {!id && (
              <ModalWind
                renderButton={(action) => (
                  <div
                    className="btn btn-light"
                    id="headbtn"
                    className="btn btn-light navbtns"
                    variant="contained"
                    onClick={action}
                  >
                    Sign In/Up
                  </div>
                )}
              />
            )}
          </div>
        </AuthComponent>
        <AuthComponent>
          <div className="my-2 my-sm-0">
            <div className="btn-group">
              <div
                type="button"
                className="dropdown-toggle d-flex flex-row alignItemsCenter"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                <p id="userNameAlign">{name}</p>
                <CustomAvatar size="small" userId={id} name={name} />
              </div>
              <div className="dropdown-menu dropdown-menu-right bgcolorwhite">
                <AuthComponent rolesMatch={Roles.User}>
                  <button className="dropdown-item bgcolorwhite" type="button">
                    my events
                  </button>
                </AuthComponent>
                <AuthComponent rolesMatch={Roles.User}>
                  <Link className="removedecorations" to={"/user/" + id}>
                    <button
                      className="dropdown-item bgcolorwhite"
                      type="button"
                    >
                      my profile
                    </button>
                  </Link>
                </AuthComponent>
                <button className="dropdown-item bgcolorwhite" type="button" onClick={this.logout_reset}>
                  log out
                </button>
                <button className="dropdown-item bgcolorwhite" type="button">
                  help and feedback
                </button>
              </div>
            </div>
          </div>
        </AuthComponent>
        </span>
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
