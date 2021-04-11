import { publish_event } from '../../actions/event-add-action';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reduxForm, Field  } from 'redux-form';
import Button from "@material-ui/core/Button";
import { renderTextArea, } from '../helpers/helpers';

class Publish extends Component {
    handleClick = async () => {
        try {
           return await this.props.onSubmit(this.props.initialValues.id);
        }
        catch (error) {
            console.log(error);
        }
    }
    renderErrors = (error) => {
        const keys = Object.keys(error);
        return keys.map(k => <div>{k}:{error[k][0]}</div>)
    }
        render() {
        return (
                <div>
                    <Button
                        className="border"
                        fullWidth={true}
                    color="primary"
                    onClick={this.handleClick}
                    >
                    Publish
                                </Button>
                <ul>
                    {this.renderErrors(this.props.errors)}
                </ul>   
            </div>
            
        )
    }
} 

const mapStateToProps = (state) => ({
    initialValues: state.event.data,
    errors: state.publishErrors.data
});

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmit: (data) => dispatch(publish_event(data)),
    }
};
Publish = connect(
    mapStateToProps,
    mapDispatchToProps,
)(Publish);
export default Publish;

