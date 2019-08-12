import { connect } from 'react-redux';
import React, { Component } from 'react';
import { get_SearchUsers } from '../actions/users';
import Spinner from '../components/spinner';
import UserItemList from '../components/users/user-item';
import UserSearchFilterWrapper from '../containers/UserSearchFilterWrapper';
import BadRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
import Unauthorized from '../components/Route guard/401'
import Forbidden from '../components/Route guard/403'
import NotFound from '../components/Route guard/404';
import { Redirect } from 'react-router'

class SearchUsers extends Component {
    componentDidUpdate(prevProps, prevState) {
        if (this.props.users.isError.ErrorCode == '500') {
            this.getUsers(this.props.params);
        }
    }
    componentDidMount() {

        this.getUsers(this.props.params);
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
                {spinner}
                <div className='col-12'>
                    <div className='col-9'>  
                        < UserSearchFilterWrapper />

                        {errorMessage}
                        {content}
                    </div>
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
        get_SearchUsers: (page) => dispatch(get_SearchUsers(page))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchUsers);