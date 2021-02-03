import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import 'moment-timezone';
import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import genders from '../../constants/GenderConstants';
import Event from '../event/event-item';
import AddEventWrapper from '../../containers/add-event';
import EventsForProfile from '../event/events-for-profile';
import Spinner from '../spinner';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average';
import './User-profile.css';

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

export default class UserItemView extends Component {
    state = {
        value: 0
    };

    getAge = birthday => {
        let today = new Date();
        let birthDate = new Date(birthday);
        let age = today.getFullYear() - birthDate.getFullYear();
        let month = today.getMonth() - birthDate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        if (age > 150) return '---';
        return age;
    }

    renderCategories = arr => arr.map(item => <div key={item.id}>#{item.name}</div>)
    renderEvents = arr => arr.map(item => <div className="col-4"><Event key={item.id} item={item} /></div>)

    handleChange = (event, value) => {
        this.setState({ value });
        value === 0 && (this.props.onFuture())
        value === 1 && (this.props.onPast())
        value === 2 && (this.props.onVisited())
        value === 3 && (this.props.onToGo())
    };

    render() {
        const {
            userPhoto,
            name,
            email,
            birthday,
            gender,
            categories,
            id,
            attitude,
            rating
        } = this.props.data;
        const { isPending, data } = this.props.events;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending
            ? <EventsForProfile
                data_list={data.items}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                current_user={this.props.current_user}
                callback={
                    (this.state.value === 0) ? this.props.onFuture :
                        (this.state.value === 1) ? this.props.onPast :
                            (this.state.value === 2) ? this.props.onVisited :
                                (this.state.value === 3) ? this.props.onToGo : null}
            />
            : null;
        const categories_list = this.renderCategories(categories);
        const render_prop = (propName, value) => (
            <div className='row mb-3 font-weight-bold'>
                <div className='col-4'>{propName + ':'}</div>
                <div className='col-8'>
                    {value ? value : ''}
                </div>
            </div>
        )

        return <>
            <div className="info">
                {(id !== this.props.current_user)
                    ?
                    <div className="col-4 user">
                        <div className='d-flex flex-column justify-content-center align-items-center'>
                            <div className="user-profile-avatar">
                                <CustomAvatar size="big" name={name} photoUrl={userPhoto} />
                            </div>
                            <RatingAverage value={rating} direction='row' />

                            <div className="row justify-content-center">
                                <Tooltip title="Like this user" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton
                                        className={attitude == '0' ? 'text-success' : ''}
                                        onClick={attitude != '0' ? this.props.onLike : this.props.onReset}
                                    >
                                        <i className="fas fa-thumbs-up"></i>
                                    </IconButton>
                                </Tooltip>
                                <Tooltip title="Dislike this user" placement="bottom" TransitionComponent={Zoom}>
                                    <IconButton
                                        className={attitude == '1' ? 'text-danger' : ''}
                                        onClick={attitude != '1' ? this.props.onDislike : this.props.onReset}
                                    >
                                        <i className="fas fa-thumbs-down"></i>
                                    </IconButton>
                                </Tooltip>
                                <Tooltip title="Start chat!" placement="bottom" TransitionComponent={Zoom}>
                                    <Link to={`/chat/${id}`}>
                                        <IconButton>
                                            <i class="far fa-comments"></i>
                                        </IconButton>
                                    </Link>
                                </Tooltip>
                            </div>
                        </div>
                    </div>
                    : <div className="col-4"></div>
                }
                <div className='col-sm-12  col-md-6'>
                    {render_prop('User Name', name)}
                    {render_prop('Age', this.getAge(birthday))}
                    {render_prop('Gender', genders[gender])}
                    {render_prop('Email', email)}
                    {render_prop('Interests', categories_list)}
                </div>
            </div>
            <div className='mt-2'>
                <AppBar position="static" color="inherit">
                    <Tabs
                        className='w-100'
                        value={this.state.value}
                        onChange={this.handleChange}
                        variant="fullWidth"
                        scrollButtons="on"
                        indicatorColor="primary"
                        textColor="primary"
                    >
                        <Tab
                            label="Future events"
                            icon={
                                <IconButton
                                    color={this.state.value === 0 ? '' : 'primary'}>
                                    <i className="far fa-calendar-alt"></i>
                                </IconButton>}
                            {...a11yProps(0)}
                        />
                        <Tab
                            label="Archive events"
                            icon={
                                <IconButton
                                    color={this.state.value === 1 ? '' : 'primary'}>
                                    <i className="fas fa-archive"></i>
                                </IconButton>}
                            {...a11yProps(1)}
                        />
                        <Tab
                            label="Visited events"
                            icon={
                                <IconButton
                                    color={this.state.value === 2 ? '' : 'primary'}>
                                    <i className="fas fa-history"></i>
                                </IconButton>}
                            {...a11yProps(2)}
                        />
                        <Tab
                            label="Events to go"
                            icon={
                                <IconButton
                                    color={this.state.value === 3 ? '' : 'primary'}>
                                    <i className="fas fa-map-marker-alt"></i>
                                </IconButton>}
                            {...a11yProps(3)}
                        />
                        
                        
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
                {this.props.add_event_flag ?
                    <div className="shadow mb-5 bg-white rounded">
                        <AddEventWrapper onCreateCanceling={() => this.handleChange(null, 0)} />
                    </div>
                    :
                    <div className="shadow pl-2 pr-2 pb-2 mb-5 bg-white rounded">
                        {spinner}{content}
                        {!isPending && !(data.items && data.items.length > 0) &&
                            <div id="notfound" className="w-100">
                                <div className="notfound">
                                    <div className="notfound-404">
                                        <div className="h1">No Results</div>
                                    </div>
                                </div>
                            </div>}
                    </div>
                }
            </div>
        </>
    }
}
