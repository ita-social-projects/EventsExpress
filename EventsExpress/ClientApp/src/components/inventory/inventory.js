import React, { Component } from 'react';
import get_unitofmeasuring from '../../actions/inventory';

class Inventory extends Component {

    constructor() {
        super();

        this.state = {
            unitsOfMeasuring: []
        };
    }

    componentDidMount() {
        get_unitofmeasuring()
        .then(response => {
            console.log(response);
            this.setState({
                unitsOfMeasuring: response
            });
        })
    }

    render() {
        return (
            <div>
                <h3>Inventory</h3>
                {this.state.unitsOfMeasuring.map(unit => {
                    return (
                        <div>
                            <h5>{unit.shortName}</h5>
                        </div>
                    )
                })}
            </div>
        )
    }
}

export default Inventory;