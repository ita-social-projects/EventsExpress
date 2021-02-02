import React, { Component } from 'react';
class DisplayOnline extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div><a
                style={{
                    color: "black",
                    textDecoration: "underline"
                }}
                target="_blank" href={this.props.locationPath}>Online meeting here</a></div>
        )
    }

}
export default DisplayOnline;