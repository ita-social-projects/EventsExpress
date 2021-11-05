﻿import React, { Component } from "react";
import Carousel from "react-material-ui-carousel";
import CarouselEventCard from "./CarouselEventCard";
import EventService from "../../services/EventService";
import ModalWind from "../modal-wind";
import AuthComponent from "../../security/authComponent";
import "./landing.css";
import LandingHeadArticle from "./LandingHeadArticle";
import ArticleBg1 from "./landing-head-articles/ArticleBg1";
import ArticleBg2 from "./landing-head-articles/ArticleBg2";
import ArticleBg3 from "./landing-head-articles/ArticleBg3";
import ArticleBg4 from "./landing-head-articles/ArticleBg4";
import ArticleBg5 from "./landing-head-articles/ArticleBg5";
import ArticleBg6 from "./landing-head-articles/ArticleBg6";
import ArticleBg7 from "./landing-head-articles/ArticleBg7";
import ArticleBg8 from "./landing-head-articles/ArticleBg8";


/* const ArticleBg1Promise = import("./landing-head-articles/ArticleBg1");
const ArticleBg1 = React.lazy(() => ArticleBg1Promise);
const ArticleBg2Promise = import("./landing-head-articles/ArticleBg2");
const ArticleBg2 = React.lazy(() => ArticleBg2Promise);
const ArticleBg3Promise = import("./landing-head-articles/ArticleBg3");
const ArticleBg3 = React.lazy(() => ArticleBg3Promise);
const ArticleBg4Promise = import("./landing-head-articles/ArticleBg4");
const ArticleBg4 = React.lazy(() => ArticleBg4Promise);
const ArticleBg5Promise = import("./landing-head-articles/ArticleBg5");
const ArticleBg5 = React.lazy(() => ArticleBg5Promise);
const ArticleBg6Promise = import("./landing-head-articles/ArticleBg6");
const ArticleBg6 = React.lazy(() => ArticleBg6Promise);
const ArticleBg7Promise = import("./landing-head-articles/ArticleBg7");
const ArticleBg7 = React.lazy(() => ArticleBg7Promise);
const ArticleBg8Promise = import("./landing-head-articles/ArticleBg8");
const ArticleBg8 = React.lazy(() => ArticleBg8Promise);
 */


const eventService = new EventService();

export default class Landing extends Component {
  constructor(props) {
    super(props);
    this.state = {
      currentBg: 1,
      ImagesLoadingCounter: 0,
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

    this.interval = setInterval(() => {
      this.setState({
        currentBg: (this.state.currentBg % 8) + 1,
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
    const { events, currentBg } = this.state;
    const carouselNavIsVisible = events.length > 1;
    const { onLogoutClick } = this.props;
    const { id } = this.props.user;

    return (
      <>
        <div className="main">
                   {currentBg === 1 && (
            <ArticleBg1 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 2 && (
            <ArticleBg2 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 3 && (
            <ArticleBg3 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 4 && (
            <ArticleBg4 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 5 && (
            <ArticleBg5 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 6 && (
            <ArticleBg6 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 7 && (
            <ArticleBg7 onLogoutClick={onLogoutClick} id={id} />
          )}
          {currentBg === 8 && (
            <ArticleBg8 onLogoutClick={onLogoutClick} id={id} />
          )} 




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
      </>
    );
  }
}
