import { connect } from 'react-redux';
import React, { Component } from 'react';
import { get_SearchUsers, reset_users } from '../actions/users';
import Spinner from '../components/spinner';
import UserItemList from '../components/users/user-item';
import UserSearchFilterWrapper from '../containers/UserSearchFilterWrapper';
import BadRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
import Unauthorized from '../components/Route guard/401'
import Forbidden from '../components/Route guard/403'
import NotFound from '../components/Route guard/404';
import { Redirect } from 'react-router'
import history from '../history';
import { reset } from 'redux-form';
class SearchUsers extends Component {
    componentDidUpdate(prevProps, prevState) {
        if (this.props.users.isError.ErrorCode == '500') {
            this.getUsers("?page=1");
        }
    }
    componentWillMount() {
        this.getUsers(this.props.params);
    }

    componentWillUnmount = () => {
        this.props.reset_users();
    }
    onReset = () => {
        this.props.reset_filters();
        var search_string = '?page=1';
        this.props.get_SearchUsers(search_string);
        history.push(window.location.pathname + search_string);   
    }
    getUsers = (page) => this.props.get_SearchUsers(page);

    render() {
        const { isPending, isError } = this.props.users;
        const spinner = isPending ? <Spinner /> : null;
        const errorMessage = isError.ErrorCode == '403' ? <Forbidden /> : isError.ErrorCode == '500' ? <Redirect from="*" to="/search/users?page=1" /> : isError.ErrorCode == '401' ? <Unauthorized /> : isError.ErrorCode == '400' ? <BadRequest /> : null;
        const content = (errorMessage == null) ? <UserItemList users={this.props.users.data.items} page={this.props.users.data.pageViewModel.pageNumber} totalPages={this.props.users.data.pageViewModel.totalPages} callback={this.getUsers} />
            : null;
        return <>
            <div className="row">
                <div className='col-12'>
                        < UserSearchFilterWrapper onReset={this.onReset}/>
                    
                        {spinner || content}
                        {errorMessage}
                </div>
            </div>
        </>
    }
}

const mapStateToProps = (state) => ({
    users: state.users
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_SearchUsers: (page) => dispatch(get_SearchUsers(page)),
        reset_users: () => dispatch(reset_users()),
        reset_filters: () => dispatch(reset('user-search-filter-form'))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchUsers);