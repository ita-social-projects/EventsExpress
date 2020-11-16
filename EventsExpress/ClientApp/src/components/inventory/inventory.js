import React, { Component } from 'react';
import { Field, FieldArray, getFormSyncErrors } from 'redux-form'
import { renderTextField, renderSelectField } from '../helpers/helpers';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import './inventory.css';

const renderInventories = ({ fields, unitOfMeasuringState }) => {

    return (
    <div className="form-group">
        <button type="button" title="Remove item" class="btn btn-secondary btn-icon" onClick={() => fields.push({})}>
            <span class="icon"><i class="fas fa-plus"></i></span> Add item
        </button>
        <ul className="">
        {fields.map((item, index) =>
            <li className="" key={index}>
                <div className="d-flex flex-wrap justify-content-between align-items-center">
                    <div className="p-2 bd-highlight align-self-end">
                        <span>{index + 1}.</span>
                    </div>
                    <div className="p-2 bd-highlight">
                        <Field
                            name={`${item}.itemName`}
                            type="text"
                            fullWidth={false}
                            label="Item name"
                            component={renderTextField}/>
                    </div>
                    <div className="p-2 bd-highlight">
                        <Field
                            name={`${item}.needQuantity`}
                            type="number"
                            fullWidth={false}
                            label="count"
                            component={renderTextField}/>
                    </div>
                    <div className="p-2 bd-highlight">
                        <Field
                            className="selectpicker"
                            name={`${item}.unitOfMeasuring.id`} 
                            component={renderSelectField}>
                            <option></option>
                            {unitOfMeasuringState.units.map((unit, key) => 
                                <option value={unit.id} key={key}>{unit.unitName}</option>
                            )} 
                        </Field>
                    </div>
                    <button type="button" title="Remove item" class="p-2 btn btn-circle clear-backgroud align-self-end" onClick={() => fields.remove(index)}>
                        <i class="fas fa-trash red"></i>
                    </button>
                </div>
            </li>
        )}
        </ul>
    </div>
    )
}



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

    render() {
        return (
            <div>
                <div className='d-flex justify-content-start align-items-center'>
                    <h3>Inventory</h3>
                    {this.state.isOpen
                        ? <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={this.handleOnClickCaret}>
                            <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                <path fillRule="evenodd" d="M3.204 5L8 10.481 12.796 5H3.204zm-.753.659l4.796 5.48a1 1 0 0 0 1.506 0l4.796-5.48c.566-.647.106-1.659-.753-1.659H3.204a1 1 0 0 0-.753 1.659z"/>
                            </svg>
                        </button>
                        :  <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={this.handleOnClickCaret}>
                                <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">                            
                                    <path d="M7.247 11.14L2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z"/>
                                </svg>
                        </button>
                    }
                    {this.props.syncErrors.inventories && !this.state.isOpen && 
                        <span className="red"><i class="fas fa-exclamation-circle red"></i>required</span>
                    }                  

                </div>
                <div className={this.state.isOpen ? "d-block" : "d-none"}>
                    <FieldArray name="inventories" props={this.props} component={renderInventories}/>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state) => ({
    unitOfMeasuringState: state.unitsOfMeasuring,
    syncErrors: getFormSyncErrors('event-form')(state)
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring())
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Inventory);