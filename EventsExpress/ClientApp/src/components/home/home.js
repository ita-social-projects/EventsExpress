import React, { Component } from 'react';
import './home.css';
import AddEventWrapper from '../../containers/add-event';
import EventListWrapper from '../../containers/event-list';

import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import Typography from '@material-ui/core/Typography';    
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { connect } from 'react-redux';


function AddComponent(props) {
    const [expanded, setExpanded] = React.useState(false);

    const handleChange = panel => (event, isExpanded) => {
        setExpanded(isExpanded ? panel : false);
    };
    return (
        <ExpansionPanel expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
        <ExpansionPanelSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
        >
            <Typography>Add Event</Typography>
        </ExpansionPanelSummary>
    <ExpansionPanelDetails>
        
        <Typography>
            <MuiThemeProvider>
                <AddEventWrapper />
            </MuiThemeProvider>
        </Typography>

    </ExpansionPanelDetails>

</ExpansionPanel>
    );
}


class Home extends Component{
    
    render(){
        

        
    return(
        <div>
            {this.props.id &&
            <AddComponent/>
            }
            <EventListWrapper match={this.props.match} /> 
          
        </div>
        
    );
    }
}

const mapStateToProps = state => {
    return {id: state.user.id };
  };

export default connect(mapStateToProps)(Home);