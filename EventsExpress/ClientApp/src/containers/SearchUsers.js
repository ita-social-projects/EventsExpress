import { connect } from 'react-redux';
import React, { Component } from 'react';
import get_users from '../actions/users';
import Spinner from '../components/spinner';
import UserItemList from '../components/users/user-item';
import UserSearchFilterWrapper from '../containers/UserSearchFilterWrapper';
class SearchUsers extends Component {
    componentDidMount() {

        this.getUsers(this.props.params);
    }
    getUsers = (page) => this.props.get_users(page);

    render() {
        const { isPending, isError } = this.props;
        const spinner = isPending ? <Spinner /> : null;
  
        return <>
            <div className="row">
                {spinner}
                <div className='col-12'>
                    <div className='col-9'>
                          < UserSearchFilterWrapper/>
                    <UserItemList users={this.props.users.data.items} page={this.props.users.data.pageViewModel.pageNumber} totalPages={this.props.users.data.pageViewModel.totalPages} callback={this.getUsers} />
                
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
        get_users: (page) => dispatch(get_users(page))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchUsers);