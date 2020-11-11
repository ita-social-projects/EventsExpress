import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import CustomAvatar from '../avatar/custom-avatar';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import IconButton from "@material-ui/core/IconButton";
import Moment from 'react-moment';
import 'moment-timezone';
import { renderPeriod } from '../occurenceEvent/render-period'
import Typography from '@material-ui/core/Typography';
import { useStyles } from '../occurenceEvent/card-style-const'
import { SelectiveForm } from '../occurenceEvent/selective-form'
import AddFromParentEventWrapper from '../../containers/add-event-from-parent';
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

        const classes = useStyles;
        const { current_user } = this.props;
        const {
            isActive,
            frequency,
            periodicity,
            lastRun,
            nextRun,
            event } = this.props.occurenceEvent.data;
        const period = renderPeriod(periodicity, frequency);

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
                            title={event.title}
                        >
                            <img src={event.photoUrl} className="w-100" />
                        </CardMedia>
                        <div className="text-block">
                            <CardContent>
                                <div className="title"> {event.title} </div>
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
                    <SelectiveForm />
                </div>
            </div>
        </>
    }
}
