import React, {Component} from 'react';
import { connect } from 'react-redux';
import { set_rating, get_currrent_rating, get_average_rating } from'../actions/rating'
import RatingAverage from '../components/rating/rating-average'
import RatingSetter from '../components/rating/rating-setter'

class RatingWrapper extends Component{
    componentDidMount = () => {
        
        this.props.getMyRate();
        this.props.getAverageRate();
        
    }

    onRateChange = event => {
        let rate = event.currentTarget.value;
        
        this.props.setRate(rate); 
        setTimeout(this.props.getAverageRate, 125);
    }

    render() {        
        return <div className='d-flex flex-row align-items-center justify-content-between'>
                    {this.props.iWillVisitIt  
                        ? <RatingSetter myRate={this.props.myRate} callback={this.onRateChange} /> 
                        : <div></div>
                    }
                    
                    <RatingAverage value={this.props.averageRate} />
                    
                                                       
            </div>
    }
}


const mapStateToProps = (state) => ({
    myRate: state.event.myRate, 
    averageRate: state.event.averageRate
});


const mapDispatchToProps = (dispatch, props) => { 
    return {
        setRate: value => dispatch(set_rating({
                eventId: props.eventId, 
                userId: props.userId,
                rate: value
        })),       
        getMyRate: () => dispatch(get_currrent_rating(props.eventId)),
        getAverageRate: () => dispatch(get_average_rating(props.eventId)),
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(RatingWrapper);