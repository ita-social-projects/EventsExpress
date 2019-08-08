import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import get_events from '../actions/event-list';
import BagRequest from '../components/Route guard/400'

class EventListWrapper extends Component{

    componentDidMount() {
        this.getEvents(this.props.params);
    }
    getEvents = (page) => this.props.get_events(page);
    

    render() {

        const { data, isPending, isError } = this.props;
        const { items } = this.props.data;
        //const hasData = !(isPending || isError);
      //  let errorM;
        const errorMessage = isError ? <BagRequest /> : null;
     //   switch (errorMessage)
      // {
          //   case "400": {
           //      errorM = <BagRequest />
            //     return errorM;
           //  }
          //   default: return errorM;
     //   }
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
        get_events: (page) => dispatch(get_events(page))
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(EventListWrapper);