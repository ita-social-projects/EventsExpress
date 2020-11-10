import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import Moment from 'react-moment';
import 'moment-timezone';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';


const useStyles = makeStyles(theme => ({
    card: {
        maxWidth: 345,
        maxHeight: 200,
        backgroundColor: theme.palette.primary.dark
    },
    media: {
        height: 0,
        paddingTop: '56.25%', // 16:9
    },
    expand: {
        transform: 'rotate(0deg)',
        marginLeft: 'auto',
        transition: theme.transitions.create('transform', {
            duration: theme.transitions.duration.shortest,
        }),
    },
    expandOpen: {
        transform: 'rotate(180deg)',
    },
    avatar: {
        backgroundColor: red[500],
    },
    button: {
    }
}));

export default class OccurenceEvent extends Component {

    render() {

        const classes = useStyles;
        const {id, isActive, frequency, periodicity, lastRun, nextRun, event} = this.props.item;

        return (
            <div className={"col-12 col-sm-8 col-md-6 col-xl-4 mt-3"}>
                <Card
                    className={classes.card}
                >
                    <CardHeader
                        title={id}
                        subheader={<Moment format="D MMM YYYY" withTitle>{lastRun}</Moment>}
                    />
                    <CardMedia
                        className={classes.media}
                        // title={event.id}
                    >
                        {/* <Link to={`/event/${event.id}/1`}>
                            <button>Event</button>
                        </Link> */}
                    </CardMedia>                
                    <CardContent>
                        <Typography variant="body2" color="textSecondary" component="p">
                            {periodicity, frequency}
                        </Typography>
                    </CardContent>
                    <CardActions disableSpacing>  
                    </CardActions>
                </Card>
            </div>
        );
    }
}
