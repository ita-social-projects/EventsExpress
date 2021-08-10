import React, { Component } from 'react';
import Carousel from 'react-material-ui-carousel';
import CarouselEventCard from './CarouselEventCard';
import EventService from '../../services/EventService';
import './landing.css';

const eventService = new EventService()

export default class Landing extends Component {
    constructor(props) {
        super(props)

        this.state = {
            events: []
                }
    }

    splitData(data) {
        return data.reduce((acc, c, i) => {
            if ((i & 3) === 0) acc.push([])
            acc[acc.length - 1].push(c)
            return acc
        }, [])
    }

    componentDidMount() {
        eventService.getAllEvents("")
            .then((response) => response.json())
            .then((data) => {
                console.log(data)
                var items = data.items
                if (items.length !== 0) {
                    this.setState({ events: this.splitData(items) })
                }
            })
    }

    renderCarouselBlock = (eventBlock) => (
        <div className="carousel-block wd-100">
            {eventBlock.map((event) => <CarouselEventCard key={event.id} event={event} />)}
        </div>
    )
        
    render() {
        const { events } = this.state
        const carouselNavIsVisible = events.length > 1
        return (<>
            <div className="main">
                <article className="head-article">
                    <nav className="row">
                        <div className="col-md-10">
                            <h1>EventsExpress</h1>
                        </div>
                        <div className="col-md-1">
                            <a className="nav-link" href="/">Log in</a>
                        </div>
                        <div className="col-md-1">
                            <a className="nav-link" href="/">Sign up</a>
                        </div>
                    </nav>
                    <div className="button-container text-center">
                        <h2>What do you want to do?</h2>
                        <div className="buttons">
                            <button className="btn btn-warning">Create event</button>
                            <button className="btn btn-warning">Find event</button>
                        </div>
                    </div>
                </article>
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
                    <div className="text-center">
                        <button className="btn btn-warning">Join EventsExpress</button>
                    </div>
                </article>
                {events.length !== 0 &&
                <article className="events-article">
                    <div className="row">
                        <div className="col-md-10">
                            <h3>Upcoming events</h3>
                        </div>
                        <div style={{ textAlign: 'right' }} className="col-md-2">
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

                            NextIcon={<i style={{ width: 32 + 'px', height: 32 + 'px' }} className="fas fa-angle-right"></i>}
                            PrevIcon={<i style={{ width: 32 + 'px', height: 32 + 'px' }} className="fas fa-angle-left"></i>}
                        >
                            {
                                    events.map((block) => this.renderCarouselBlock(block))
                            }
                        </Carousel>
                    </div>
                </article>
                }
            </div>
        </>);
    }
}
