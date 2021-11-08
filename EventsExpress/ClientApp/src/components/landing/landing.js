import React, { Component } from "react";
import Carousel from "react-material-ui-carousel";
import CarouselEventCard from "./CarouselEventCard";
import EventService from "../../services/EventService";
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
import SpinnerWrapper from "../../containers/spinner";

const eventService = new EventService();

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
export default class Landing extends Component {
  constructor(props) {
    super(props);
    this.state = {
      currentBg: 1,
      currentImage: image1,
      events: [],
    };
  }

  handleClick = () => {
    this.props.onSubmit();
  };

  splitDataIntoBlocks(itemsArray) {
    return itemsArray.reduce((acc, c, i) => {
      if ((i & 3) === 0) acc.push([]);
      acc[acc.length - 1].push(c);
      return acc;
    }, []);
  }

  async componentDidMount() {
    let events = await eventService.getAllEvents("?Page=1");
    events = (await events.json()).items;
    if (events.length !== 0) {
      this.setState({ events: this.splitDataIntoBlocks(events) });
    }

    imagesPreload.forEach((image) => {
      const newImage = new Image();
      newImage.src = image;
      window[image] = newImage;
    });

    this.interval = setInterval(() => {
      this.setState({
        currentImage:
          imagesPreload[Math.floor(Math.random() * imagesPreload.length)],
      });
    }, 5000);
  }

  renderCarouselBlock = (eventBlock) => (
    <div className="carousel-block wd-100">
      {eventBlock.map((event) => (
        <CarouselEventCard key={event.id} event={event} />
      ))}
    </div>
  );

  render() {
    const { events, currentBg, currentImage } = this.state;
    const carouselNavIsVisible = events.length > 1;
    const { onLogoutClick } = this.props;
    const { id } = this.props.user;

    return (
      <>
        {" "}
        <SpinnerWrapper showContent={window.length < imagesPreload.length}>
          <div className="main">
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
                  <button
                    className="btn btn-warning"
                    onClick={this.handleClick}
                  >
                    Create event
                  </button>
                  <Link to={"home/events"} className="btn btn-warning">
                    Find event
                  </Link>
                </div>
              </div>
            </article>{" "}
            <article className="works-article text-center">
              <div className="works-title">
                <h2>How EventsExpress Works</h2>
              </div>
              <div className="icons-wrapper">
                <div>
                  <i className="fas fa-hand-pointer"></i>
                  <h3>Search Events</h3>
                </div>
                <div>
                  <i className="fas fa-user-plus"></i>
                  <h3>Create Your Own Event</h3>
                </div>
                <div>
                  <i className="fas fa-search"></i>
                  <h3>Find Events Partners</h3>
                </div>
                <div>
                  <i className="far fa-smile"></i>
                  <h3>Get Connected</h3>
                </div>
                <div>
                  <i className="fas fa-users"></i>
                  <h3>Have Fun Together</h3>
                </div>
              </div>
              <AuthComponent onlyAnonymous>
                <div className="text-center">
                  <ModalWind
                    renderButton={(action) => (
                      <button
                        className="btn btn-warning"
                        onClick={() => action()}
                      >
                        Join EventsExpress
                      </button>
                    )}
                  />
                </div>
              </AuthComponent>
            </article>
            {events.length !== 0 && (
              <article className="events-article">
                <div className="row">
                  <div className="col-md-10">
                    <h3>Upcoming events</h3>
                  </div>
                  <div style={{ textAlign: "right" }} className="col-md-2">
                    <a href="/home/events">Explore more events</a>
                  </div>
                </div>
                <div className="carousel-wrapper text-center">
                  <Carousel
                    autoPlay={false}
                    animation={"slide"}
                    interval={1000}
                    indicators={false}
                    navButtonsAlwaysVisible={carouselNavIsVisible}
                    navButtonsAlwaysInvisible={!carouselNavIsVisible}
                    NextIcon={
                      <i
                        style={{ width: 32 + "px", height: 32 + "px" }}
                        className="fas fa-angle-right"
                      ></i>
                    }
                    PrevIcon={
                      <i
                        style={{ width: 32 + "px", height: 32 + "px" }}
                        className="fas fa-angle-left"
                      ></i>
                    }
                  >
                    {events.map((block) => this.renderCarouselBlock(block))}
                  </Carousel>
                </div>
              </article>
            )}
          </div>
        </SpinnerWrapper>
      </>
    );
  }
}
