import React, { Component } from "react";
import IconButton from "@material-ui/core/IconButton";

export default class CategoryItem extends Component {

    render() {
        const { item, callback } = this.props;

        return (<>
            <td>
                <i className="fas fa-hashtag mr-1" />
                {item.name}
            </td>
            <td className="d-flex align-items-center justify-content-center">
                {item.countOfUser}
            </td>
            <td className="justify-content-center">
                {item.countOfEvents}
            </td>
            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-info"  size="small" onClick={callback}>
                        <i className="fas fa-edit" />
                    </IconButton>
                </div>
            </td>
        </>);
    }
}
