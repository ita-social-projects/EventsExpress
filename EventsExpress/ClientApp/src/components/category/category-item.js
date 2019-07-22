import React, { Component } from "react";
import { reduxForm } from "redux-form";

import './Category.css';



export default class categoryItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { name } = this.props.item;
        return (
            <div >
                <h4>#{name}</h4>
            </div>
        );
    }
}


