import React, { Component } from "react";
import ModalWind from "../modal-wind";
import AuthComponent from "../../security/authComponent";
import "./landing.css";
import Button from "@material-ui/core/Button";
import { Link } from "react-router-dom";
import image1 from "./landing-images/1.jpg";
import image2 from "./landing-images/2.jpg";
import image3 from "./landing-images/3.jpg";
import image4 from "./landing-images/4.jpg";
import image5 from "./landing-images/5.jpg";
import image6 from "./landing-images/6.jpg";
import image7 from "./landing-images/7.jpg";
import image8 from "./landing-images/8.jpg";

const imagesPreload = [
  image1,
  image2,
  image3,
  image4,
  image5,
  image6,
  image7,
  image8,
];

export default class HeadArticle extends Component {
  constructor(props) {
    super(props);
    this.state = {
      currentImage: image1,
      preloadedPictures: [],
    };
    var preloadedData = imagesPreload.map((image) => {
      const newImage = new Image();
      newImage.src = image;
      return newImage;
    });

    this.setState.preloadedPictures = preloadedData;
  }

  async componentDidMount() {
    this.interval = setInterval(() => {
      this.setState({
        currentImage: imagesPreload[currentImage++ % imagesPreload.length],
      });
    }, 5000);
  }

  render() {
    const { currentImage } = this.state;
    const { onLogoutClick, id } = this.props;

    return (
      <article
        style={{
          backgroundImage: `url(${currentImage})`,
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
        }}
      >
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
      </article>
    );
  }
}
