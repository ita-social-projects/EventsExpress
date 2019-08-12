import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import get_events from '../actions/event-list';
import BagRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'

class EventListWrapper extends Component{

    componentDidMount() {
        this.getEvents(this.props.params);
    }

    componentWillUpdate = (newProps) => {
        if(newProps.params !== this.props.params)
            this.getEvents(newProps.params);
    }
    

    getEvents = (filter) => this.props.get_events(filter);
    

    render() {
        const { data, isPending, isError } = this.props;
        const { items } = this.props.data;
     
        const errorMessage = isError.ErrorCode == '400' ? <BagRequest /> : isError.ErrorCode == '500' ? <InternalServerError /> : null;
    
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventList  data_list={items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getEvents} /> : null;
       
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
        get_events: (filter) => dispatch(get_events(filter))
    } 
};

    export default connect(mapStateToProps, mapDispatchToProps)(EventListWrapper);