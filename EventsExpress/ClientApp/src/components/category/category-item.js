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