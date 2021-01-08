import React, { Component } from "react";
import IconButton from "@material-ui/core/IconButton";

export default class UnitOfMeasuringItem extends Component {
    state = ({
        editUnit: false
    })

    displayEditedUnit = () => {
        this.setState({
            editUnit: !this.state.editUnit
        });

    }

    render() {
        const { item } = this.props;
        return (<>
            <td>
                <i className="fas fa-hashtag mr-1"></i>
                {item.unitName}
            </td>
            <td className="d-flex align-items-center justify-content-left">
                {item.shortName}
            </td>
            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-info" size="small" onClick={() => this.props.callback(item.id)}>
                        <i className="fas fa-edit"></i>
                    </IconButton>
                </div>
            </td>
        </>);
    }
}

