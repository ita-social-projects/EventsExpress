import React from "react";
import EditUsername from "../../components/profile/editProfile/editUsername";
import { connect } from "react-redux";
import editUsername from "../../actions/EditProfile/editUsername";

class EditUsernameContainer extends React.Component {
    submit = value => {
        console.log(value);
        this.props.editUsername(value.name);
    }



    render() {
        let { isEditUsernamePending, isEditUsernameSuccess, EditUsernameError } = this.props;

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