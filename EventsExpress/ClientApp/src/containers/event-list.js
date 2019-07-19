import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import get_events from '../actions/event-list';


class EventListWrapper extends Component{

    componentDidMount = () => this.props.get_events();

    render(){   
    
        const {data, isPending, isError} = this.props;
        // const hasData = !(isPending || isError);

        // const errorMessage = isError ? <ErrorIndicator/> : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventList data_list={data} /> : null;
    
        return <>
                {spinner}
                {content}
               </>
    }
}

const mapStateToProps = (state) => (state.events);

const mapDispatchToProps = (dispatch) => { 
    return {
        get_events: () => dispatch(get_events())
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(EventListWrapper);