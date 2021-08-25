import React, { Component } from 'react';
import Carousel from 'react-material-ui-carousel';
import CarouselEventCard from './CarouselEventCard';
import EventService from '../../services/EventService';
import { Link } from "react-router-dom"
import ModalWind from '../modal-wind';
import AuthComponent from '../../security/authComponent';
import BackgroundSlider from 'react-background-slider';
import './landing.css';
import './ChangeBackground';
import image_1 from './images/image-1.jpg';
import image_2 from './images/image-2.jpg';
import image_3 from './images/image-3.jpg';
import image_4 from './images/image-4.jpg';
import image_5 from './images/image-5.jpg';
import image_6 from './images/image-6.jpg';
import image_7 from './images/image-7.jpg';
import image_8 from './images/image-8.jpg';

const eventService = new EventService()

export default class Landing extends Component {
    constructor(props) {
        super(props)

        this.state = {
            events: []
                }
    }

    handleClick = () => {
        this.props.onSubmit();
    }

    splitDataIntoBlocks(itemsArray) {
        return itemsArray.reduce((acc, c, i) => {
            if ((i & 3) === 0) acc.push([])
            acc[acc.length - 1].push(c)
            return acc
        }, [])
    }

    async componentDidMount() {
        let events = await eventService.getAllEvents("?Page=1")
        events = (await events.json()).items
        if (events.length !== 0) {
            this.setState({ events: this.splitDataIntoBlocks(events) })
        }
    }

    renderCarouselBlock = (eventBlock) => (
        <div className="carousel-block wd-100">
            {eventBlock.map((event) => <CarouselEventCard key={event.id} event={event} />)}
        </div>
    )
        
    render() {
        const { events } = this.state
        const carouselNavIsVisible = events.length > 1
        const { onLogoutClick } = this.props;
        const { id } = this.props.user;
      
        return (<>
            <div className="main">
                <article className="head-article">
                    <BackgroundSlider
                        images={[image_1,
                            image_2,
                            image_3,
                            image_4,
                            image_5,
                            image_6,
                            image_7,
                            image_8
                            ]}
                        duration={5} transition={2} />
                    <nav className="row">
                        <div className="col-md-10">
                            <h1>EventsExpress</h1>
                        </div>
                        <AuthComponent onlyAnonymous>
                            <div className="col-md-1">
                            {
                                !id && (<ModalWind />)
                            }
                        </div>
                        </AuthComponent>
                        <AuthComponent>
                            <div className="col-md-2 text-right">
                                <div onClick={onLogoutClick} className="btn">Log out</div>
                            </div>
                        </AuthComponent>
                    </nav>
                    
                    <div className="button-container text-center">
                        <div id="square"> </div>
                        <div id="text-container">
                            <h2>What do you want to do?</h2>
                            <div className="buttons">
                                <button className="btn btn-warning" onClick={this.handleClick}>Create event</button>
                                <Link to={"home/events"} className="btn btn-warning">Find event</Link>
                            </div>
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
