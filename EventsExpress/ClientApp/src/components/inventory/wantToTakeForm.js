import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { renderCheckbox } from '../helpers/helpers';

class WantToTakeForm extends Component {

    render() {
        const { item } = this.props;
        return(
            <form onSubmit={this.props.handleSubmit}  className="form-inline w-100">  
                <div className="col col-md-1">
                    <Field
                        id={item.id}
                        name={item.id}
                        component={renderCheckbox} 
                        type="checkbox"/>     
                </div>                              
                <div className="col col-md-5">
                    {item.itemName}
                </div>
                <div className="col">{item.needQuantity}</div>
                <div className="col">{item.unitOfMeasuring.shortName}</div>
            </form>
        );
    }
}

export default reduxForm({
    form: 'wantToTake-form',
    enableReinitialize: true
})(WantToTakeForm);