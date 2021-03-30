import React, { Component } from 'react';
import EventsForProfile from '../../event/events-for-profile';
import Spinner from '../../spinner';
import 'moment-timezone';
import '../User-profile.css';

export default class ArchiveEvents extends Component {
    render() {
        const { isPending, data } = this.props.events;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending
            ? <EventsForProfile
                data_list={data.items}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                current_user={this.props.current_user}
                callback={this.props.typeOfEvents} />
            : null;

        return (
            <div className="shadow pl-2 pr-2 pb-2 mb-5 bg-white rounded">
                {spinner}{content}
                {!isPending && !(data.items && data.items.length > 0) &&
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