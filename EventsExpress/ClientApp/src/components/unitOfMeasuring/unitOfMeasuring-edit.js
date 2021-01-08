import React, { Component } from "react";
import { connect } from 'react-redux';
import { Field, reduxForm, formValueSelector } from "redux-form";
import { renderTextField } from '../helpers/helpers';
import IconButton from "@material-ui/core/IconButton";

const divStyle = {
    width: "90wh"
};

const dSt = {
    marginLeft: "0"
}

const ShowError = (props) => {
    return (
        <div className="text-danger">
            <div>{props.error}</div>
        </div>
    )
}

class UnitOfMeasuringEdit extends Component {
    state = ({
        unitError: null,
        shortError: null,
        showAlert: false
    })

    showAlert = () => {
        this.setState({
            showAlert: true
        });
    };

    hideAlert = () => {
        this.setState({
            showAlert: false
        });
    };

    componentDidMount = () => {
        let obj = JSON.parse('{"unitName":"' + this.props.item.unitName + '","shortName":"' + this.props.item.shortName + '"}')
        this.props.initialize(obj);
    }

    setErrors = () => {
        if (this.props.message) {
            let resError = (JSON.parse(this.props.message)).errors;
            if (resError) {
                console.log(resError);
                const { UnitName = '', ShortName = '', ...other } = resError;
                const errorArray = Object.keys(other).map(key =>
                    other[key]);
                this.setState({
                    unitError: `${UnitName} ${errorArray.join(" ")}`,
                    shortError: ShortName + errorArray.join(" ")
                })
            }
        }

    }

    handleSubmit = (e) => {
        e.preventDefault();

        this.props.callback({
            unitName: this.props.newUnitName,
            shortName: this.props.newShortName
        });
        this.setErrors();
    }

    render() {
        return <>
            <td colSpan="3" className="align-middle">
                <form className="w-100" id="save-form" onSubmit={this.handleSubmit}>
                    <div style={divStyle} className="d-flex flex justify-content-around ">

                        <Field
                            className="form-control"
                            autoFocus
                            name="unitName"
                            label="Unit name"
                            defaultValue={this.props.item.unitName}
                            component={renderTextField}

                        />
                        {this.state.unitError ?
                            <ShowError error={this.state.unitError} /> :
                            <div></div>}

                        <Field
                            className="form-control"
                            name="shortName"
                            label="Short name"
                            defaultValue={this.props.item.shortName}
                            component={renderTextField}
                        />
                        {this.state.shortError ?
                            <ShowError error={this.state.shortError} /> :
                            <div></div>}


                    </div>
                </form>


            </td>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-success" size="small" type="submit" form="save-form">
                        <i className="fa fa-check"></i>
                    </IconButton>
                </div>
            </td>
            <td className="align-middle align-items-stretch" width="15%">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
                </div>
            </td>
        </>
    }

}

const selector = formValueSelector("save-form")

const mapStateToProps = (state, props) => {
    return {
        newUnitName: selector(state, "unitName"),
        newShortName: selector(state, "shortName"),
        initialUnitName: props.item.unitName,
        initialShortName: props.item.shortName
    };
};

UnitOfMeasuringEdit = connect(mapStateToProps, null)(UnitOfMeasuringEdit);

export default reduxForm({
    form: "save-form",
    enableReinitialize: true
})(UnitOfMeasuringEdit);

