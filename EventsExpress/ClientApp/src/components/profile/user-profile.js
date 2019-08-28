import React, { Component } from 'react';
import 'moment-timezone';
import Avatar from '@material-ui/core/Avatar';
import genders from '../../constants/GenderConstants';
import Event from '../event/event-item';
import AddEventWrapper from '../../containers/add-event';
import './User-profile.css';
import EventsForProfile from '../event/events-for-profile';
import Spinner from '../spinner';
import { Link } from 'react-router-dom'
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import PhoneIcon from '@material-ui/icons/Phone';
import FavoriteIcon from '@material-ui/icons/Favorite';
import PersonPinIcon from '@material-ui/icons/PersonPin';
import HelpIcon from '@material-ui/icons/Help';
import ShoppingBasket from '@material-ui/icons/ShoppingBasket';
import ThumbDown from '@material-ui/icons/ThumbDown';
import ThumbUp from '@material-ui/icons/ThumbUp';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';

function TabPanel(props) {
    const { children, value, index, ...other } = props;

    return (
        <Typography
            component="div"
            role="tabpanel"
            hidden={value !== index}
            id={`scrollable-force-tabpanel-${index}`}
            aria-labelledby={`scrollable-force-tab-${index}`}
            {...other}
        >
            <Box p={3}>{children}</Box>
        </Typography>
    );
}

TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.any.isRequired,
    value: PropTypes.any.isRequired,
};

function a11yProps(index) {
    return {
        id: `full-width-tab-${index}`,
        'aria-controls': `full-width-tabpanel-${index}`,
    };
}

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        width: '100%',
        backgroundColor: theme.palette.background.paper,
    },
}));

export default class UserItemView extends Component {
    state = {
        value: 0
    };
    getAge = birthday => {
        let today = new Date();
        let birthDate = new Date(birthday);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        return age;
    }
    
    
    renderCategories = arr => arr.map(item => <span key={item.id}>#{item.name}</span>)
    renderEvents = arr => arr.map(item => <div className="col-4"><Event key={item.id} item={item} /></div>)

    
    handleChange = (event, value) => {
        this.setState({ value });
        value === 0 && (this.props.onFuture())
        value === 1 && (this.props.onPast())
        value === 2 && (this.props.onVisited())
        value === 3 && (this.props.onToGo())
        value === 4 && (this.props.onAddEvent())     
    };


    render() {
        const classes = useStyles;
        const { userPhoto, name, email, birthday, gender, categories, id, attitude } = this.props.data;
        const { isPending, data } = this.props.events;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventsForProfile
            data_list={data.items}
            page={data.pageViewModel.pageNumber}
            totalPages={data.pageViewModel.totalPages}
            callback={
                (this.state.value === 0) ? this.props.onFuture :
                    (this.state.value === 1) ? this.props.onPast :
                        (this.state.value === 2) ? this.props.onVisited :
                            (this.state.value === 3) ? this.props.onToGo : null}
            /> : null;

        const categories_list = this.renderCategories(categories);
       
        return <>
            <div className="row box info">
                
                <div className="col-3">
                    <h6><strong><p className="font-weight-bolder" >User Name:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Age:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Gender:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Email:</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >Interests:</p></strong></h6>
                </div>
                <div className="col-3">
                    <h6><strong><p className="font-weight-bolder" >{name}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{this.getAge(birthday)}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{genders[gender]}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{email}</p></strong></h6>
                    <h6><strong><p className="font-weight-bolder" >{categories_list}</p></strong></h6>
                </div>
                {(id !== this.props.current_user) &&
                    <div className="col-3">
                        <div className="d-flex align-items-center attitude">
                            <Avatar
                                alt="Тут аватар"
                                src={userPhoto}

                                className='bigAvatar'
                            />
                        </div>
                        <center>
                        {attitude == '2' && <div className="row attitude">
                            <button onClick={this.props.onLike} className="btn btn-info">Like</button>
                            <button onClick={this.props.onDislike} className="btn btn-info">Dislike</button>
                        </div>}
                        {attitude == '1' && <div className="row attitude">
                            <button onClick={this.props.onLike} className="btn btn-info">Like</button>
                            <button className="btn btn-light">Dislike</button>
                            <button onClick={this.props.onReset} className="btn btn-info">Reset</button>
                        </div>}
                        {attitude == '0' && <div className="row attitude">
                            <button className="btn btn-light">Like</button>
                            <button onClick={this.props.onDislike} className="btn btn-info">Dislike</button>
                            <button onClick={this.props.onReset} className="btn btn-info">Reset</button>
                        </div>}
                        <Link to={`/chat/${id}`}><button className="btn btn-info mt-1">Message</button></Link>
                        </center>
                    </div>
                }

                {(id === this.props.current_user) &&
                    <div className="col-2">
                    </div>
                }
            </div>
            <div className={classes.root}>
                <AppBar position="static" color="inherit">
                    <Tabs
                        value={this.state.value}
                        onChange={this.handleChange}
                        variant="fullWidth"
                        scrollButtons="on"
                        indicatorColor="primary"
                        textColor="primary"
                        aria-label="scrollable force tabs example"
                    >
                        <Tab label="Future events" icon={<PhoneIcon />} {...a11yProps(0)} />
                        <Tab label="Archive events" icon={<FavoriteIcon />} {...a11yProps(1)} />
                        <Tab label="Visited events" icon={<PersonPinIcon />} {...a11yProps(2)} />
                        <Tab label="Events to go"  icon={<HelpIcon />} {...a11yProps(3)} />
                        {(id === this.props.current_user)&&
                            <Tab label="Add event" icon={<ShoppingBasket />} {...a11yProps(4)} /> 
                        }
                    </Tabs>
                </AppBar>
                <TabPanel value={this.state.value} index={0}> 
                </TabPanel>
                <TabPanel value={this.state.value} index={1}>
                </TabPanel>
                <TabPanel value={this.state.value} index={2}>
                </TabPanel>
                <TabPanel value={this.state.value} index={3}>     
                </TabPanel>
                <TabPanel value={this.state.value} index={4}>
                </TabPanel>
                
            </div>
                    {this.props.add_event_flag ? 
                    <div className="row shadow p-5 mb-5 bg-white rounded">
                    <AddEventWrapper /> 
                     </div>
                    :
                <div className="shadow p-5 mb-5 bg-white rounded">
                    {(data.items && data.items.length > 0) ? <>{spinner}{content}</> : <h4><strong><p className="font-weight-bold p-9" align="center">No events yet!</p></strong></h4>}
                    </div>
                    }
        </>
    }
}

