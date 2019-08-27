import React, {Component} from 'react';
import { connect } from 'react-redux';
import Rating from '@material-ui/lab/Rating';
import { set_rating, get_currrent_rating } from'../actions/rating'


class RatingWrapper extends Component{
    componentDidMount = () => {
        
        this.props.getMyRate();
        this.props.getAverageRate();
        
    }

    onRateChange = event => {
        let rate = event.currentTarget.value;
        
        this.props.setRate(rate);        

    }

    render() {        
        return <>
                    Rate it: 
                    <Rating 
                        value={Number(this.props.myRate)}
                        max={10}
                        size="large"
                        onChange={this.onRateChange}
                    />
                    Average rating: {this.props.averageRate}
               </>
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
        getAverageRate: () => {}
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(RatingWrapper);