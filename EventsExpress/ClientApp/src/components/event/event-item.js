import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import Moment from 'react-moment';
import 'moment-timezone';
import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Collapse from '@material-ui/core/Collapse';
import Avatar from '@material-ui/core/Avatar';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import FavoriteIcon from '@material-ui/icons/Favorite';
import ShareIcon from '@material-ui/icons/Share';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import IconDecorator from '@material-ui/core/Icon';
import Tooltip from '@material-ui/core/Tooltip';
import Badge from '@material-ui/core/Badge';
import SocialShare from '././share/ShareMenu'
import EventManagmentWrapper from '../../containers/event-managment';
import CustomAvatar from '../avatar/custom-avatar';

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

export default class Event extends Component {
    renderCategories = (arr) => {
        return arr.map((x) => (<div key={x.id}>#{x.name}</div>)
        );
    }

    render() {
        const classes = useStyles;
        const { id, title, dateFrom, comment_count, description, photoUrl, categories, user, countVisitor, isBlocked } = this.props.item;
        const { city, country } = this.props.item;

        return (
            <div className={"col-12 col-sm-8 col-md-6 col-xl-4 mt-3"}>
                <Card className={classes.card} style={{ backgroundColor: (isBlocked) ? "gold" : "" }}>
                    <CardHeader
                        avatar={
                            <Tooltip title={user.username}>
                                <Link to={'/user/' + user.id} className="btn-custom">
                                    <CustomAvatar className={classes.avatar} photoUrl={user.photoUrl} name={user.username} />
                                </Link>
                            </Tooltip>
                        }

                        action={
                            <Tooltip title="Visitors">
                                <IconButton>
                                    <Badge badgeContent={countVisitor} color="primary">
                                        <i className="fa fa-users"></i>
                                    </Badge>
                                </IconButton>
                            </Tooltip>
                        }
                        title={title}
                        subheader={<Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>}
                    />
                    <CardMedia
                        className={classes.media}
                        title={title}
                    >
                        <Link to={'/event/' + id + '/' + 1}>
                            <img src={photoUrl} className="w-100" />
                        </Link>
                    </CardMedia>
                    <CardContent>
                        <Typography variant="body2" color="textSecondary" component="p">
                            {description.substr(0, 128) + '...'}
                        </Typography>
                    </CardContent>
                    <CardActions disableSpacing>
                        <div className='w-100'>
                            <div className='mb-2'>
                                {country + ' ' + city}
                            </div>
                            <div className="float-left">
                                {this.renderCategories(categories.slice(0, 2))}
                            </div>
                            <div className='d-flex flex-row align-items-center justify-content-center float-right'>
                                <Link to={'/event/' + id + '/' + 1}>
                                    <IconButton className={classes.button} aria-label="view">
                                        <i className="fa fa-eye"></i>
                                    </IconButton>
                                </Link>
                                {(this.props.current_user != null && this.props.current_user.role == "Admin")
                                    ? <EventManagmentWrapper eventItem={this.props.item} />
                                    : null
                                }
                                <SocialShare href={'https://eventsexpress.azurewebsites.net/event/' + id + '/' + 1} />
                            </div>
                        </div>
                    </CardActions>
                </Card>
            </div>
        );
    }
}
