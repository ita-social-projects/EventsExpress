import React, { Component } from 'react';
import { connect } from 'react-redux';
import OwnerSeeItem from '../components/inventory/ownerSeeItem';
import OwnerEditItemForm from '../components/inventory/ownerEditItem';
import VisitorSeeItem from '../components/inventory/visitorSeeItem';
import VisitorEditItemForm from '../components/inventory/visitorTakeItem';
import { get_inventories_by_event_id }  from '../actions/inventory-list';
import { delete_users_inventory, edit_users_inventory }  from '../actions/usersInventories';
import { delete_item, edit_item, add_item, want_to_take } from '../actions/inventar';

class InventoryItemWrapper extends Component {

    constructor() {
        super();
        this.state = {
            showAlreadyGetDetailed: false
        };

        this.onAlreadyGet = this.onAlreadyGet.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.onCancel = this.onCancel.bind(this);
        this.onWillTake = this.onWillTake.bind(this);
    }

    onAlreadyGet = () => {
        this.setState(state => ({
            showAlreadyGetDetailed: !state.showAlreadyGetDetailed
        }));
    }

    
    deleteItemFromList = inventar => {
        this.props.delete_item(inventar.id, this.props.eventId);
    }

    markItemAsEdit = inventar => {
        let updateList = this.props.inventories.items;
        updateList.find(e => e.id === inventar.id).isEdit = true;

        this.props.get_inventories(updateList);
        this.props.changeDisableEdit(true);
    }

    markItemAsWillTake = inventar => {
        let updateList = this.props.inventories.items;
        updateList.find(e => e.id === inventar.id).isWillTake = true;
        updateList.find(e => e.id === inventar.id).isEdit = true;

        this.props.get_inventories(updateList);
        this.props.changeDisableEdit(true);
    }
    
    onSubmit = values => {
        if (values.isNew) {
            this.props.add_item(values, this.props.eventId);
        }
        else {
            values.unitOfMeasuring = {
                id: values.unitOfMeasuring.id
            };
            this.props.edit_item(values, this.props.eventId);
        }

        this.props.changeDisableEdit(false);
    }

    onCancel = inventar => {
        if (inventar.isNew) {
            this.deleteItemFromList(inventar);
            this.props.changeDisableEdit(false);
            return;
        }

        this.props.get_inventories_by_event_id(this.props.eventId);
        this.props.changeDisableEdit(false);
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

        this.props.changeDisableEdit(false);
    }

    onWillNotTake = inventar => {
        const data = {
            eventId: this.props.eventId,
            userId: this.props.user.id,
            inventoryId: inventar.id
        }

        this.props.delete_users_inventory(data);
    }

    render() {
        const { item, user, usersInventories, isMyEvent, disabledEdit } = this.props;
        const alreadyGet = usersInventories.data.reduce((acc, cur) => {
            return cur.inventoryId === item.id ? acc + cur.quantity : acc + 0
        }, 0);
        return(
            <div className="row p-1 d-flex align-items-center" key={item.id}>
            {item.isEdit && isMyEvent && 
                <OwnerEditItemForm
                    onSubmit={this.onSubmit} 
                    onCancel={this.onCancel}
                    unitOfMeasuringState={this.props.unitOfMeasuringState}
                    alreadyGet={alreadyGet}
                    initialValues={item}/>
            }

            {item.isEdit && !isMyEvent &&
                <VisitorEditItemForm
                    onSubmit={this.onWillTake}
                    onCancel={this.onCancel}
                    alreadyGet={alreadyGet - (item.isWillTake ? 0 : usersInventories.data.find(e => e.inventoryId === item.id)?.quantity || 0)}
                    initialValues={item}
                />
            }

            {!item.isEdit && isMyEvent &&
                <OwnerSeeItem
                    item={item}
                    disabledEdit={disabledEdit}
                    showAlreadyGetDetailed={this.state.showAlreadyGetDetailed}
                    onAlreadyGet={this.onAlreadyGet}
                    markItemAsEdit={this.markItemAsEdit}
                    deleteItemFromList={this.deleteItemFromList}
                    usersInventories={this.props.usersInventories}/>
            }

            {!item.isEdit && !isMyEvent && 
                <VisitorSeeItem 
                    item={item}
                    disabledEdit={disabledEdit}
                    showAlreadyGetDetailed={this.state.showAlreadyGetDetailed}
                    alreadyGet={alreadyGet}
                    onAlreadyGet={this.onAlreadyGet}
                    onWillNotTake={this.onWillNotTake}
                    markItemAsEdit={this.markItemAsEdit}
                    markItemAsWillTake={this.markItemAsWillTake}
                    usersInventories={this.props.usersInventories}
                    user={user}/>
            }
        </div>
        );
    }
}

const mapStateToProps = (state) => ({
    unitOfMeasuringState: state.unitsOfMeasuring
});

const mapDispatchToProps = (dispatch) => {
    return {
        delete_item: (itemId, eventId) => dispatch(delete_item(itemId, eventId)),
        edit_item: (item, eventId) => dispatch(edit_item(item, eventId)),
        add_item: (item, eventId) => dispatch(add_item(item, eventId)),
        get_inventories_by_event_id: (eventId) => dispatch(get_inventories_by_event_id(eventId)),
        delete_users_inventory: (data) => dispatch(delete_users_inventory(data)),
        edit_users_inventory: (data) => dispatch(edit_users_inventory(data)),
        want_to_take: (data) => dispatch(want_to_take(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InventoryItemWrapper);