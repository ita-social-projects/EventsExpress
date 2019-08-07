import React, { Component } from "react";
import { connect } from 'react-redux';
import { Field, reduxForm, formValueSelector } from "redux-form";
import { renderTextField } from '../helpers/helpers';
import Fab from '@material-ui/core/Fab';
import './Category.css';

import IconButton from "@material-ui/core/IconButton";


class categoryItem extends Component {

    handleSubmit = (e) => {
        e.preventDefault();
        console.log(this.props.newName);
        this.props.callback({name: this.props.newName});
    }

    render() {
        return <>
            <td>
                <form className="w-100" id="save-form" onSubmit={this.handleSubmit}>
                    <div className="d-flex justify-content-around ">
                        <Field
                            name="category"
                            component={renderTextField}
                            label={this.props.item.name}
                            type="category"
                        />
                        
                    </div>
                </form>
            </td>
            <td>
                <IconButton  className="text-info"  size="small" type="submit" form="save-form">
                    <i className="fa fa-check-circle"></i>
                </IconButton>   
                <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                    <i className="fas fa-times"></i>
                </IconButton>
                    
                </Fab>
            </td>
        </>
    }

};

const selector = formValueSelector("save-form")

const mapStateToProps = (state, props) => {
    return {
        newName: selector(state, "category")
    };
};

categoryItem = connect(mapStateToProps, null)(categoryItem);

categoryItem = reduxForm({
    form: "save-form",
    enableReinitialize: true
})(categoryItem);

export default categoryItem;