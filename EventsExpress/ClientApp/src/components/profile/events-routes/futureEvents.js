import React, { Component } from 'react';
import Event from '../../event/event-item';
import EventsForProfile from '../../event/events-for-profile';
import Spinner from '../../spinner/spinner';
import 'moment-timezone';
import '../User-profile.css';

export default class FutureEvents extends Component {
    render() {
        return (
                // this.props.add_event_flag ?
                // <div className="shadow mb-5 bg-white rounded">
                //     <AddEventWrapper onCreateCanceling={() => this.handleChange(null, 0)} />
                // </div>
                // :
                <div className="shadow pl-2 pr-2 pb-2 mb-5 bg-white rounded">
                    {this.props.spinner}{this.props.content}
                    {!this.props.isPending && !(this.props.data.items && this.props.data.items.length > 0) &&
                        <div id="notfound" className="w-100">
                            <div className="notfound">
                                <div className="notfound-404">
                                    <div className="h1">No Results</div>
                                </div>
                            </div>
                        </div>}
                </div>
        );
    }
}