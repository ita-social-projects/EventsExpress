import React, { Component } from "react";
import AuthComponent from "../../security/authComponent";
import "./header.css";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import logout from "./../../actions/login/logout-action";
import CustomAvatar from "../avatar/custom-avatar";
import { Roles } from "../../constants/userRoles";
import add_event from "../../actions/event/event-add-action";
import { toggleLoginModalState } from "../../actions/login-modal";
import HeaderButton from "./header-button";

class Header extends Component {
  logout_reset = () => {
    this.props.hub.stop();
    this.props.logout();
  };

  render() {
    const { id, name } = this.props.user.id !== null ? this.props.user : {};

    const {onLoginModalOpen, add_event} = this.props;

    return (
      <nav
        className="navbar navbar-expand-lg navbar-light extraHeaderStyles"
        id="bgcolornav"
      >
        <div className="navbar-brand">
          <Link to={"/home"} className="nav-link" id="EEButton">
            EVENTS EXPRESS
          </Link>
        </div>
        <ul className="navbar-nav mr-auto"></ul>
        <span className="form-inline my-2 my-lg-0">
          <div className="my-2 my-sm-0 mr-3">
            <HeaderButton
              onClick={!id ? onLoginModalOpen : add_event}
            >
              Create Event
            </HeaderButton>
          </div>
          <AuthComponent onlyAnonymous>
            <div className="my-2 my-sm-0">
              {!id && (
                <HeaderButton onClick={onLoginModalOpen}>
                  Sign In/Up
                </HeaderButton>
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
                    <Link className="removedecorations" to={"/user/" + id}>
                      <button
                        className="dropdown-item bgcolorwhite"
                        type="button"
                      >
                        my events
                      </button>
                    </Link>
                  </AuthComponent>
                  <AuthComponent>
                    <Link className="removedecorations" to={"/editProfile"}>
                      <button
                        className="dropdown-item bgcolorwhite"
                        type="button"
                      >
                        my profile
                      </button>
                    </Link>
                  </AuthComponent>
                  <button
                    className="dropdown-item bgcolorwhite"
                    type="button"
                    onClick={this.logout_reset}
                  >
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
    add_event: () => dispatch(add_event()),
    onLoginModalOpen: () => dispatch(toggleLoginModalState(true)),
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Header);
