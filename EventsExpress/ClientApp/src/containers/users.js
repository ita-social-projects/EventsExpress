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
        const { users: { data }, location } = this.props;

        return <Spinner showContent={data != undefined}>
            <div className="row">
                <div className='col-9'>
                    <Users
                        users={data.items}
                        page={data.pageViewModel.pageNumber}
                        totalPages={data.pageViewModel.totalPages}
                        callback={this.getUsers}
                    />
                </div>
                <div className="col-3">
                    < UsersFilterWrapper location={location} />
                </div>
            </div>
        </Spinner>
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