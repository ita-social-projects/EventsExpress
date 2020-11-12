import React, { Component } from 'react';
import { Field, FieldArray } from 'redux-form'
import { renderTextField } from '../helpers/helpers';
import { connect } from 'react-redux';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import './inventory.css';

const renderInventories = ({ fields, unitOfMeasuringState }) => {
    return (
    <ul className="list-group">
      <li className="list-group-item">
        {/* <button type="button" onClick={() => fields.push({})}>Add item</button> */}
        <button type="button" title="Remove item" class="btn btn-secondary btn-icon" onClick={() => fields.push({})}>
            <span class="icon"><i class="fas fa-plus"></i></span> Add item
        </button>
      </li>
      {fields.map((item, index) =>
        <li className="list-group-item" key={index}>
          <h4>item #{index + 1}</h4>
            <Field
                name={`${item}.itemName`}
                type="text"
                fullWidth={false}
                component={renderTextField}/>
            <Field
                name={`${item}.needQuantity`}
                type="number"
                fullWidth={false}
                component={renderTextField}/>
            <Field 
                name={`${item}.unitOfMeasuring.id`} component="select">
                <option></option>
                {unitOfMeasuringState.units.map((unit, key) => 
                    <option value={unit.id} key={key}>{unit.unitName}</option>
                )} 
            </Field>
            <button type="button" title="Remove item" class="btn btn-circle" onClick={() => fields.remove(index)}>
                <i class="fas fa-trash red"></i>
            </button>
        </li>
      )}
    </ul>
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
                            <FieldArray name="inventories" props={this.props} component={renderInventories}/>
                          </div>
                        : null
                    }
            </div>
        )
    }
}

const mapStateToProps = (state) => ({
    unitOfMeasuringState: state.unitsOfMeasuring
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring())
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Inventory);