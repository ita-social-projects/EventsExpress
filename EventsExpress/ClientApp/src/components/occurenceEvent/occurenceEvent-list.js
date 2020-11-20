import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reset_occurenceEvents } from '../../actions/occurenceEvent-list';
import OccurenceEvent from './occurenceEvent-item';

const limit = 2;
const pageCount = 3;

class OccurenceEventList extends Component {

    renderItems = arr =>
        arr.map(item => (
            <OccurenceEvent
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

export default OccurenceEventList;
