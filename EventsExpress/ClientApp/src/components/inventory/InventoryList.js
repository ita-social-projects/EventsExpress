import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';
import ItemFrom from './itemForm';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import { update_inventories, get_inventories_by_event_id }  from '../../actions/inventory-list';
import { add_item, delete_item, edit_item } from '../../actions/inventar';
import IconButton from "@material-ui/core/IconButton";

class InventoryList extends Component {

    constructor() {
        super();
        this.state = {
            isOpen: true,
            disabledEdit: false
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.onCancel = this.onCancel.bind(this);
    }
    
    componentDidMount() {
        this.props.get_unitsOfMeasuring();
        this.props.get_inventories_by_event_id(this.props.eventId);
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
        updateList.map(item => {
            if (inventar.id === item.id)
                item.isEdit = true;
        });

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

    onSubmit = values => {
        if (values.isNew) {
            this.props.add_item(values, this.props.eventId);
        }
        else {
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

    render() {
        const { inventories, event, user } = this.props;
        return (
            <>
                <div className="d-flex justify-content-start align-items-center">
                    <InventoryHeaderButton isOpen={this.state.isOpen} handleOnClickCaret={this.handleOnClickCaret}/>
                </div>
                
                { this.state.isOpen &&
                <div>
                    {event.user.id === user.id 
                    ?  <IconButton
                            disabled = {this.state.disabledEdit}
                            onClick = {this.addItemToList.bind(this)}
                            size = "small">
                            <span class="icon"><i class="fa-sm fas fa-plus"></i></span> &nbsp; Add item 
                        </IconButton>
                    :   null
                    }
                        <div className="container">
                            <div className="row">
                                <div className="col col-md-5"><b>Item name</b></div>
                                <div className="col"><b>Count</b></div>
                                <div className="col"><b>Measuring unit</b></div>
                                {event.user.id === user.id
                                ? <div className="col"><b>Action</b></div>
                                : null
                                }
                                
                            </div>
                            {inventories.items.map(item => {
                                return (
                                    item.isEdit 
                                    ? <div className="row">
                                        <ItemFrom 
                                            onSubmit={this.onSubmit} 
                                            onCancel={this.onCancel}
                                            unitOfMeasuringState={this.props.unitOfMeasuringState}
                                            initialValues={item}/>
                                    </div>
                                    : <div className="row">
                                        <div className="col col-md-5">{item.itemName}</div>
                                        <div className="col">{item.needQuantity}</div>
                                        <div className="col">{item.unitOfMeasuring.shortName}</div>
                                        {event.user.id === user.id
                                        ? <div className="col">
                                                <IconButton 
                                                    disabled = {this.state.disabledEdit} 
                                                    onClick = {this.markItemAsEdit.bind(this, item)}>
                                                    <i class = "fa-sm fas fa-pencil-alt text-warning"></i>
                                                </IconButton>
                                                <IconButton
                                                    disabled = {this.state.disabledEdit} 
                                                    onClick = {this.deleteItemFromList.bind(this, item)}>
                                                    <i className = "fa-sm fas fa-trash text-danger"></i>
                                                </IconButton>
                                            </div>
                                        : null
                                        }   
                                    </div>
                                )
                            })}                    
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
    inventories: state.inventories
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring()),
        add_item: (item, eventId) => dispatch(add_item(item, eventId)),
        delete_item: (itemId, eventId) => dispatch(delete_item(itemId, eventId)),
        edit_item: (item, eventId) => dispatch(edit_item(item, eventId)),
        get_inventories: (inventories) => dispatch(update_inventories(inventories)),
        get_inventories_by_event_id: (eventId) => dispatch(get_inventories_by_event_id(eventId))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InventoryList);