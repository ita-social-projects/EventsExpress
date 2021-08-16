import React, { Component } from 'react';
import Carousel from 'react-material-ui-carousel';
import { Link } from "react-router-dom"
import ModalWind from '../modal-wind';
import './landing.css';

export default class Landing extends Component {
    handleClick = () => {
        this.props.onSubmit();

    }
    render() {
        var event = {
                    id: "157cf1f2-5d0d-4ca5-6b2f-08d950fd9320/1",
                    img: "https://c.pxhere.com/photos/da/5a/silhouette_in_xinjiang_ghost_city_people_sunset_joke_together-454504.jpg!d",
                    date: "01.01.2000 00:00 GTM+0",
                    name: "Sample Name",
                    author: "Sample Author",
                    part: 10000
                }
        var eventsBlock = [event, event, event, event]
        var events = [eventsBlock, eventsBlock, eventsBlock]
        const { id } = this.props.user;

        return (<>
            <div className="main">
                <article className="head-article">
                    <nav className="row">
                        <div className="col-md-10">
                            <h1>EventsExpress</h1>
                        </div>
                        <div className="col-md-1">
                            {
                                !id && (<ModalWind />)
                            }
                        </div>
                    </nav>
                    <div className="button-container text-center">
                        <h2>What do you want to do?</h2>
                        <div className="buttons">
                            <button className="btn btn-warning" onClick={this.handleClick}>Create event</button>
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
                            navButtonsAlwaysVisible={true}
                            NextIcon={<i style={{ width: 32 + 'px', height: 32 + 'px' }} className="fas fa-angle-right"></i>}
                            PrevIcon={<i style={{ width: 32 + 'px', height: 32 + 'px' }} className="fas fa-angle-left"></i>}
                        >
                            {
                                events.map((block, i) => 
                                    <>
                                        <div className="carousel-block wd-100">
                                            {block.map((event, j) => <Card key={block.length * i + j} event={event} />)}
                                        </div>
                                    </>
                                )
                            }
                        </Carousel>
                    </div>
                </article>
            </div>
        </>);
    }
}

function Card(props) {
    return (<>
        <div className="card">
            <img className="card-img-top" src={props.event.img} alt="Card image cap" />
            <div className="card-body">
                <p className="card-text text-muted">{props.event.date}</p>
                <p className="card-text">{props.event.name}</p>
                <p className="card-text text-muted">{props.event.author}</p>
                <div className="row">
                    <div className="col-md-6">
                        Participants: {props.event.part}
                    </div>
                    <div className="col-md-6">
                        <a href={`/home/events/${props.event.id}`} className="link">Join event</a>
                    </div>
                </div>
            </div>
        </div>
    </>
    )
}
