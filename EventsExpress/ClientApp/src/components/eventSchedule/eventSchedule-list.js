import React, { Component } from 'react';
import EventSchedule from './eventSchedule-item';

class EventSchedulesList extends Component {

    renderItems = arr =>
        arr.map(item => (
            <EventSchedule
                key={item.id}
                item={item}
                current_user={this.props.current_user}
            />
        ));

    render() {
        const items = this.renderItems(this.props.data_list);
        const { data_list } = this.props;

        return <>
            <div className="row">
                {data_list.length > 0
                    ? items
                    : <div id="notfound" className="w-100">
                        <div className="notfound">
                            <div className="notfound-404">
                                <div className="h1">No Results</div>
                            </div>
                        </div>
                    </div>}
            </div>
        </>
    }
}

export default EventSchedulesList;
