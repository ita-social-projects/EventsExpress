import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { renderTextField, renderDatePicker } from '../helpers/helpers';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';



class Inventory extends Component {

    constructor() {
        super();

        this.state = {
            isOpen: true
        };

        this.handleOnClickCaret = this.handleOnClickCaret.bind(this);
    }

    componentWillMount() {
        this.props.get_unitsOfMeasuring();
    }

    handleOnClickCaret() {
        this.setState(state => ({
            isOpen: !state.isOpen
        }));
    }

    addInventarToEvent = () => {
        let inventar = {
            itemName: this.itemNameInput.value,
            itemCount: this.itemCountInput.value
        };

        let inventories = this.props.inventoryState.items;
        inventories.push(inventar);
        this.props.onAddInventar(inventories);
    }

    render() {
        const { units } = this.props.unitOfMeasuringState;
        return (
            <div>
                <div className='d-flex justify-content-start align-items-center'>
                    <h3>Inventory</h3>
                    <span>{this.props.count} items</span>
                    
                    {this.state.isOpen 
                        ?  <svg onClick={this.handleOnClickCaret} width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                            <path fillRule="evenodd" d="M3.204 5L8 10.481 12.796 5H3.204zm-.753.659l4.796 5.48a1 1 0 0 0 1.506 0l4.796-5.48c.566-.647.106-1.659-.753-1.659H3.204a1 1 0 0 0-.753 1.659z"/>
                        </svg>
                        :  <svg onClick={this.handleOnClickCaret} width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">                            
                            <path d="M7.247 11.14L2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z"/>
                        </svg>
                    }
                </div>

                    {
                        this.state.isOpen 
                        ? <div>
                            <div className="mt-2">
                                <input type="text" ref={(input) => {this.itemNameInput = input}}/>
                                <div className="d-flex justify-content-start">
                                    <input type="number" ref={(input) => {this.itemCountInput = input}}/>
                                    <select>
                                        {units.map((unit, key) => {
                                            return (
                                                    <option key={key} value={unit}>{unit.unitName}</option>
                                            );
                                        })}
                                    </select>
                                </div>
                            </div>

                            <button type="button" onClick={this.addInventarToEvent.bind(this)}>Add</button>
                          </div>
                        : null
                    }
            </div>
        )
    }
}

const mapStateToProps = (state) => ({
    inventoryState: state.inventories,
    unitOfMeasuringState: state.unitsOfMeasuring
});

const mapDispatchToProps = (dispatch) => {
    return {
        onAddInventar: (item) => dispatch({type: "ADD_ITEM_TO_INVENTAR", payload: item}),
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring())
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Inventory);