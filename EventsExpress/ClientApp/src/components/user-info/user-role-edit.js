import React, { Component } from 'react';
import Fab from '@material-ui/core/Fab';
import { connect } from 'react-redux';
import { reduxForm, formValueSelector, Field , Form} from 'redux-form';
import get_roles from '../../actions/roles'
import IconButton from "@material-ui/core/IconButton";

class UserRoleEdit extends Component {
    componentDidMount = () => {
        this.props.get_roles();
        let obj = JSON.parse('{"role-for-' + this.props.user.id + '":"' + this.props.user.role.id + '"}')
        this.props.initialize(obj)   
    }



    renderRolesOptions = (arr) => {
        return arr.map((item) => {
            return <option key={item.id} value={item.id}>{item.name}</option>;
        });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        var newRole = this.props.roles.find(r => r.id === this.props.newRoleId);
        this.props.callback(newRole);
    }


    render() {

        return (<>
            <td className="align-middle">
                <Form onSubmit={this.handleSubmit} id="user-role"> 
                    <Field className="form-control" name={"role-for-"+ this.props.user.id} component="select">
                        {this.renderRolesOptions(this.props.roles)}
                    </Field>
                </Form>
            </td>

            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center">
                    <IconButton  className="text-success" size="small" type="submit" form='user-role' >
                        <i className="fas fa-check"></i>
                    </IconButton>
                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
                </div>
            </td>
        </>)
    }
}

const selector = formValueSelector("user-role")

const mapStateToProps = (state, props) => {
    return {
        roles: state.roles.data,
        newRoleId: selector(state, "role-for-" + props.user.id)
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_roles: () => dispatch(get_roles())
    }
};

UserRoleEdit = connect(mapStateToProps, mapDispatchToProps)(UserRoleEdit);

UserRoleEdit = reduxForm({
    form: "user-role",
    enableReinitialize: true
})(UserRoleEdit);

export default UserRoleEdit;