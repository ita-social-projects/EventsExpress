import React, { useEffect, useState } from 'react';
import { Button, Card, CardActions, CardContent, CardMedia, Icon, Typography } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { eventDefaultImage } from '../../../../constants/eventDefaultImage';
import PhotoService from '../../../../services/PhotoService';
import { makeStyles } from '@material-ui/core/styles';
import Moment from 'react-moment';

const useStyles = makeStyles({
    root: {
        display: 'flex',
        flexDirection: 'column'
    },
    header: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center'
    },
    actions: {
        marginTop: 'auto'
    },
    button: {
        width: '100%',
        display: 'inline-flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        textTransform: 'none',
    }
});

export const EventCarouselCard = ({ event }) => {
    const classes = useStyles();
    const { id, title, dateFrom, description } = event;
    const [image, setImage] = useState(eventDefaultImage);

    useEffect(() => {
        new PhotoService().getPreviewEventPhoto(id).then(eventPreviewImage => {
            if (eventPreviewImage != null) {
                setImage(URL.createObjectURL(eventPreviewImage));
            }
        });
    }, []);

    return (
        <Card className={classes.root}>
            <CardMedia
                title={title}
                image="/static/images/cards/contemplative-reptile.jpg"
            >
                <Link to={`/event/${id}/1`} id="LinkToEvent">
                    <img
                        src={image}
                        id={"eventPreviewPhotoImg" + id} alt="Event"
                        className="w-100"
                    />
                </Link>
            </CardMedia>
            <CardContent>
                <div className={classes.header}>
                    <Typography variant="h5" component="h2">
                        {title}
                    </Typography>
                    <div>
                        <Icon className="fas fa-bookmark" />
                    </div>
                </div>
                <Typography variant="body2" color="textSecondary" gutterBottom={true}>
                    <Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>
                </Typography>
                <Typography variant="body2" color="textSecondary">
                    {description}
                </Typography>
            </CardContent>
            <CardActions className={classes.actions}>
                <Button component={Link} to={`/event/${id}/1`} className={classes.button} size="small" color="primary">
                    <span>
                        View event details
                    </span>
                    <Icon className="fas fa-arrow-right" />
                </Button>
            </CardActions>
        </Card>
    );
};
