import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { getAttitudeClassName } from './attitude';
import CustomAvatar from '../avatar/custom-avatar';
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import './users-view.css';
import { getAge } from '../helpers/helpers';

export default class UserView extends Component{
    constructor() {
        super();
    }

    render() {
        const { user } = this.props;
        const att = user.attitude; 

        return( <div>
            <div className={"d-flex align-items-center border-bottom w-100 " + getAttitudeClassName(att)} >
                <div className="flex-grow-1" >
                    <Link to={'/user/' + user.id} className="btn-custom">
                        <div className="d-flex align-items-center">
                            <CustomAvatar size="little" userId={user.id} name={user.username} />
                            <div>
                                <h5>{user.username}</h5>
                                {'Age: ' + getAge(user.birthday)}
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
                {
                    this.props.children
                }
            </div>
        </div>);
    }
}
