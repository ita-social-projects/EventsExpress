import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';
import { connect } from 'react-redux';
import IconButton from "@material-ui/core/IconButton";
import { getInventoryData } from '../../actions/inventory/inventory-list-action';
import InventoryItemWrapper from '../../containers/inventory-item';
import { edit_users_inventory } from '../../actions/users/users-inventories-action';

class InventoryList extends Component {

    constructor() {
        super();
        this.state = {
            isOpen: true,
            disabledEdit: false,
            isNew: false
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    addItemToList = () => {        
        this.setState({
            disabledEdit: true,
            isNew: true
        });
    }

    handleOnClickCaret = () => {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    changeDisableEdit = (value) => {
        if (!value) {
            this.setState({
                isNew: false
            });
        }

        this.setState({
            disabledEdit: value
        });
    }

    render() {
        const { inventories, event, user, usersInventories } = this.props;
        let isMyEvent = event.owners.find(x => x.id === user.id) != undefined;
        let updateList = [];
        if (inventories.items) {
            updateList = inventories.items.map(item => {
                return { 
                    ...item,
                    isTaken: usersInventories.data
                        .filter(dataItem => 
                            user.id === dataItem.userId && 
                            item.id === dataItem.inventoryId)
                        .length > 0
                }                
            });
        }
        return (
            <>
                
                <InventoryHeaderButton 
                    isOpen={this.state.isOpen} 
                    handleOnClickCaret={this.handleOnClickCaret}
                />
                
                
                { this.state.isOpen &&
                <div>
                    {isMyEvent &&
                        <IconButton
                            disabled = {this.state.disabledEdit}
                            onClick = {this.addItemToList.bind(this)}
                            size = "small">
                            <span className="icon"><i className="fa-sm fas fa-plus"></i></span> &nbsp; Add item 
                        </IconButton>
                    }
                        <div className="container">
                            <div className="row p-1">
                                <div className="col col-md-4"><b>Item name</b></div>
                                <div className="col"><b>Already get</b></div>
                                {!isMyEvent &&
                                <div className="col col-md-2"><b>Will take</b></div>
                                }
                                <div className="col col-md-1"><b>Count</b></div>
                                <div className="col col-md-1"></div>
                                <div className="col col-md-2"></div>
                            </div>
                            {this.state.isNew &&
                                <InventoryItemWrapper
                                    item={{
                                        itemName: '',
                                        needQuantity: 0,
                                        unitOfMeasuring: {}
                                    }}
                                    user={user}
                                    usersInventories={usersInventories}
                                    inventories={inventories}
                                    isMyEvent={isMyEvent}
                                    disabledEdit={this.state.disabledEdit}
                                    changeDisableEdit={this.changeDisableEdit}
                                    get_inventories={this.props.get_inventories}
                                    eventId={this.props.eventId}
                                    isNew
                                />
                            }
                            {updateList.map((item, key) => {
                                return (
                                    <InventoryItemWrapper
                                        item={item}
                                        user={user}
                                        usersInventories={usersInventories}
                                        inventories={inventories}
                                        isMyEvent={isMyEvent}
                                        disabledEdit={this.state.disabledEdit}
                                        changeDisableEdit={this.changeDisableEdit}
                                        get_inventories={this.props.get_inventories}
                                        eventId={this.props.eventId}
                                        key={key}
                                    />
                                );
                            })}                    
                        </div>
                </div>
                }
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    event: state.event.data,
    user: state.user,
    inventories: state.inventories,
    usersInventories: state.usersInventories
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_inventories: (inventories) => dispatch(getInventoryData(inventories)),
        edit_users_inventory: (data) => dispatch(edit_users_inventory(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InventoryList);