import { publish_event } from '../../actions/event-add-action';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reduxForm, Field, getFormValues, reset, isPristine, initialize } from 'redux-form';
import Button from "@material-ui/core/Button";



class Publish extends Component {
    onPublish = (values) => {
        return this.props.publish(this.props.event.id);
    }

    render() {
        return (
            <form onSubmit={this.onPublish}>
                <div>
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit"
                    >
                        Publish
                                </Button>
                </div>
            </form>
        )
    }
} 

const mapStateToProps = (state) => ({
    initialData: state.event.data,
    user_id: state.user.id,
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        publish: (data) => dispatch(publish_event(data)),
    }
};

Publish = connect(
    mapStateToProps,
    mapDispatchToProps,
)(Publish);

export default reduxForm({
    form: 'Publish',
    enableReinitialize: true
})(Publish);