import React, { Component } from "react";
import Carousel from "react-material-ui-carousel";
import CarouselEventCard from "./CarouselEventCard";
import ModalWind from "../modal-wind";
import AuthComponent from "../../security/authComponent";
import "./landing.css";
import { get_upcoming_events } from "../../actions/event/event-list-action";
import { connect } from "react-redux";
import HeadArticle from "./HeadArticle";

class Landing extends Component {
  constructor(props) {
    super(props);
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

  componentDidMount() {
    this.props.get_upcoming_events();
  }

  renderCarouselBlock = (eventBlock) => (
    <div className="carousel-block wd-100">
      {eventBlock.map((event) => (
        <CarouselEventCard key={event.id} event={event} />
      ))}
    </div>
  );

  render() {
    const { items } = this.props.events.data;
    const events = this.splitDataIntoBlocks(items);

    const carouselNavIsVisible = events.length > 1;

    return (
      <>
        <div className="main">
          <HeadArticle />
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

const mapStateToProps = (state) => {
  return {
    events: state.events,
    user: state.user,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    get_upcoming_events: () => dispatch(get_upcoming_events()),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Landing);
