import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';

class InventoryList extends Component {

    constructor() {
        super();

        this.state = {
            isOpen: true
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    handleOnClickCaret() {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    render() {
        const { inventories } = this.props;
        return (
            <>
                <div className="d-flex justify-content-start align-items-center">
                    <InventoryHeaderButton isOpen={this.state.isOpen} handleOnClickCaret={this.handleOnClickCaret}/>
                </div>
                { this.state.isOpen &&
                    <div className="table-responsive">
                        <div className="table-wrapper">
                            <table className="table">
                                <thead>
                                    <tr>
                                        <th>Item name</th>
                                        <th>Count</th>
                                        <th>Measuring unit</th>
                                    </tr>
                                </thead>
                                <tbody>
                                {inventories.map(item => {
                                    return (
                                        <tr>
                                            <td>{item.itemName}</td>
                                            <td>{item.needQuantity}</td>
                                            <td>{item.unitOfMeasuring.shortName}</td>
                                        </tr>
                                    )
                                })}
                                                    
                                </tbody>
                            </table>
                        </div>
                    </div>
                }
            </>
        );
    }
}

export default InventoryList;