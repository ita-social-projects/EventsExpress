import React, { Component } from 'react';
import UnitOfMeasuringAddWrapper from '../../containers/unitsOfMeasuring/unitOfMeasuring-add';
import Spinner from '../spinner';
import UnitOfMeasuringListWrapper from '../../containers/unitsOfMeasuring/UnitOfMeasuringListWrapper';
import { connect } from 'react-redux';
import get_unitsOfMeasuring from '../../actions/unitOfMeasuring/unitsOfMeasuring-list-action';

class UnitsOfMeasuring extends Component {

    componentWillMount = () => this.props.get_unitsOfMeasuring();

    render() {
        const { unitsOfMeasuring } = this.props;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                    <UnitOfMeasuringAddWrapper
                        item={{ id: "00000000-0000-0000-0000-000000000000", unitName: "", shortName: "", category: "" }}
                    />
                    <Spinner showContent={unitsOfMeasuring != undefined}>
                        <UnitOfMeasuringListWrapper data={unitsOfMeasuring} />
                    </Spinner>
                </tbody>
            </table>
        </div>
    }
}

const mapStateToProps = (state) => ({
    unitsOfMeasuring: state.unitsOfMeasuring.units
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring())
    }
};


export default connect(mapStateToProps, mapDispatchToProps)(UnitsOfMeasuring)






