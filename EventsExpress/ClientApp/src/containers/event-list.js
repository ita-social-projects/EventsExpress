import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import get_events from '../actions/event-list';


class EventListWrapper extends Component{

    componentDidMount() {
        console.log('bbb', this.props.params);
        this.getEvents(this.props.params);
    }
    getEvents = (page) => this.props.get_events(page);
    

    render() {
        console.log('ccc', this.props.params);
        const { data, isPending, isError } = this.props;
        const { items } = this.props.data;
        // const hasData = !(isPending || isError);

        // const errorMessage = isError ? <ErrorIndicator/> : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventList  data_list={items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getEvents} /> : null;
       
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