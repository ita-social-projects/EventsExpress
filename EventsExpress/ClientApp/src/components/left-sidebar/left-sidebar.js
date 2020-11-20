import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Badge from '@material-ui/core/Badge';
import HeaderProfileWrapper from '../../containers/header-profile';
import './left-sidebar.css';

const NavItem = ({ to, icon, text, my_icon }) => {
    return (
        <li className="sidebar-header">
            <Link to={to} className="active">
                <span className="link">
                    <i className={icon + ' nav-item-icon'}></i>
                    {my_icon}
                    <span className="nav-item-text">&nbsp;{text}</span>
                    <strong></strong>
                </span>
            </Link>
        </li>
    );
}

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
                            {this.props.user.id &&
                                <>
                                    <NavItem
                                        to={'/user/' + this.props.user.id}
                                        icon={'fa fa-user'}
                                        text={"Profile"}
                                    />
                                    <NavItem
                                        to={'/search/users?page=1'}
                                        icon={'fa fa-users'}
                                        text={"Search Users"}
                                    />
                                    <NavItem
                                        to={'/occurenceEvents'}
                                        my_icon={<i className="fa fa-clone"></i>}
                                        text={"Reccurent Events"}
                                    />
                                    <NavItem
                                        to={'/user_chats'}
                                        my_icon={
                                            <Badge badgeContent={this.props.msg_for_read().length} color="primary">
                                                <i className="fa fa-envelope"></i>
                                            </Badge>
                                        }
                                        text={"Comuna"}
                                    />
                                </>
                            }
                            {this.props.user.role === "Admin" &&
                                <>
                                    <NavItem
                                        to={'/admin/categories/'}
                                        icon={'fa fa-hashtag'}
                                        text={"Categories"}
                                    />
                                    <NavItem
                                        to={'/admin/users?page=1'}
                                        icon={'fa fa-users'}
                                        text={"Users"}
                                    />
                                    <NavItem
                                        to={'/admin/events?page=1'}
                                        icon={'fa fa-calendar'}
                                        text={"Events"}
                                    />
                                </>
                            }
                            {this.props.user.role === "User" &&
                                <>
                                    <NavItem
                                        to={'/contactUs'}
                                        icon={'fa fa-exclamation-circle'}
                                        text={'Contact us'}
                                    />
                                </>
                            }
                        </ul>
                    </nav>
                </div>
            </>
        );
    }
}

export default LeftSidebar;
