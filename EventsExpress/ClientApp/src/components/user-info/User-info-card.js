import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import genders from '../../constants/GenderConstants';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average';
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import { getAge } from '../helpers/get-age-string';
import './user-info.css';

export default class UserInfoCard extends Component {

    render() {
        const { user } = this.props;
        const attitudeColor = (user.attitude === 0)
            ? '#c2ffc2'
            : (user.attitude === 1)
                ? '#ffc6c2'
                : '';

        return (
            <div className="offset-3 col-6 mt-4 mb-4">
                <Paper style={{ backgroundColor: attitudeColor }}>
                    <div className='d-flex'>
                        <Link to={`/user/${user.id}`}>
                            <CustomAvatar
                                size="little"
                                userId={user.id}
                                name={user.firstName}
                            />
                        </Link>
                        <div className='d-flex flex-column'>
                            <Link to={`/user/${user.id}`}>{user.firstName}</Link>
                            <div>{genders[user.gender]}</div>
                            <div>Age: {getAge(user.birthday)}</div>
                        </div>
                        <div className='ml-auto'>
                            <RatingAverage value={user.rating} direction='col' />
                            {user.attitude === 0 &&
                                <Tooltip title="You like this user" placement="bottom" TransitionComponent={Zoom}>
                                    <div className="retreat">
                                        <i className="far fa-thumbs-up Size" />
                                    </div>
                                </Tooltip>
                            }
                            {user.attitude === 1 &&
                                <Tooltip title="You dislike this user" placement="bottom" TransitionComponent={Zoom}>
                                    <div className="retreat" >
                                        <i className="far fa-thumbs-down Size" />
                                    </div>
                                </Tooltip>
                            }
                        </div>
                    </div>
                </Paper>
            </div>
        )
    }
}
