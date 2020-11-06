import OccurenceEventModal from '../components/event/occurence-event-modal';
import React, { Component } from 'react';
import Button from "@material-ui/core/Button";
import EventsExpressService from '../services/EventsExpressService';

const api_serv = new EventsExpressService();

class OccurenceEvent extends Component {

    constructor() {
        super();

        this.state = {
            modalShow: false,
            setModalShow: false,
            event: {}
        };

        this.handleClick = this.handleClick.bind(this);
    }

    componentDidMount() {
        let data = api_serv.getOccurenceEvent();
        this.setState({
            event: data
        });
        console.log(this.state);
    }

    handleClick() {
        this.setState({
            modalShow: true
        });
    }

    handleHide() {
        this.setState({
            modalShow: false
        });
    }

    render() {

        return (
            <>
                <Button onClick={this.handleClick}>
                    Launch modal
                </Button>
                <OccurenceEventModal show={this.state.modalShow} onHide={this.handleHide} />
            </>
        );
    }
}

export default OccurenceEvent