import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import CustomAvatar from '../avatar/custom-avatar';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import { connect } from 'react-redux';
import IconButton from "@material-ui/core/IconButton";
import Moment from 'react-moment';
import 'moment-timezone';
import { renderPeriod } from './render-period'
import { useStyles } from './card-style-const'
import SelectiveForm from './selective-form'
import '../layout/colorlib.css';
import get_event from '../../actions/event/event-item-view-action';
import {eventImage} from "../../constants/eventImage";

class EventScheduleItemView extends Component {

    componentWillMount() {
        this.props.get_event(this.props.eventSchedule.data.eventId);
    }

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
                    <CustomAvatar size="little" userId={user.id} name={user.username} />
                </div>
                <div>
                    <h5>{user.username}</h5>
                    {'Age: ' + this.getAge(user.birthday)}
                </div>
            </div>
        </Link>
    )


    render() {

        const classes = useStyles;
        const { current_user } = this.props;
        const {
            frequency,
            periodicity,
            lastRun,
            nextRun,
            title,
            photoUrl,
            eventId,
            owners
        } = this.props.eventSchedule.data;
        const period = renderPeriod(periodicity, frequency);
        let isMyEvent = owners.find(x => x.id === current_user.id) != undefined;
        return <>
            <div className="container-fluid mt-1">
                <div className={"col-8 col-sm-10 col-md-8 col-xl-8 mt-3"}>
                    <Card
                        className={classes.card}
                    >
                        <CardHeader
                            subheader={`Reccurent event ${period}`}
                        />
                        <CardMedia
                            className={classes.media}
                            title={title}
                        >
                            <img src={eventImage}
                                 id="eventPreviewPhotoImg" alt="Event"
                                 className="w-100" />
                        </CardMedia>
                        <div className="text-block">
                            <CardContent>
                                <div className="title"> {title} </div>
                                <div>Last Run
                                    <Moment className="ml-2" format="D MMM YYYY" withTitle>{lastRun}</Moment>
                                </div>
                                <div>Next Run
                                    <Moment className="ml-2" format="D MMM YYYY" withTitle>{nextRun}</Moment>
                                </div>
                            </CardContent>
                        </div>
                    </Card>
                </div>
                <div className={"col-8 col-sm-10 col-md-8 col-xl-8 mt-3"}>
                    {isMyEvent &&                        
                        <SelectiveForm eventId={eventId} event={this.props.event} />
                    }
                </div>
            </div>
        </>
    }
}

const mapStateToProps = (state) => ({
    eventSchedule: state.eventSchedule,
});

const mapDispatchToProps = (dispatch) => ({
    get_event: (id) => dispatch(get_event(id))
})

export default connect(mapStateToProps, mapDispatchToProps)(EventScheduleItemView);
