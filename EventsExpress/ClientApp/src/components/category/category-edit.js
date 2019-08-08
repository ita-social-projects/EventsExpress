import React, { Component } from "react";
import { connect } from 'react-redux';
import { Field, reduxForm, formValueSelector } from "redux-form";
import { renderTextField } from '../helpers/helpers';
import Fab from '@material-ui/core/Fab';
import './Category.css';

import TextField from '@material-ui/core/TextField';
import IconButton from "@material-ui/core/IconButton";


class categoryItem extends Component {

    componentDidMount = () => {
        this.props.get_roles();
        let obj = JSON.parse('{"category":"' + this.props.item.name + '"}')
        this.props.initialize(obj)   
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.callback({Name: this.props.newName});
    }

    render() {

        return <>
            <td className="align-middle" width="75%">
                <form className="w-100" id="save-form" onSubmit={this.handleSubmit}>
                    <div className="d-flex flex-column justify-content-around ">
                        <Field
                            className="form-control"
                            autoFocus
                            name="category"
                            label="Name"
                            defaultValue={this.props.item.name}
                            component={renderTextField}
                        />

                        {(this.props.message) 
                            ? <div className="text-danger">
                                {this.props.message}
                            </div> 
                            : null
                        }    
                    </div>
                </form>
                

            </td>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton  className="text-success"  size="small" type="submit" form="save-form">
                        <i className="fa fa-check"></i>
                    </IconButton>   

                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
                </div>        
            </td>
        </>
    }

};

const selector = formValueSelector("save-form")

const mapStateToProps = (state, props) => {
    return {
        newName: selector(state, "category"),
        initialValues: props.item.name
    };
};

categoryItem = connect(mapStateToProps, null)(categoryItem);

categoryItem = reduxForm({
    form: "save-form",
    enableReinitialize: true
})(categoryItem);

export default categoryItem;