import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';
import OwnerEditItemForm from './ownerEditItem';
import VisitorEditItemForm from './visitorTakeItem';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import { update_inventories, get_inventories_by_event_id }  from '../../actions/inventory-list';
import { get_users_inventories_by_event_id, delete_users_inventory, edit_users_inventory }  from '../../actions/usersInventories';
import { add_item, delete_item, edit_item, want_to_take } from '../../actions/inventar';
import IconButton from "@material-ui/core/IconButton";
import OwnerSeeItem from './ownerSeeItem';
import VisitorSeeItem from './VisitorSeeItem';

class InventoryList extends Component {

    constructor() {
        super();
        this.state = {
            isOpen: true,
            disabledEdit: false,
            showAlreadyGetDetailed: false
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
        this.handleOnClickWantToTake = this.handleOnClickWantToTake.bind(this);
        this.onAlreadyGet = this.onAlreadyGet.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.onCancel = this.onCancel.bind(this);
        this.onWillTake = this.onWillTake.bind(this);
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

    deleteItemFromList = inventar => {
        this.props.delete_item(inventar.id, this.props.eventId);
    }

    markItemAsEdit = inventar => {
        let updateList = this.props.inventories.items;
        updateList.find(e => e.id === inventar.id).isEdit = true;

        this.props.get_inventories(updateList);
        this.setState({
            disabledEdit: true
        });
    }

    markItemAsWillTake = inventar => {
        let updateList = this.props.inventories.items;
        updateList.find(e => e.id === inventar.id).isWillTake = true;
        updateList.find(e => e.id === inventar.id).isEdit = true;

        this.props.get_inventories(updateList);
        this.setState({
            disabledEdit: true
        });
    }

    onAlreadyGet = inventar => {
        let updateList = this.props.inventories.items;
        updateList.map(item => {
            if (inventar.id === item.id)
                item.showAlreadyGetDetailed = !item.showAlreadyGetDetailed;
        });

        this.props.get_inventories(updateList);
    }

    handleOnClickCaret = () => {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    handleOnClickWantToTake = () => {
        this.setState(state => ({
            showAlreadyGetDetailed: !state.showAlreadyGetDetailed
        }));
    }

    onSubmit = values => {
        if (values.isNew) {
            this.props.add_item(values, this.props.eventId);
        }
        else {
            console.log('submit', values);
            values.unitOfMeasuring = values.unitOfMeasuring.id;
            this.props.edit_item(values, this.props.eventId);
        }

        this.setState({
            disabledEdit: false
        });
    }

    onCancel = inventar => {
        if (inventar.isNew) {
            this.deleteItemFromList(inventar);
            this.setState({
                disabledEdit: false
            })
            return;
        }

        this.props.get_inventories_by_event_id(this.props.eventId);

        this.setState({
            disabledEdit: false
        });
    }

    onWillTake = inventar => {
        const data = {
            eventId: this.props.eventId,
            userId: this.props.user.id,
            inventoryId: inventar.id,
            quantity: Number(inventar.willTake)
        }

        if (inventar.isWillTake) {
            this.props.want_to_take(data);
        } else {
            this.props.edit_users_inventory(data);
        }

        this.setState({
            disabledEdit: false
        });
    }

    onWillNotTake = inventar => {
        const data = {
            eventId: this.props.eventId,
            userId: this.props.user.id,
            inventoryId: inventar.id
        }

        this.props.delete_users_inventory(data);
    }

    renderInventoryItem = item => {
        const { user, usersInventories } = this.props;
        let isMyEvent = this.props.event.owners.find(x => x.id === user.id) != undefined;
        return (
            <div className="row p-1 d-flex align-items-center" key={item.id}>
                {item.isEdit && isMyEvent && 
                    <OwnerEditItemForm
                        onSubmit={this.onSubmit} 
                        onCancel={this.onCancel}
                        unitOfMeasuringState={this.props.unitOfMeasuringState}
                        alreadyGet={usersInventories.data.reduce((acc, cur) => {
                            return cur.inventoryId === item.id ? acc + cur.quantity : acc + 0
                        }, 0)}
                        initialValues={item}/>
                }

                {item.isEdit && !isMyEvent &&
                    <VisitorEditItemForm
                        onSubmit={this.onWillTake}
                        onCancel={this.onCancel}
                        alreadyGet={usersInventories.data.reduce((acc, cur) => {
                            return cur.inventoryId === item.id ? acc + cur.quantity : acc + 0
                        }, 0) - (item.isWillTake ? 0 : usersInventories.data.find(e => e.inventoryId === item.id)?.quantity || 0)}
                        initialValues={item}
                    />
                }

                {!item.isEdit && isMyEvent &&
                    <OwnerSeeItem
                        item={item}
                        disabledEdit={this.state.disabledEdit}
                        onAlreadyGet={this.onAlreadyGet}
                        markItemAsEdit={this.markItemAsEdit}
                        deleteItemFromList={this.deleteItemFromList}
                        usersInventories={this.props.usersInventories}/>
                }

                {!item.isEdit && !isMyEvent && 
                    <VisitorSeeItem 
                        item={item}
                        disabledEdit={this.state.disabledEdit}
                        onWillNotTake={this.onWillNotTake}
                        markItemAsEdit={this.markItemAsEdit}
                        markItemAsWillTake={this.markItemAsWillTake}
                        usersInventories={this.props.usersInventories}
                        user={this.props.user}/>
                }
            </div>
        )
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
                            this.props.user.id === dataItem.userId && 
                            item.id === dataItem.inventoryId)
                        .length > 0
                }                
            });
        }
        console.log(this.props);
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
                            {updateList.map(item => this.renderInventoryItem(item))}                    
                        </div>
                </div>
                }
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    unitOfMeasuringState: state.unitsOfMeasuring,
    event: state.event.data,
    user: state.user,
    inventories: state.inventories,
    usersInventories: state.usersInventories
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring()),
        add_item: (item, eventId) => dispatch(add_item(item, eventId)),
        delete_item: (itemId, eventId) => dispatch(delete_item(itemId, eventId)),
        edit_item: (item, eventId) => dispatch(edit_item(item, eventId)),
        get_inventories: (inventories) => dispatch(update_inventories(inventories)),
        get_inventories_by_event_id: (eventId) => dispatch(get_inventories_by_event_id(eventId)),
        get_users_inventories_by_event_id: (eventId) => dispatch(get_users_inventories_by_event_id(eventId)),
        delete_users_inventory: (data) => dispatch(delete_users_inventory(data)),
        edit_users_inventory: (data) => dispatch(edit_users_inventory(data)),
        want_to_take: (data) => dispatch(want_to_take(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InventoryList);