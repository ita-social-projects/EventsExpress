import React, { Component } from 'react';
import { get_unitofmeasuring } from '../../actions/inventory';

class Inventory extends Component {

    constructor() {
        super();

        this.state = {
            unitsOfMeasuring: [],
            isOpen: false
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    componentDidMount() {
        get_unitofmeasuring()
        .then(response => {
            console.log(response);
            this.setState({
                unitsOfMeasuring: response
            });
        })
    }

    handleOnClickCaret() {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    render() {
        return (
            <div>
                <h3>Inventory</h3>

                {this.state.isOpen 
                    ?  <svg onClick={this.handleOnClickCaret} width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" d="M3.204 5L8 10.481 12.796 5H3.204zm-.753.659l4.796 5.48a1 1 0 0 0 1.506 0l4.796-5.48c.566-.647.106-1.659-.753-1.659H3.204a1 1 0 0 0-.753 1.659z"/>
                       </svg>
                    : <svg onClick={this.handleOnClickCaret} width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                        <path d="M7.247 11.14L2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z"/>
                      </svg>
                }
                {
                    this.state.isOpen 
                    ? this.state.unitsOfMeasuring.map(unit => {
                            return (
                                <div>
                                    <h5>{unit.shortName}</h5>
                                </div>
                            )
                        })
                    : null
                }
            </div>
        )
    }
}

export default Inventory;