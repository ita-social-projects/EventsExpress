import React from "react";
import EditUsername from "../../components/profile/editProfile/editUsername";
import { connect } from "react-redux";
import editUsername from "../../actions/editProfile/userName-edit-action";

class EditUsernameContainer extends React.Component {
    submit = value => {
        return this.props.editUsername(value);
    }
    render() {
        return <EditUsername onSubmit={this.submit} />;
    }   
}

const mapStateToProps = state => {
    return state.editUsername;       
};

const mapDispatchToProps = dispatch => {
    return {
        editUsername: (name) => dispatch(editUsername(name))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditUsernameContainer);