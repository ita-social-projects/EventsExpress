import React, {Component} from 'react';
import get_users from '../actions/users';
import { connect } from 'react-redux';
import Users from '../components/users';


class UsersWrapper extends Component{

    componentDidMount = () => this.props.get_users()

    render() {
        return (
            <Users users={this.props.users.data} />
        );
    }
}

const mapStateToProps = (state) => ({
    users: state.users
});

const mapDispatchToProps = (dispatch) => { 
    return {
    get_users: () => dispatch(get_users())
 };
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersWrapper);