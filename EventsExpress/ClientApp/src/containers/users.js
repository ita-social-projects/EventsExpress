import React, {Component} from 'react';
import get_users from '../actions/users';
import { connect } from 'react-redux';
import Users from '../components/users';
import Spinner from '../components/spinner';

class UsersWrapper extends Component{

    componentDidMount() {
        const { page } = this.props.match.params;
        this.getUsers(page);
    }
    getUsers = (page) => this.props.get_users(page);

    render() {
        const {isPending, isError } = this.props;
        const spinner = isPending ? <Spinner /> : null;
        return <>
            {spinner}
            <Users users={this.props.users.data.items} page={this.props.users.data.pageViewModel.pageNumber} totalPages={this.props.users.data.pageViewModel.totalPages} callback={this.getUsers} /> 
        </>
    }

}

const mapStateToProps = (state) => ({
    users: state.users,
    currentUser: state.user
});

const mapDispatchToProps = (dispatch) => { 
    return{
        get_users: (page) => dispatch(get_users(page))
 };
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersWrapper);