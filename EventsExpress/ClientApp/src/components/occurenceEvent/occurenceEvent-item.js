import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import Moment from 'react-moment';
import 'moment-timezone';
import { renderPeriod } from '../occurenceEvent/render-period'
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Typography from '@material-ui/core/Typography';
import { useStyles } from '../occurenceEvent/card-style-const'


export default class OccurenceEvent extends Component {

    render() {

        const classes = useStyles;
        const {
            id,
            isActive,
            frequency,
            periodicity,
            lastRun,
            nextRun,
            event } = this.props.item;
        const period = renderPeriod(periodicity, frequency);
        return (
            <div className={"col-12 col-sm-8 col-md-6 col-xl-4 mt-3"}>
                <Card
                    className={classes.card}
                >
                    <CardHeader
                        title={event.title}
                        subheader={<Moment format="D MMM YYYY" withTitle>{lastRun}</Moment>}
                    />
                    <CardMedia
                        className={classes.media}
                        title={event.title}
                    >
                        <Link to={`/occurenceEvent/${id}`}>
                            <img src={event.photoUrl} className="w-100" />
                        </Link>
                    </CardMedia>
                    <CardContent>
                        <Typography
                            variant="body2"
                            color="textSecondary"
                            component="p"
                        >
                            {period}
                        </Typography>
                        <Moment format="D MMM YYYY" withTitle>{nextRun}</Moment>
                        <Link to={`/event/${event.id}/1`}>
                            <Button
                                className="ml-2"
                                style={{ background: '#3f51b50a' }}
                                fullWidth={false}
                                color="primary"
                                type="submit"
                                onClick={this.disableCreateButton}>
                                Go to Parent Event
                        </Button>
                        </Link>
                    </CardContent>
                    <CardActions disableSpacing>
                    </CardActions>
                </Card>
            </div>
        );
    }
}
