import React, { Component } from 'react';
import InventoryHeaderButton from './InventoryHeaderButton';
import ItemFrom from './itemForm';
import WillTakeItemForm from './willTakeItemForm';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import { update_inventories, get_inventories_by_event_id }  from '../../actions/inventory-list';
import { get_users_inventories_by_event_id }  from '../../actions/usersInventories';
import { add_item, delete_item, edit_item, want_to_take } from '../../actions/inventar';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';

class InventoryList extends Component {

    constructor() {
        super();
        this.state = {
            isOpen: true,
            disabledEdit: false,
            isWantToTake: false
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
        this.handleOnClickWantToTake = this.handleOnClickWantToTake.bind(this);
        this.markItemAsWantToTake = this.markItemAsWantToTake.bind(this);
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
        updateList.map(item => {
            if (inventar.id === item.id)
                item.isEdit = true;
        });

        this.props.get_inventories(updateList);
        this.setState({
            disabledEdit: true
        });
    }

    markItemAsWantToTake = inventar => {
        let updateList = this.props.inventories.items;
        updateList.map(item => {
            if (inventar.id === item.id)
                item.isWantToTake = !item.isWantToTake;
        });

        this.props.get_inventories(updateList);
    }

    // initialState = () => {
    //     console.log('initial state')
    //     let updateList = this.props.invetnories.items;
    //     updateList.map(item => {
    //         this.props.usersInventories.map(data => {
    //             if (this.props.user.id === data.userId && item.id === data.inventoryId)
    //                 item.isTaken = true;
    //         });
    //     });

    //     this.props.get_inventories(updateList);
    // }

    handleOnClickCaret = () => {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    handleOnClickWantToTake = () => {
        this.setState(state => ({
            isWantToTake: !state.isWantToTake
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

    onWillTake = inventar => {
        const data = {
            eventId: this.props.eventId,
            userId: this.props.user.id,
            inventoryId: inventar.id,
            quantity: inventar.quantity
        }

        this.props.want_to_take(data);

        this.setState({
            disabledEdit: false
        });
    }

    render() {
        const { inventories, event, user } = this.props;
        let isMyEvent = event.owners.find(x => x.id === user.id) != undefined;
        const reducer = (accumulator, currentValue) => accumulator + currentValue.quantity;
        let updateList = [];
        if (inventories.items) {
            updateList = inventories.items.map(item => {
                return { 
                    ...item,
                    isTaken: this.props.usersInventories.data
                        .filter(dataItem => 
                            this.props.user.id === dataItem.userId && 
                            item.id === dataItem.inventoryId)
                        .length > 0
                }                
            });
            console.log("step 1", updateList);
        }
        console.log(this.props);
        return (
            <>
                <div className="d-flex justify-content-start align-items-center">
                    <InventoryHeaderButton isOpen={this.state.isOpen} handleOnClickCaret={this.handleOnClickCaret}/>
                </div>
                
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
                                <div className="col"><b>Will take</b></div>
                                }
                                <div className="col"><b>Count</b></div>
                                <div className="col"><b>Measuring unit</b></div>
                                <div className="col col-md-2"><b>Action</b></div>
                            </div>
                            {updateList.map(item => {
                                return (
                                    item.isEdit 
                                    ? <div className="row p-1" key={item.id}>
                                        {isMyEvent
                                         ?   <ItemFrom 
                                                onSubmit={this.onSubmit} 
                                                onCancel={this.onCancel}
                                                unitOfMeasuringState={this.props.unitOfMeasuringState}
                                                initialValues={item}/>
                                        :   <WillTakeItemForm
                                                onSubmit={this.onWillTake}
                                                onCancel={this.onCancel}
                                                initialValues={item}
                                            />
                                        }
                                    </div>
                                    : <div className="row p-1" key={item.id}>
                                        <div className="col col-md-4">
                                            <span className="item" onClick={() => this.markItemAsWantToTake(item)}>{item.itemName}</span>
                                        </div>
                                        <div className="col" key={item.id}>
                                                {item.isWantToTake
                                                ? 
                                                <>  
                                                    {this.props.usersInventories.data.map(data => {
                                                        return (
                                                            data.inventoryId === item.id 
                                                            ?   <span>{data.user.name}: {data.quantity}</span>
                                                            :   null
                                                        );
                                                    })}
                                                </>
                                                : 
                                                  <>
                                                    {
                                                        this.props.usersInventories.data.length === 0 ? 
                                                            0 
                                                            : this.props.usersInventories.data.reduce((acc, cur) => {
                                                                return acc + cur.quantity
                                                            }, 0)
                                                    }
                                                  </> 
                                                }
                                        </div>
                                        {!isMyEvent &&
                                        <div className="col">0</div>
                                        }
                                        <div className="col">{item.needQuantity}</div>
                                        <div className="col">{item.unitOfMeasuring.shortName}</div>
                                        {isMyEvent
                                        ?    <div className="col">
                                                <IconButton 
                                                    disabled={this.state.disabledEdit} 
                                                    onClick={this.markItemAsEdit.bind(this, item)}>
                                                    <i className="fa-sm fas fa-pencil-alt text-warning"></i>
                                                </IconButton>
                                                <IconButton
                                                    disabled={this.state.disabledEdit} 
                                                    onClick={this.deleteItemFromList.bind(this, item)}>
                                                    <i className="fa-sm fas fa-trash text-danger"></i>
                                                </IconButton>
                                            </div>
                                        :   <div className='col col-md-2'>
                                            {/* {item.isTaken 
                                            ? <Tooltip title="Will take" placement="right-start">
                                                <IconButton
                                                    onClick={this.markItemAsEdit.bind(this, item)}>
                                                    <i className="fa-sm fas fa-plus text-success"></i>
                                                    ?
                                                </IconButton>
                                            </Tooltip>
                                            :  <Tooltip title="Will not take" placement="right-start">
                                                <IconButton
                                                    onClick={this.markItemAsEdit.bind(this, item)}>
                                                    <i className="fa-sm fas fa-minus text-danger"></i>
                                                    !
                                                </IconButton>
                                            </Tooltip>
                                            }  */}
                                            {item.isTaken && 
                                            <Tooltip title="Will not take" placement="right-start">
                                                <IconButton
                                                    onClick={this.markItemAsEdit.bind(this, item)}>
                                                    <i className="fa-sm fas fa-minus text-danger"></i>
                                                </IconButton>
                                            </Tooltip>
                                            }

                                            {!item.isTaken && 
                                            <Tooltip title="Will take" placement="right-start">
                                                <IconButton
                                                    onClick={this.markItemAsEdit.bind(this, item)}>
                                                    <i className="fa-sm fas fa-plus text-success"></i>
                                                </IconButton>
                                            </Tooltip>
                                            }
                                            </div>
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
        want_to_take: (data) => dispatch(want_to_take(data))
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InventoryList);