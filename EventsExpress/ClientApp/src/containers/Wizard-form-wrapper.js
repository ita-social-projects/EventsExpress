import React, { Component } from 'react';
import { connect } from 'react-redux';
import get_event, {
    resetEvent,
    approveUser,
}
    from '../actions/event-item-view';
import WizardStepper from './wizard-stepper';

class WizardFormWrapper extends Component {
    componentWillMount() {
        const { id } = this.props.match.params;
        this.props.get_event(id);
    }

    componentWillUnmount() {
        this.props.reset();
    }

    onCancel = (reason) => {
        this.props.cancel(this.props.event.data.id, reason);
    }

    onApprove = (userId, buttonAction) => {
        this.props.approveUser(userId, this.props.event.data.id, buttonAction);
    }

    render() {
        return <WizardStepper />
    }
}

const mapStateToProps = (state) => ({
    event: state.event,
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => ({
    get_event: (id) => dispatch(get_event(id)),
    approveUser: (userId, eventId, buttonAction) => dispatch(approveUser(userId, eventId, buttonAction)),
    reset: () => dispatch(resetEvent())
});


export default connect(mapStateToProps, mapDispatchToProps)(WizardFormWrapper);
