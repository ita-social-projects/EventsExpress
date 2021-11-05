import React, { Component } from "react";
import Button from "@material-ui/core/Button";
import { Link } from "react-router-dom";
import ModalWind from "../modal-wind";
import AuthComponent from "../../security/authComponent";
import "./landing.css";

export default class LandingHeadArticle extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { onLogoutClick, currentBg, id } = this.props;
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

    const articleBg = (
      <>
        {currentBg === 1 && (
          <article
            className="head-article-1"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
        {currentBg === 2 && (
          <article
            className="head-article-2"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
        {currentBg === 3 && (
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
        )}
        {currentBg === 4 && (
          <article
            className="head-article-4"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
        {currentBg === 5 && (
          <article
            className="head-article-5"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
        {currentBg === 6 && (
          <article
            className="head-article-6"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
        {currentBg === 7 && (
          <article
            className="head-article-7"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
        {currentBg === 8 && (
          <article
            className="head-article-8"
            style={{
              backgroundPosition: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
            }}
          >
            {articleDisplay}
          </article>
        )}
      </>
    );

    return articleBg;
  }
}
