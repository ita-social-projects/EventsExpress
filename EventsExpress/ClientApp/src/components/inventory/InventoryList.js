import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import IconButton from "@material-ui/core/IconButton";
import { get_inventories_by_event_id }  from '../../actions/inventory-list';
import InventoryItemWrapper from '../../containers/inventory-item';
import { update_inventories }  from '../../actions/inventory-list';
import { get_users_inventories_by_event_id, edit_users_inventory }  from '../../actions/usersInventories';
import { add_item, edit_item } from '../../actions/inventar';

class InventoryList extends Component {

    constructor() {
        super();
        this.state = {
            isOpen: true,
            disabledEdit: false,
            showAlreadyGetDetailed: false
        };

        this.onSubmit = this.onSubmit.bind(this);
        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    componentDidMount() {
        this.props.get_unitsOfMeasuring();
        this.props.get_inventories_by_event_id(this.props.eventId);
        this.props.get_users_inventories_by_event_id(this.props.eventId);
    }
 
    addItemToList = () => {
        const updateList = [...this.props.inventories.items, {
            id: '',
            itemName: '',
            needQuantity: 0,
            unitOfMeasuring: {},
            isEdit: true,
            isNew: true
        }];
        
        this.props.get_inventories(updateList);
        this.setState({
            disabledEdit: true
        });
    }

    handleOnClickCaret = () => {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    changeDisableEdit = (value) => {
        this.setState({
            disabledEdit: value
        });
    }

    onSubmit = values => {
        console.log('submit', values);

        if (values.isNew) {
            this.props.add_item(values, this.props.eventId);
        }
        else {
            values.unitOfMeasuring = values.unitOfMeasuring.id;
            this.props.edit_item(values, this.props.eventId);
        }

        this.setState({
            disabledEdit: false
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
                            <span class="icon"><i class="fa-sm fas fa-plus"></i></span> &nbsp; Add item 
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
                            {updateList.map((item, key) => {
                                return (
                                    <InventoryItemWrapper
                                        item={item}
                                        user={user}
                                        usersInventories={usersInventories}
                                        inventories={inventories}
                                        isMyEvent={isMyEvent}
                                        disabledEdit={this.state.disabledEdit}
                                        onSubmit={this.onSubmit}
                                        changeDisableEdit={this.changeDisableEdit}
                                        get_inventories={this.props.get_inventories}
                                        event={event}
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
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring()),
        add_item: (item, eventId) => dispatch(add_item(item, eventId)),
        edit_item: (item, eventId) => dispatch(edit_item(item, eventId)),
        get_inventories: (inventories) => dispatch(update_inventories(inventories)),
        get_users_inventories_by_event_id: (eventId) => dispatch(get_users_inventories_by_event_id(eventId)),
        get_inventories_by_event_id: (eventId) => dispatch(get_inventories_by_event_id(eventId)),
        edit_users_inventory: (data) => dispatch(edit_users_inventory(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InventoryList);