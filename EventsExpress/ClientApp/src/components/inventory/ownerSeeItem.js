import React, { Component } from 'react';
import IconButton from "@material-ui/core/IconButton";

export default class OwnerSeeItem extends Component {

    render() {
        const { item, disabledEdit, onAlreadyGet, markItemAsEdit, deleteItemFromList, usersInventories, showAlreadyGetDetailed } = this.props;
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
                                            ?   <div key={key}>{data.user.name}: {data.quantity};</div>
                                            :   null
                                        );
                                    })
                                }

                                {!showAlreadyGetDetailed && 
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
                        <div className="col col-md-2 d-flex align-items-center">{item.needQuantity}</div>
                        <div className="col col-md-2 d-flex align-items-center">{item.unitOfMeasuring.shortName}</div>
                    <div className="col col-md-2 d-flex align-items-center">
                            <IconButton 
                                disabled={disabledEdit} 
                                onClick={markItemAsEdit}>
                                <i className="fa-sm fas fa-pencil-alt text-warning"></i>
                            </IconButton>
                            <IconButton
                                disabled={disabledEdit} 
                                onClick={deleteItemFromList.bind(this, item)}>
                                <i className="fa-sm fas fa-trash text-danger"></i>
                            </IconButton>
                        </div> 
                    </>
                }
            </>
        );
    }
}