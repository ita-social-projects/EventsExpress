import React, {Component} from 'react';
import get_users from '../actions/users';
import { connect } from 'react-redux';
import Users from '../components/users';
import Spinner from '../components/spinner';
import UsersFilterWrapper from '../containers/user-filter';
import BagRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
class UsersWrapper extends Component{

    componentDidMount() {
       
        this.getUsers(this.props.params);
    }
    getUsers = (page) => this.props.get_users(page);

    render() {
        const {isPending, isError } = this.props.users;
        const spinner = isPending ? <Spinner /> : null;
      
        const errorMessage = isError.ErrorCode == '400' ? <BagRequest /> : isError.ErrorCode == '500' ? <InternalServerError /> : null;
       
        return <>
            <div className="row">
                {spinner}
                
                <div className='col-9'>
                    <Users users={this.props.users.data.items} page={this.props.users.data.pageViewModel.pageNumber} totalPages={this.props.users.data.pageViewModel.totalPages} callback={this.getUsers} />
                    {errorMessage}
                </div>
                <div className="col-3">
             < UsersFilterWrapper/>
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
        get_users: (page) => dispatch(get_users(page))
 };
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersWrapper);