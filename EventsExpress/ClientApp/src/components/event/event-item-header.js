﻿import React, { Component } from 'react';
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
            anchorElM: null
        };
    }

    handleClickOnMember = (event) => {
        this.setState({ anchorElM: event.currentTarget });
    };

    handleCloseOnMember = () => {
        this.setState({ anchorElM: null });
    };

    handleClickOnOrganizers = (event) => {
        this.setState({ anchorElO: event.currentTarget });
    };

    handleCloseOnOrganizers = () => {
        this.setState({ anchorElO: null });
    };

    renderOrganizers = (organizers, avatar) => {
        return (
            <Button title={organizers[0].username} className="btn-custom" onClick={this.handleClickOnOrganizers}>
                <Badge overlap="circular" badgeContent={organizers.length} color="primary">
                    <CustomAvatar
                        className={avatar}
                        userId={organizers[0].id}
                        name={organizers[0].username}
                    />
                </Badge>
            </Button>
        );
    };


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
            );
        } else {
            return (
                <Tooltip title="Visitors">
                    <IconButton>
                        <Badge badgeContent={visitorsCount} color="primary">
                            <i className="fa fa-users" />
                        </Badge>
                    </IconButton>
                </Tooltip>
            );
        }
    };

    render() {
        const classes = useStyle;

        const {
            members,
            countVisitor,
            organizers,
            dateFrom,
            title
        } = this.props;
        const { anchorElM, anchorElO } = this.state;

        const firstMember = members ? members[0] : null;

        const PrintMenuMembers = members.map(user => (
            <MenuItem key={user.id} onClick={this.handleCloseOnMember} style={{ overflow: "visible" }}>
                <div className={"d-flex align-items-center border-bottom w-100 " + getAttitudeClassName(user.attitude)}>
                    <div className="flex-grow-1">
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
        ));

        const PrintMenuItems = organizers.map(user => (
            <MenuItem key={user.id} onClick={this.handleCloseOnOrganizers}>
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
        ));

        return (
            <>
                <Menu
                    id="menu-for-members"
                    anchorEl={anchorElO}
                    keepMounted
                    open={Boolean(anchorElO)}
                    onClose={this.handleCloseOnOrganizers}
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
                    avatar={this.renderOrganizers(organizers, classes.avatar)}
                    action={this.renderMembers(firstMember, countVisitor, classes.avatar)}
                    title={title}
                    subheader={<Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>}
                    classes={{ title: 'title' }}
                />
            </>
        );

    }
}
