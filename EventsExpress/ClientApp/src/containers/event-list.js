import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import get_events from '../actions/event-list';


class EventListWrapper extends Component{

    componentDidMount() {
        const { page } = this.props.match.params;
        this.getEvents(page);
    }
    getEvents = (page) => this.props.get_events(page);
    

    render() {

        const { data, isPending, isError } = this.props;
        const { events } = this.props.data;
        // const hasData = !(isPending || isError);

        // const errorMessage = isError ? <ErrorIndicator/> : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventList data_list={events} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getEvents} /> : null;
       
        return <>
              
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