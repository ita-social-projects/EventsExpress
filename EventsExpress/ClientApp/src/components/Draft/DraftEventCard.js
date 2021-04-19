import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import Moment from 'react-moment';
import 'moment-timezone';
import Card from '@material-ui/core/Card';
import { Button } from '@material-ui/core'
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Tooltip from '@material-ui/core/Tooltip';
import Badge from '@material-ui/core/Badge';
import CustomAvatar from '../avatar/custom-avatar';
import './event-item.css';
import { useStyle } from '../event/CardStyle'

const useStyles = useStyle;

export default class DraftEventCard extends Component {
    constructor(props) {
        super(props);

        this.state = {
            anchorEl: null
        }
    }

    render() {
        const classes = useStyles;
        const {
            id,
            title,
            dateFrom,
            description,
            photoUrl,
            owners
        } = this.props.item;    
        return (
            <div className={"col-12 col-sm-8 col-md-6 col-xl-4 mt-3"}>
                <Link to={`/editEvent/${id}/`}>
                <Card
                <Card
                    className={classes.card}
                >                 
                        <CardHeader
                            avatar={
                                <Button title={owners[0].username} className="btn-custom">
                                    <Badge overlap="circle" badgeContent={owners.length} color="primary">
                                        <CustomAvatar
                                            className={classes.avatar}
                                            photoUrl={owners[0].photoUrl}
                                            name={owners[0].username}
                                        />
                                    </Badge>
                                </Button>
                            }

                            title={title}
                            subheader={<Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment>}
                            classes={{ title: 'title' }}
                        />
                        <CardMedia
                            className={classes.media + ' d-flex justify-content-center'}
                            title={title}
                        >                                                    
                            {photoUrl &&
                                <img src={photoUrl} className="w-100" alt="Event" /> 
                            }
                            {photoUrl === null &&
                                <i class="far fa-images fa-10x" ></i>   
                            }
                        </CardMedia>
                    <CardContent>
                        {description &&
                            <Tooltip title={description.substr(0, 570) + (description.length > 570 ? '...' : '')} classes={{ tooltip: 'description-tooltip' }} >
                                <Typography variant="body2" color="textSecondary" className="description" component="p">
                                    {description.substr(0, 128)}
                                </Typography>
                            </Tooltip>
                        }
                    </CardContent>
                    </Card>
                </Link>
            </div>
        );
    }
}
