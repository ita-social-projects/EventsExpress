import React, { Component } from 'react';
import Avatar from '@material-ui/core/Avatar';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import ButtonBase from '@material-ui/core/ButtonBase';
import genders from '../../constants/GenderConstants'
import './user-info.css'
const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        backgroundImage: "linear-gradient(90deg, #94d4ff, transparent)",
 
    },
    paper: {
        padding: theme.spacing(2),
        margin: 'auto',
        maxWidth: 500,
        
        
    },
    Avatar: {
        width: 128,
        height: 128,
    },
    img: {
        margin: 'auto',
        display: 'block',
        maxWidth: '100%',
        maxHeight: '100%',
    },
}));
 const getAge = (birthday) => {
    let today = new Date();
    let birthDate = new Date(birthday);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age = age - 1;
    }
    return age;
}
export default class UserInfoCard extends Component {
   

    render() {
        const { user } = this.props;
        const classes = useStyles;
        return (
            <>
                <br />
             
                <div className="row">
                    <div className="col-3"></div>
                    <div className="col-6">
                    <div className={classes.root}>
           
                    <Paper className={classes.paper}>
                        <Grid container spacing={1}>
                            <Grid item>
                                        <ButtonBase classN ame={classes.Avatar}>
                                    {user.photoUrl
                                                ? <Avatar className='MiddleAvatar' src={user.photoUrl} />
                                                : <Avatar className='MiddleAvatar' >{user.email.charAt(0).toUpperCase()}</Avatar>}
                                        </ButtonBase>
                            </Grid>
                            <Grid item xs={1} sm container>
                                <Grid item xs container direction="column" spacing={2}>
                                    <Grid item xs>
                                                <Typography gutterBottom variant="subtitle1">
                                                    {(user.username) ? user.username : user.email.substring(0, user.email.search("@"))}
                </Typography>
                                                <Typography variant="body2" gutterBottom>
                                                    {genders[user.gender]}
                </Typography>
                                                <Typography variant="body2" color="textSecondary">
                                                    Age:{getAge(user.birthday)}
                </Typography>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Grid>
                            </Paper>
                        </div>
                </div> 
                    <div className="col-3">
                    </div>
                </div>
                <br />
            </>


        )
    }
}
