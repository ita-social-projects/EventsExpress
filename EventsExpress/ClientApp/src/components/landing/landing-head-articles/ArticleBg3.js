import React, { Component } from "react";
import Button from "@material-ui/core/Button";
import { Link } from "react-router-dom";
import ModalWind from "../../modal-wind";
import AuthComponent from "../../../security/authComponent";
import "../landing.css";

export default class ArticleBg3 extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { onLogoutClick, id } = this.props;
    const articleDisplay = (
      <>
        <nav className="row">
          <div className="col-md-10">
            <h1>EventsExpress</h1>
          </div>
          <AuthComponent onlyAnonymous>
            <div className="col-md-1">
              {!id && (
                <ModalWind
                  renderButton={(action) => (
                    <Button
                      className="mt-5 btn btn-warning"
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
              <div onClick={onLogoutClick} className="btn">
                Log out
              </div>
            </div>
          </AuthComponent>
        </nav>
        <div className="button-container text-center">
          <h2>What do you want to do?</h2>
          <div className="buttons">
            <button className="btn btn-warning" onClick={this.handleClick}>
              Create event
            </button>
            <Link to={"home/events"} className="btn btn-warning">
              Find event
            </Link>
          </div>
        </div>
      </>
    );

    return (
      <article
        className="head-article-3"
        style={{
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        }}
      >
        {articleDisplay}
      </article>
    );
  }
}
