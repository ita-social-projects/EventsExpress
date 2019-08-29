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
import Avatar from '@material-ui/core/Avatar';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import Tooltip from '@material-ui/core/Tooltip'; 
import Badge from '@material-ui/core/Badge';
import EventManagmentWrapper from '../../containers/event-managment';


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
    button:{
    }
  }));

export default class Event extends Component {

    renderCategories = (arr) => {
        return arr.map((x) => (<span key={x.id}>#{x.name}</span>)
        );
    }
      
    render() {        
        const classes = useStyles;
      
        const { id, title, dateFrom, comment_count, description, photoUrl, categories, user, countVisitor, isBlocked  } = this.props.item;
        const { city, country } = this.props.item;
    
        const rateTextColor = (user.rating < 5) 
        ? 'text-danger' 
        : (user.rating < 8) 
            ? 'text-warning'
            : 'text-success';



        return (
            <div className="col-4 mt-3">
            <Card className={classes.card}>
                <CardHeader
                    avatar={
                            <Tooltip title={user.username}>
                                <Link to={'/user/' + user.id} className="btn-custom">
                                    <Avatar 
                                        aria-label="recipe"
                                        src={user.photoUrl}
                                        className={classes.avatar} 
                                    >
                                        {user.username[0].toUpperCase()}
                                    </Avatar>
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
                    <img src={photoUrl} />
                </CardMedia>

                <CardContent>
                    <Typography variant="body2" color="textSecondary" component="p">
                        {description.substr(0, 128) + '...'}
                    </Typography>
                </CardContent>
                <CardActions disableSpacing>
                    <div className="flex flex-column">                        
                        <Link to={'/event/'+id+'/'+1}>
                            <IconButton className={classes.button} aria-label="view">
                                <i className="fa fa-eye"></i>
                            </IconButton>
                        </Link>
                        {this.renderCategories(categories.slice(0,2))}
                    </div>
                    {(this.props.current_user.role=="Admin")
                        ? <EventManagmentWrapper eventItem={this.props.item} />
                        : null
                    }
                        
                </CardActions>
            </Card>
            </div>
        );
    }
}