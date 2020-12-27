import React, { Component } from 'react';
//import CategoryAddWrapper from '../../containers/categories/category-add';
//import CategoryListWrapper from '../../containers/categories/category-list';
import Spinner from '../spinner';
import UnitOfMeasuringListWrapper  from '../../containers/unitsOfMeasuring/unitOfMeasuring-list';
import { connect } from 'react-redux';
import get_unitsOfMeasuring from '../../actions/unitsOfMeasuring';

class UnitsOfMeasuring extends Component {

    componentWillMount = () => this.props.get_unitsOfMeasuring();

    render() {
        console.log('unit of measuring', this.props)
        const { unitsOfMeasuring } = this.props;
        return (
            <>
            <div>
                <table className="table w-75 m-auto">
                    <tbody>                        
                        {/* {!unitsOfMeasuring.isPending ? <UnitOfMeasuringListWrapper data={unitsOfMeasuring.units} /> : null } */}
                        {<UnitOfMeasuringListWrapper data={unitsOfMeasuring.units}/>}
                    </tbody>
                </table> 
                {/* {unitsOfMeasuring.isPending ? <Spinner/> : null} */}
            </div>
                
            </>
        );

    }
}

const mapStateToProps = (state) => ({
    unitsOfMeasuring: state.unitsOfMeasuring
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring())
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UnitsOfMeasuring);
//





