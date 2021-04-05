import { connect } from 'react-redux';
import React, { Component } from 'react';
import { get_SearchUsers, reset_users } from '../actions/users/users-action';
import Spinner from '../components/spinner';
import UserItemList from '../components/users/user-item';
import UserSearchFilterWrapper from '../containers/UserSearchFilterWrapper';
import history from '../history';
import { reset } from 'redux-form';
import {getQueryStringByUsersFilter} from '../components/helpers/userHelper';

class SearchUsers extends Component {
    componentDidUpdate(prevProps, prevState) {
        if (prevProps.users.userSearchFilter !== this.props.users.userSearchFilter) {
            this.getUsers(getQueryStringByUsersFilter(this.props.users.userSearchFilter));
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
        const { isPending } = this.props.users;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending 
            ? <UserItemList users={this.props.users.data.items} 
                page={this.props.users.data.pageViewModel.pageNumber} 
                totalPages={this.props.users.data.pageViewModel.totalPages} 
                callback={this.getUsers} />
           : null;

        return <>
            <div className="row">
                <div className='col-12'>
                        < UserSearchFilterWrapper onReset={this.onReset}/>
                        {spinner || content}
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