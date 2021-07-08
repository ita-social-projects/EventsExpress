import React, { Component } from 'react';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';

export default class VisitorSeeItem extends Component {

    render() {
        const { item, disabledEdit, onWillNotTake, markItemAsEdit, usersInventories, user, showAlreadyGetDetailed, onAlreadyGet, alreadyGet } = this.props;
        return (
            <>
                {!item.isEdit &&
                    <>
                        <div className="col col-md-3 d-flex align-items-center">
                            <span className="item" onClick={() => onAlreadyGet(item)}>{item.itemName}</span>
                        </div>
                        <div className="col align-items-center" key={item.id}>
                            {showAlreadyGetDetailed &&
                                usersInventories.data.map((data, key) => {
                                    return (
                                        data.inventoryId === item.id
                                            ? <div key={key}>{data.user.name}: {data.quantity};</div>
                                            : null
                                    );
                                })
                            }

                            {!showAlreadyGetDetailed &&
                                <>
                                    {usersInventories.data.length === 0
                                        ? 0
                                        : alreadyGet
                                    }
                                </>
                            }
                        </div>
                        <div className="col col-md-1 d-flex align-items-center">
                        {usersInventories.data.find(e => e.userId === user.id && e.inventoryId === item.id) === undefined
                            ? 0
                            : usersInventories.data.find(e => e.userId === user.id && e.inventoryId === item.id).quantity}
                        </div>
                        <div className="col col-md-2 d-flex align-items-center">{item.needQuantity}</div>
                        <div className="col col-md-2 d-flex align-items-center">{item.unitOfMeasuring.shortName}</div>

                        <div className='col col-md-2'>
                            {item.isTaken &&
                                <>
                                    <IconButton
                                        disabled={disabledEdit}
                                        onClick={markItemAsEdit}>
                                        <i className="fa-sm fas fa-pencil-alt text-warning" />
                                    </IconButton>
                                    <Tooltip title="Will not take" placement="right-start">
                                        <IconButton
                                            disabled={disabledEdit}
                                            onClick={onWillNotTake.bind(this, item)}>
                                            <i className="fa-sm fas fa-minus text-danger" />
                                        </IconButton>
                                    </Tooltip>
                                </>
                            }

                            {!item.isTaken && item.needQuantity - alreadyGet > 0 &&
                                <Tooltip title="Will take" placement="right-start">
                                    <IconButton
                                        disabled={disabledEdit}
                                        onClick={markItemAsEdit}>
                                        <i className="fa-sm fas fa-plus text-success" />
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
