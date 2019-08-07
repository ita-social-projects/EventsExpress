import React, { Component } from "react";
import IconButton from "@material-ui/core/IconButton";
import { reduxForm } from "redux-form";
import './Category.css';

import Fab from '@material-ui/core/Fab';


export default class categoryItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { item, callback } = this.props;
            return (<>
                <td width="80%">
                    #{item.name}
                </td>
                <td width="10%">
                    <IconButton  className="text-info"  size="small" onClick={callback}>
                        <i className="fas fa-edit"></i>
                    </IconButton>
                </td>
            </>);
    }
}