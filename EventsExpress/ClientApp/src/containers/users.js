import React, {Component} from 'react';
import {get_users, reset_users } from '../actions/users';
import { connect } from 'react-redux';
import Users from '../components/users';
import Spinner from '../components/spinner';
import UsersFilterWrapper from '../containers/user-filter';
import BadRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403'
import NotFound from '../components/Route guard/404';
import { Redirect } from 'react-router'
class UsersWrapper extends Component{
    componentDidUpdate(prevProps, prevState) {
        if (this.props.users.isError.ErrorCode == '500') {
            this.getUsers('?page=1');
        }
    }
    componentDidMount() {
        this.getUsers(this.props.params);
    }

    componentWillUnmount = () => {
        this.props.reset_users();
    }

    getUsers = (page) => this.props.get_users(page);

    render() {
        const {isPending, isError } = this.props.users;
        const spinner = isPending ? <Spinner /> : null;
        const errorMessage = isError.ErrorCode == '403' ? <Forbidden /> : isError.ErrorCode == '500' ? <Redirect from="*" to="/admin/users?page=1" /> : isError.ErrorCode == '401' ? <Unauthorized /> : isError.ErrorCode == '400' ? <BadRequest /> : null;
        const content = (errorMessage == null) ? <Users users={this.props.users.data.items} page={this.props.users.data.pageViewModel.pageNumber} totalPages={this.props.users.data.pageViewModel.totalPages} callback={this.getUsers} />
            : null;
        const filter = (isError.ErrorCode != '403') ? < UsersFilterWrapper /> : null;
        return <>
            <div className="row">
                <div className='col-9'>
                {spinner || content}
                    {errorMessage}
                </div>
                <div className="col-3">
                    {filter}
                </div> 
            </div>
        </>
    }
}

const mapStateToProps = (state) => ({
    users: state.users
});

const mapDispatchToProps = (dispatch) => { 
    return{
        get_users: (page) => dispatch(get_users(page)),
        reset_users: () => dispatch(reset_users())
 };
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersWrapper);