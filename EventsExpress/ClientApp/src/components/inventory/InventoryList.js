import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';

class InventoryList extends Component {

    constructor() {
        super();

        this.state = {
            isOpen: true,
            inventoryList: []
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    componentDidMount() {
        this.setState({
            inventoryList: this.props.inventories
        })
    }

    addItemToList() {
        const undateList = [...this.state.inventoryList, {
            itemName: '',
            needQuantity: 0,
            unitOfMeasuring: ''
        }];

        this.setState({
            inventoryList: undateList
        });
    }

    deleteItemFromList = inventar => {
        const undateList = this.state.inventoryList.filter(function(item){
            return item.id !== inventar.id;
        });
  
        this.setState({
            inventoryList: undateList
        });
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
                <div>
                        <div className="">
                            <button type="button" onClick={this.addItemToList.bind(this)} title="Remove item" class="btn btn-secondary btn-icon p-2" >
                                <span class="icon"><i class="fas fa-plus"></i></span> Add item
                            </button>
                        </div>
                    <div className="table-responsive">
                        <div className="table-wrapper">
                            <table className="table">
                                <thead>
                                    <tr>
                                        <th>Item name</th>
                                        <th>Count</th>
                                        <th>Measuring unit</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                {this.state.inventoryList.map(item => {
                                    return (
                                        <tr>
                                            <td>{item.itemName}</td>
                                            <td>{item.needQuantity}</td>
                                            <td>{item.unitOfMeasuring.shortName}</td>
                                            <td className="d-flex justify-content-end align-items-center">
                                                <button type="button" title="Remove item" class="btn clear-backgroud">
                                                    <i class="fas fa-pencil-alt orange"></i>
                                                </button>
                                                <button type="button" onClick={this.deleteItemFromList.bind(this, item)} title="Remove item" class="btn clear-backgroud">
                                                    <i class="fas fa-trash red"></i>
                                                </button>
                                            </td>
                                        </tr>
                                    )
                                })}
                                                    
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                }
            </>
        );
    }
}

export default InventoryList;