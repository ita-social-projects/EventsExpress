import React, { Component } from 'react';
import { Button, Menu, MenuItem } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { useStyle } from './CardStyle';
import CustomAvatar from '../avatar/custom-avatar';
import Badge from '@material-ui/core/Badge';
import Moment from 'react-moment';
import Tooltip from '@material-ui/core/Tooltip';
import CardHeader from '@material-ui/core/CardHeader';
import IconButton from '@material-ui/core/IconButton';
import { getAttitudeClassName } from './attitude';
import './event-item-header.css';


export default class EventHeader extends Component {

    constructor(props) {
        super(props);

        this.state = {
            anchorElO: null,
            anchorElM: null,
        }
    }

    handleClickOnMember = (event) => {
        this.setState({ anchorElM: event.currentTarget });
    }

    handleCloseOnMember = () => {
        this.setState({ anchorElM: null });
    }

    handleClickOnOwners = (event) => {
        this.setState({ anchorElO: event.currentTarget });
    }

    handleCloseOnOwners = () => {
        this.setState({ anchorElO: null });
    }

    renderOwners = (owners , avatar) => {
        return (
            <Button title={owners[0].username} className="btn-custom" onClick={this.handleClickOnOwners}>
                <Badge overlap="circle" badgeContent={owners.length} color="primary">
                    <CustomAvatar
                        className={avatar}
                        userId={owners[0].id}
                        name={owners[0].username}
                    />
                </Badge>
            </Button>
            );
    }



    renderMembers = (first, visitorsCount, avatar) => {
        if (first !== undefined) {
            return (
                < Button title="Visitors" className="btn-custom" onClick={this.handleClickOnMember}>
                    <Badge overlap="circle" badgeContent={visitorsCount} color="primary">
                        <CustomAvatar
                            className={avatar}
                            userId={first.id}
                            name={first.username}
                        />
                    </Badge>
                </Button>
            )
        }
        else {
            return (
                <Tooltip title="Visitors">
                    <IconButton>
                        <Badge badgeContent={ visitorsCount} color="primary">
                            <i className="fa fa-users" />
                        </Badge>
                    </IconButton>
                </Tooltip>
            )

        }
    }
    
    renderTitle = (title, onlyAdults) => {
        return (
            <div>
                {title}
                {onlyAdults &&
                    <span> &#128286;</span>
                }
            </div>
        )
    }

    render() {
        const classes = useStyle;

        const {
            members,
            countVisitor,
            owners,
            dateFrom,
            title,
            onlyAdults,
        } = this.props;
        const { anchorElM, anchorElO } = this.state;

        const firstMember = members ? members[0] : null;



        const PrintMenuMembers = members.map(user => (
            <MenuItem onClick={this.handleCloseOnMember} style={{ overflow: "visible"}}>
                <div className={"d-flex align-items-center border-bottom w-100 " + getAttitudeClassName(user.attitude)} >
                    <div className="flex-grow-1" >
                        <Link to={'/user/' + user.id} className="btn-custom">
                            <div className="d-flex align-items-center border-bottom">
                                <CustomAvatar
                                    userId={user.photoUrl}
                                    name={user.username}
                                />
                                <div>
                                    <h5 className="pl-2">{user.username}</h5>
                                </div>
                            </div>
                        </Link>
                    </div>
                </div>
            </MenuItem>
        ))

        const PrintMenuItems = owners.map(user => (
            <MenuItem onClick={this.handleCloseOnOwners}>
                <div className="d-flex align-items-center border-bottom">
                    <div className="flex-grow-1">
                        <Link to={'/user/' + user.id} className="btn-custom">
                            <div className="d-flex align-items-center border-bottom">
                                <CustomAvatar
                                    userId={user.id}
                                    name={user.username}
                                />
                                <div>
                                    <h5 className="pl-2">{user.username}</h5>
                                </div>
                            </div>
                        </Link>
                    </div>
                </div>
            </MenuItem>
        ))

        return (
            <>
                <Menu
                    id="menu-for-members"
                    anchorEl={anchorElO}
                    keepMounted
                    open={Boolean(anchorElO)}
                    onClose={this.handleCloseOnOwners}
                >
                    {
                        PrintMenuItems
                    }
                </Menu>
                <Menu
                    id="menu-for-members"
                    anchorEl={anchorElM}
                    keepMounted
                    open={Boolean(anchorElM)}
                    onClose={this.handleCloseOnMember}
                >
                    {
                        PrintMenuMembers
                    }
                </Menu>
                <CardHeader
                    avatar={ this.renderOwners(owners, classes.avatar) }
                    action={ this.renderMembers(firstMember, countVisitor, classes.avatar) }
                    title={ this.renderTitle(title, onlyAdults) }
                    subheader={<Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>}
                    classes={{ title: 'title' }}
                />
            </>
        );

    }
}
