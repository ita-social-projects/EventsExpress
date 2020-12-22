import React, { Component } from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';

export default class VisitorSeeItem extends Component {

    constructor() {
        super()
    }

    render() {
        const { item, disabledEdit, onWillNotTake, markItemAsEdit, markItemAsWillTake, usersInventories, user } = this.props;
        return (
            <>
                {!item.isEdit &&
                    <>
                        <div className="col col-md-4 d-flex align-items-center">
                            <span className="item" onClick={() => this.onAlreadyGet(item)}>{item.itemName}</span>
                        </div>
                        <div className="col d-flex align-items-center" key={item.id}>
                                {item.showAlreadyGetDetailed &&
                                    usersInventories.data.map((data, key) => {
                                        return (
                                            data.inventoryId === item.id 
                                            ?   <div key={key}>{data.user.name}: {data.quantity};</div>
                                            :   null
                                        );
                                    })
                                }

                                {!item.showAlreadyGetDetailed && 
                                    <>
                                        {usersInventories.data.length === 0 ? 
                                                0 
                                                : usersInventories.data.reduce((acc, cur) => {
                                                    return cur.inventoryId === item.id ? acc + cur.quantity : acc + 0
                                                }, 0)
                                        }
                                    </>
                                }
                        </div>
                        <div className="col col-md-2 d-flex align-items-center">
                            {usersInventories.data.find(e => e.userId === user.id && e.inventoryId === item.id)?.quantity || 0}
                        </div>
                        <div className="col col-md-1 d-flex align-items-center">{item.needQuantity}</div>
                        <div className="col col-md-1 d-flex align-items-center">{item.unitOfMeasuring.shortName}</div>

                        <div className='col col-md-2'>
                            {item.isTaken && 
                                <>
                                    <IconButton 
                                        disabled={disabledEdit} 
                                        onClick={markItemAsEdit.bind(this, item)}>
                                        <i className="fa-sm fas fa-pencil-alt text-warning"></i>
                                    </IconButton>
                                    <Tooltip title="Will not take" placement="right-start">
                                        <IconButton
                                            disabled={disabledEdit} 
                                            onClick={onWillNotTake.bind(this, item)}>
                                            <i className="fa-sm fas fa-minus text-danger"></i>
                                        </IconButton>
                                    </Tooltip>
                                </>
                            }

                            {!item.isTaken && item.needQuantity - usersInventories.data.reduce((acc, cur) => {
                                                return cur.inventoryId === item.id ? acc + cur.quantity : acc + 0
                                            }, 0) > 0 &&
                                <Tooltip title="Will take" placement="right-start">
                                    <IconButton
                                        onClick={markItemAsWillTake.bind(this, item)}>
                                        <i className="fa-sm fas fa-plus text-success"></i>
                                    </IconButton>
                                </Tooltip>
                            }
                        </div>
                    </>
                }
            </>
        );
    }
}