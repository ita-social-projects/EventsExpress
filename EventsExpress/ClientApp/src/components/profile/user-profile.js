import React, { Component } from 'react';
import 'moment-timezone';
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
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import CustomAvatar from '../avatar/custom-avatar';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';

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
            current_user={this.props.current_user}
            callback={
                (this.state.value === 0) ? this.props.onFuture :
                    (this.state.value === 1) ? this.props.onPast :
                        (this.state.value === 2) ? this.props.onVisited :
                            (this.state.value === 3) ? this.props.onToGo : null}
        /> : null;

        const categories_list = this.renderCategories(categories);

        return <>
                <div className="row info">

                    <div className="col-3">
                        <h6><strong><p className="font-weight-bolder" key={name} >User Name:</p></strong></h6>
                        <h6><strong><p className="font-weight-bolder" >Age:</p></strong></h6>
                        <h6><strong><p className="font-weight-bolder" >Gender:</p></strong></h6>
                        <h6><strong><p className="font-weight-bolder" >Email:</p></strong></h6>
                        <h6><strong><p className="font-weight-bolder" >Interests:</p></strong></h6>
                    </div>
                    <div className="col-3">
                        {(name) ? <h6><strong><p className="font-weight-bolder" >{name}</p></strong></h6> : <h6><strong><p className="font-weight-bolder" >---</p></strong></h6>}
                        {(this.getAge(birthday)) ? <h6><strong><p className="font-weight-bolder" >{this.getAge(birthday)}</p></strong></h6> : <h6><strong><p className="font-weight-bolder" >---</p></strong></h6>}
                        {(genders[gender]) ? <h6><strong><p className="font-weight-bolder" >{genders[gender]}</p></strong></h6> : <h6><strong><p className="font-weight-bolder" >---</p></strong></h6>}
                        {(email) ? <h6><strong><p className="font-weight-bolder" >{email}</p></strong></h6> : <h6><strong><p className="font-weight-bolder" >---</p></strong></h6>}
                        {(categories_list) ? <h6><strong><p className="font-weight-bolder" >{categories_list}</p></strong></h6> : <h6><strong><p className="font-weight-bolder" >---</p></strong></h6>}
                    </div>
                    {(id !== this.props.current_user) &&
                        <div className="col-4 user">
                            <center>
                                <div className="user-profile-avatar">
                                    <CustomAvatar size="big" name={name} photoUrl={userPhoto} />
                                    <div className="msg-btn">
                                        <Link to={`/chat/${id}`}>
                                            <button className="btn btn-success mt-1">Write</button>
                                        </Link>
                                    </div>
                                </div>
                                <div className="row justify-content-center">
                                    <Tooltip title="Like this user" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton 
                                            className={attitude == '0' ? 'text-success' : ''}
                                            onClick={attitude != '0' ? this.props.onLike : this.props.onReset}
                                        >
                                            <i class="fas fa-thumbs-up"></i>
                                        </IconButton>
                                    </Tooltip>
                                    <Tooltip title="Dislike this user" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton
                                            className={attitude == '1' ? 'text-danger' : ''}
                                            onClick={attitude != '1' ? this.props.onDislike : this.props.onReset}
                                        >
                                            <i class="fas fa-thumbs-down"></i>
                                        </IconButton>
                                    </Tooltip>
                                </div>
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
                             </center>
                        </div>
                    }

                    {(id === this.props.current_user) &&
                        <div className="col-2">
                        </div>
                    }
                </div>
                <div className='mt-2'>
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
                            <Tab label="Events to go" icon={<HelpIcon />} {...a11yProps(3)} />
                            {(id === this.props.current_user) &&
                                <Tab label="Add event" icon={<ShoppingBasket />} {...a11yProps(4)} />
                            }
                        </Tabs>
                    </AppBar>

                    <TabPanel value={this.state.value} index={0}> </TabPanel>
                    <TabPanel value={this.state.value} index={1}> </TabPanel>
                    <TabPanel value={this.state.value} index={2}> </TabPanel>
                    <TabPanel value={this.state.value} index={3}> </TabPanel>
                    <TabPanel value={this.state.value} index={4}> </TabPanel>
                    {this.props.add_event_flag 
                        ? <div className="shadow mb-5 bg-white rounded">
                            <AddEventWrapper />
                        </div>
                        : <div className="shadow pl-2 pr-2 mb-5 bg-white rounded">
                            {spinner}
                            {content}
                            {!isPending && !(data.items && data.items.length > 0) && 
                                <p className="font-weight-bold p-9" align="center" >No events yet!</p>
                            }
                        </div>
                    }
                </div>
                  
        </>
    }
}

