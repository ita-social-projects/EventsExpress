import React, { Component } from 'react';
import './users.css';
import UserInfoWpapper from '../../containers/user-info';
import users from '../../containers/users';


export default class Users extends Component {
    renderUsers = (arr) => {
        
        return arr.map(user => <UserInfoWpapper id={user.id + user.isBlocked} user={user} /> );
    }


    render() {

        return (
            <table className="table">
                <thead className="bg-light">
                    <tr>
                        <td scope="col"></td>
                        <td scope="col">email</td>
                        <td scope="col">name</td>
                        <td scope="col">role</td>
                        <td scope="col"></td>
                        <td scope="col">status</td>
                    </tr>
                </thead>
                <tbody>
                    {this.renderUsers(this.props.users)}
                </tbody>
            </table>
        )
    }
}