import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Comment from '../comment/comment';
import EditEventWrapper from '../../containers/edit-event';
import CustomAvatar from '../avatar/custom-avatar';
import RatingWrapper from '../../containers/rating';
import IconButton from "@material-ui/core/IconButton";
import Moment from 'react-moment';
import 'moment-timezone';
import '../layout/colorlib.css';

export default class OccurenceEventItemView extends Component {

    renderUsers = arr => {
        return arr.map(x => (
            <Link to={'/user/' + x.id} className="btn-custom">
                <div className="d-flex align-items-center border-bottom">
                    <CustomAvatar size="little" photoUrl={x.photoUrl} name={x.username} />
                    <div>
                        <h5>{x.username}</h5>
                        {'Age: ' + this.getAge(x.birthday)}
                    </div>
                </div>
            </Link>)
        );
    }

    renderOwner = user => (
        <Link to={'/user/' + user.id} className="btn-custom">
            <div className="d-flex align-items-center border-bottom">
                <div className='d-flex flex-column'>
                    <IconButton className="text-warning" size="small" disabled >
                        <i class="fas fa-crown"></i>
                    </IconButton>
                    <CustomAvatar size="little" photoUrl={user.photoUrl} name={user.username} />
                </div>
                <div>
                    <h5>{user.username}</h5>
                    {'Age: ' + this.getAge(user.birthday)}
                </div>
            </div>
        </Link>
    )

    render() {
        console.log('props', this.props);
        const {current_user} = this.props; 
        const {isActive, frequency, periodicity, lastRun, nextRun} = this.props.occurenceEvent.data;

        return <>
            <div className="container-fluid mt-1">
                <div className="row">
                    <div className="col-2">
                        Last Run : {lastRun}
                    </div>
                        Next Run : {nextRun}
                    <div className="col-2">
                        Frequency : {frequency}
                    </div>
                    <div className="col-2">
                        Periodicity : {periodicity}
                    </div>
                    <div className="col-2">
                        Is Active : {isActive}
                    </div>
                    <div className="col-2">
                    </div>
                    
                </div>
            </div>
        </>
    }
}
