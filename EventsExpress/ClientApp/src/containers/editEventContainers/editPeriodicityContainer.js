import React from "react";
import EditPeriodicity from "../../components/event/editEvent/editPeriodicity";
import { connect } from "react-redux";
import edit_Periodicity from "../../actions/EditEvent/EditPeriodicity";

class EditPeriodicityContainer extends React.Component {
    submit = value => {
        this.props.editPeriodicity(value);
    }



    render() {
        return <EditPeriodicity onSubmit={this.submit} />;
    }


}

const mapStateToProps = state => {
    return state.gender

};

const mapDispatchToProps = dispatch => {
    return {
        editPeriodicity: (periodicity) => dispatch(edit_Periodicity(periodicity))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditPeriodicityContainer);