import React, { Component } from "react";
import { connect } from 'react-redux';
import { add_unitOfMeasuring } from '../../actions/unitOfMeasuring/add-unitOfMeasuring';

class EditUnit extends Component {
    state = ({
        id: this.props.item.id,
        unitName: this.props.item.unitName,
        shortName: this.props.item.shortName
    });

    setUnitName = (e) => {
        this.setState({
            unitName: e.target.value
        })
    }

    setShortName = (e) => {
        this.setState({
            shortName: e.target.value
        })
    }

    saveChange = () => {
        const data = {
            id: this.state.id,
            unitName: this.state.unitName,
            shortName: this.state.shortName
        };
        this.props.add_unitOfMeasuring(data);
    }

    render() {
        return (
            <>
                <input onChange={this.setUnitName} value={this.state.unitName} />
                <input onChange={this.setShortName} value={this.state.shortName} />
                <button onClick={this.saveChange}>Save change</button>
            </>
        )
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        add_unitOfMeasuring: (data) => dispatch(add_unitOfMeasuring(data))
    }
};

export default connect(
    null,
    mapDispatchToProps
)(EditUnit);