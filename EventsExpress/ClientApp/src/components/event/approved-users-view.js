import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { getAttitudeClassName } from './attitude';
import CustomAvatar from '../avatar/custom-avatar';
import Button from "@material-ui/core/Button";
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import IconButton from "@material-ui/core/IconButton";
import SimpleModal from './simple-modal';
import './approved-users-view.css';

export default class UserView extends Component{
    constructor() {
        super();
    }

    getAge = birthday => {
        let today = new Date();
        let birthDate = new Date(birthday);
        let age = today.getFullYear() - birthDate.getFullYear();
        let m = today.getMonth() - birthDate.getMonth();

        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }

        if (age >= 100) {
            age = "---";
        }

        return age;
    }


    render() {
        const {
            user,
            isMyEvent,
            isMyPrivateEvent
        } = this.props;


        
        return( <div>
            <div className={"d-flex align-items-center border-bottom w-100 " + getAttitudeClassName(user.attitude)} >
                    <div className="flex-grow-1" >
                        <Link to={'/user/' + user.id} className="btn-custom">
                            <div className="d-flex align-items-center">
                                <CustomAvatar size="little" photoUrl={user.photoUrl} name={user.username} />
                                <div>
                                    <h5>{user.username}</h5>
                                    {'Age: ' + this.getAge(user.birthday)}
                                </div>
                                {user.attitude === 0 &&
                                    <Tooltip title="You like this user" placement="bottom" TransitionComponent={Zoom}>
                                        <div className="retraet">
                                            <i class="far fa-thumbs-up cancel-text"></i>
                                        </div>
                                    </Tooltip>
                                }
                                {user.attitude === 1 &&
                                    <Tooltip title="You dislike this user" placement="bottom" TransitionComponent={Zoom}>
                                        <div className="retraet">
                                            <i class="far fa-thumbs-down cancel-text"></i>
                                        </div>
                                    </Tooltip>
                                }
                            </div>
                        </Link>
                    </div>

                    {(isMyEvent) &&
                        <div>
                            <SimpleModal
                                id={user.id}
                                action={() => this.props.onPromoteToOwner(user.id)}
                                data={'Are you sure, that you wanna approve ' + user.username + ' to owner?'}
                                button={
                                    <Tooltip title="Approve as an owner">
                                        <IconButton aria-label="delete">
                                            <i className="fas fa-plus-circle" ></i>
                                        </IconButton>
                                    </Tooltip>
                                }
                            />
                        </div>
                    }
                </div>
                {isMyPrivateEvent &&
                    <Button
                        onClick={() => this.props.onApprove(user.id, false)}
                        variant="outlined"
                        color="success"
                    >
                        Delete from event
                    </Button>
                }
            </div>);
    }
}
