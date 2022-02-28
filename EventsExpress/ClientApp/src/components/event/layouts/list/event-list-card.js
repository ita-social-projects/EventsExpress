import React from 'react';
import { Card, CardContent, Icon, Typography } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import Moment from 'react-moment';
import DisplayLocation from '../../map/display-location';

const useStyles = makeStyles({
    root: {
        position: 'relative',
        margin: '10px'
    },
    cardContent: {
        display: 'inline-block',
        padding: '0px 80px 0px 20px',
        verticalAlign: 'top',
    },
    bookmarkStyle: {
        position: 'absolute',
        right: '10px',
        top: '10px'
    },
    activityStyle: {
        paddingTop: '30px'
    },
    numberOfAttendiesStyle: {
        paddingRight: '100px',
        float: 'right'
    }
});

export const EventListCard = ({ event }) => {
    const classes = useStyles();
    const { id, title, dateFrom, countVisitor, categories, location } = event;

    return (
        <Link to={`/event/${id}/1`} id="LinkToEvent">
            <Card className={classes.root}>
                <CardContent>
                    <div className={classes.cardContent}>
                        <Typography variant="h5" component="h2" gutterBottom={true}>
                            <Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>
                        </Typography>
                    </div>
                    <div className={classes.cardContent} >
                        <Typography variant="h5" component="h2">
                            {title}
                        </Typography>
                        <div className={classes.activityStyle}>
                            {categories.map(category => (
                                <Typography variant="h5" component="h2">
                                    {category.name}
                                </Typography>
                            ))}
                            {location &&
                                <DisplayLocation
                                    location={location}
                                />
                            }
                        </div>
                    </div>
                    <div className={classes.cardContent + ' ' + classes.numberOfAttendiesStyle}>
                        <Typography variant="h5" component="h2">
                            Number of attendies: {countVisitor}
                        </Typography>
                    </div>
                    <div className={classes.bookmarkStyle}>
                        <Icon className="fas fa-bookmark" />
                    </div>
                </CardContent>
            </Card>
        </Link>
    );
};
