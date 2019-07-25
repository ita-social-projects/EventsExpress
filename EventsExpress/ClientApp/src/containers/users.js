import React, {Component} from 'react';
import get_users from '../actions/users';
import { connect } from 'react-redux';
import Avatar from '@material-ui/core/Avatar';


class UsersWrapper extends Component{

    componentDidMount = () => this.props.get_users()

    render() {
        return (
            <Users users={this.props.users.data} />
        );
    }

} 

const mapStateToProps = (state) => ({
    users: state.users,
    currentUser: state.user
});

const mapDispatchToProps = (dispatch) => { 
    return{
    get_users: () => dispatch(get_users())
 };
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersWrapper);




class Users extends Component{
    renderUsers = (arr) => {
        return arr.map(user => <UserInfoWpapper key={user.id} user={user} />);
    }

    render() {
        return (
            <table className="table">
                <thead className="bg-light"> 
                    <td scope="col"></td>
                    <td scope="col">email</td>
                    <td scope="col">name</td>
                    <td scope="col">role</td>
                    <td scope="col">status</td>
                </thead>
                <tbody>
                    {this.renderUsers(this.props.users)}
                </tbody>
            </table>
                
        )
    }       
}




class UserInfoWpapper extends Component {
    render() {
        return(
            <tr id={this.props.user.id} className={(this.props.user.isBlocked == true) ? "bg-warning" : ""}>
                <td className="align-middle">
                    {this.props.user.photoUrl
                        ? <Avatar src={this.props.user.photoUrl} />
                        : <Avatar>{this.props.user.email.charAt(0).toUpperCase()}</Avatar>}
                    
                </td>
                <td className="align-middle">{this.props.user.email}</td>
                <td className="align-middle">{this.props.user.username}</td>
                <td className="align-middle">
                    <div className="d-flex justify-content-between align-items-center h-100">
                        {this.props.user.role.name}
                        <i class="fas fa-edit"></i>
                    </div>
                    
                </td>
                <td className="align-middle">
                    <div className="d-flex justify-content-center align-items-center">
                        {(this.props.user.isBlocked == true)
                            ? <i class="fas fa-lock"></i>
                            : <i class="fas fa-unlock-alt"></i>}
                    </div>
                </td>
            </tr>
        )
    }
}