import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import genders from '../../constants/GenderConstants';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average';
import './user-info.css';

const getAge = (birthday) => {
    let today = new Date();
    let birthDate = new Date(birthday);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age = age - 1;
    }
    if (age > 100) {
        age = "---";
    }
    return age;
}

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
                                name={user.username}
                            />
                        </Link>
                        <div className='d-flex flex-column'>
                            <Link to={`/user/${user.id}`}>{user.username}</Link>
                            <div>{genders[user.gender]}</div>
                            <div>Age: {getAge(user.birthday)}</div>
                        </div>
                        <div className='ml-auto'>
                            <RatingAverage value={user.rating} direction='col' />
                        </div>
                    </div>
                </Paper>
            </div>
        )
    }
}
