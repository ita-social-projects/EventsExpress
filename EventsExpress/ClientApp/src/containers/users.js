import React, { Component } from 'react';
import { get_users, reset_users } from '../actions/users/users-action';
import { connect } from 'react-redux';
import Users from '../components/users';
import Spinner from '../components/spinner';
import UsersFilterWrapper from '../containers/user-filter';

class UsersWrapper extends Component {
    componentDidMount() {
        this.getUsers(this.props.location.search);
    }

    componentWillUnmount = () => {
        this.props.reset_users();
    }

    getUsers = (page) => this.props.get_users(page);

    render() {
        const { users: { isPending, data }, location } = this.props;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending
            ? <Users
                users={data.items}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                callback={this.getUsers}
            />
            : null;
        const filter = <UsersFilterWrapper location={location} />;

        return <>
            <div className="row">
                <div className='col-9'>
                    {spinner || content}
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
    return {
        get_users: (page) => dispatch(get_users(page)),
        reset_users: () => dispatch(reset_users())
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersWrapper);