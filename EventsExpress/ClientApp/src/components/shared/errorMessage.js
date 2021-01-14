import React, { Component } from "react";


export default class ErrorMessages extends Component {

    render() {
        return (
            <>
                {this.props.error.map(x => <div className={"text-danger " + this.props.className}> {x}</div>)}
            </>
        );
    }
}
