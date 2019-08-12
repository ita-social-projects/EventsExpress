import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event-list';
import BadRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
import Unauthorized from '../components/Route guard/401'
import Forbidden from '../components/Route guard/403'
import { Redirect } from 'react-router'
import history from '../history';


class EventListWrapper extends Component{
    componentDidUpdate(prevProps, prevState) {
        if (this.props.isError.ErrorCode=='500') {
            this.getEvents(this.props.params);
        }
    }
    componentDidMount() {
        this.getEvents(this.props.params);
    }
    getEvents = (page) => this.props.get_events(page);
    

    render() {

        const { data, isPending, isError } = this.props;
        const { items } = this.props.data;
        const errorMessage = isError.ErrorCode == '403' ? <Forbidden /> : isError.ErrorCode == '500' ? <Redirect from="*" to="/home/events?page=1"/>: isError.ErrorCode == '401' ? <Unauthorized /> : isError.ErrorCode == '400' ? <BadRequest /> : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage ? <EventList  data_list={items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getEvents} /> : null;
       
        return <>
            {errorMessage}
                {spinner}
                {content}
               </>
    }
}

const mapStateToProps = (state) => (state.events);

const mapDispatchToProps = (dispatch) => { 
    return {
        get_events: (page) => dispatch(get_events(page))
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(EventListWrapper);