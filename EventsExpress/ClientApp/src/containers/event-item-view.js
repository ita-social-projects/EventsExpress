import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventItemView from '../components/event/event-item-view';
import Spinner from '../components/spinner';
import get_event from '../actions/event-item-view';


class EventItemViewWrapper extends Component{
    
    componentDidMount(){    
        const { id } = this.props.match.params;
        this.props.get_event(id);
    }

    render(){   
    
        const {data, isPending, isError} = this.props;
        // const hasData = !(isPending || isError);

        // const errorMessage = isError ? <ErrorIndicator/> : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventItemView data={data}  /> : null;
    
        return <>
                {spinner}
                {content}
               </>
    }
}

const mapStateToProps = (state) => (state.event);

const mapDispatchToProps = (dispatch) => { 
    return {
        get_event: (id) => dispatch(get_event(id))
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(EventItemViewWrapper);