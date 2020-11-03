﻿import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';
import './Comment.css';
import { getTimeDifferenceFromNull } from '../helpers/TimeHelper';

export default class commentItem extends Component {

    render() {
        const { text, userPhoto, date, userName, userId } = this.props.item;
        const { user } = this.props;
        return (
            <div>
                <div>
                    <div className="row">
                        {!(user === userId) && 
							<div className="photo-container">
                            	<Avatar
                                	alt="Тут аватар"
                                	src={userPhoto}
                            	/>
                            	<h1 className="text-secondary comment-text"> {getTimeDifferenceFromNull(date)}</h1>
                        	</div>
						}
                        <div className="mybutton">
                            <p>
                                <Link to={'/user/' + userId} className="btn-custom">
									<a className="float-left" href={`/user/${userId}`}>
										<strong className="text-primary">{userName}</strong>
									</a>
								</Link>
                            </p>
                            <div className="clearfix"></div>
                            <p>{text}</p>
                        </div>
                        {(user === userId) && 
							<div className="photo-container">
                            	<Avatar
                                	alt="Avatar"
                                	src={userPhoto}
                            	/>
                            	<h1 className="text-secondary comment-text"> {getTimeDifferenceFromNull(date)}</h1>
                        	</div>
						}
                    </div>
                </div>
            </div>
        );
    }
}
