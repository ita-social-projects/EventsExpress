import React, { Component } from "react";
import IconButton from "@material-ui/core/IconButton";


export default class CategoryItem extends Component {
    
    render() {
        const { item, callback } = this.props;
        
        return (<>
            <td>
                <i className="fas fa-hashtag mr-1"></i>
                {item.name}
            </td>
            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-info"  size="small" onClick={callback}>
                        <i className="fas fa-edit"></i>
                    </IconButton>
                </div>
            </td>
        </>);
    }
}