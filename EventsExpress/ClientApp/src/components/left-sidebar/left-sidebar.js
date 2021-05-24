import React, { Component } from 'react';
import Badge from '@material-ui/core/Badge';
import HeaderProfileWrapper from '../../containers/header-profile';
import './left-sidebar.css';
import { NavItem } from '../NavItem/NavItem';
import AuthComponent from '../../security/authComponent';
import { Roles } from '../../constants/userRoles';


class LeftSidebar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            _class: "left-sidebar-closed"
        };
    }

    render() {
        return (
            <>
                <div id='open-close-zone'
                    className={this.state._class + ' d-flex justify-content-start'}
                    onClick={() => {
                        this.state._class === "left-sidebar-opened"
                            ? this.setState({ _class: "left-sidebar-closed" })
                            : this.setState({ _class: "left-sidebar-opened" })
                    }}
                >
                    <button className="open-close-btn">
                        {this.state._class === "left-sidebar-opened" ? '×' : '☰'}
                    </button>
                </div>
                <div className={this.state._class + ' left-sidebar'}>
                    <HeaderProfileWrapper />
                    <nav>
                        <hr />
                        <ul className="list-unstyled">
                            <NavItem
                                to={'/'}
                                icon={'fa fa-home'}
                                text={"Home"}
                            />
                            <AuthComponent rolesMatch={Roles.User}>
                                <NavItem
                                    to={'/user/' + this.props.user.id}
                                    icon={'fa fa-user'}
                                    text={"Profile"}
                                />
                                <NavItem
                                    to={'/drafts'}
                                    icon={'fa fa-edit'}
                                    text={"Draft"}
                                />
                                <NavItem
                                    to={'/search/users?page=1'}
                                    icon={'fa fa-users'}
                                    text={"Search Users"}
                                />
                                <NavItem
                                    to={'/eventSchedules'}
                                    my_icon={<i className="fa fa-clone"></i>}
                                    text={"Recurrent Events"}
                                />
                                <NavItem
                                    to={'/contactAdmin'}
                                    icon={'fa fa-exclamation-circle'}
                                    text={'Contact us'}
                                />
                            </AuthComponent>
                            <AuthComponent>
                                <NavItem
                                    to={'/user_chats'}
                                    my_icon={
                                        <Badge badgeContent={this.props.msg_for_read().length} color="primary">
                                            <i className="fas fa-comments"></i>
                                        </Badge>
                                    }
                                    text={"Comuna"}
                                />
                            </AuthComponent>
                            <AuthComponent rolesMatch={Roles.Admin}>
                                <NavItem
                                    to={'/admin/'}
                                    icon={'fa fa-user-secret'}
                                    text={"Admin"}
                                />
                                <NavItem
                                    to={'/contactAdmin/issues'}
                                    icon={'fa fa-exclamation-triangle'}
                                    text={"Issues"}
                                />
                            </AuthComponent>
                            <AuthComponent onlyAnonymous>
                                <NavItem
                                    to={'/contactAdmin'}
                                    icon={'fa fa-exclamation-circle'}
                                    text={'Contact us'}
                                />
                            </AuthComponent>
                        </ul>
                    </nav>
                </div>
            </>
        );
    }
}

export default LeftSidebar;
