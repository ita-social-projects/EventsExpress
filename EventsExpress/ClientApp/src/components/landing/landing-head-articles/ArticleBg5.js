import React, { Component } from "react";
import Button from "@material-ui/core/Button";
import { Link } from "react-router-dom";
import ModalWind from "../../modal-wind";
import AuthComponent from "../../../security/authComponent";
import HeadArticleContent from "./HeadArticleContent";
import "../landing.css";

export default class ArticleBg5 extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <article
        className="head-article-5"
        style={{
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        }}
      >
        {HeadArticleContent}
      </article>
    );
  }
}

