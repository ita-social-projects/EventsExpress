import React, { Component } from 'react';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

class EventVisitors extends Component {
    render() {
        const {isMyPrivateEvent, visitors, admins } = this.props;

        return (
            <div>
                <ExpansionPanel>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                        >
                        <Typography>Admins</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails>
                        <Typography>
                            {this.props.renderOwners(admins)}
                        </Typography>
                    </ExpansionPanelDetails>
                </ExpansionPanel>
                <ExpansionPanel disabled={visitors.approvedUsers.length == 0}>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                        >
                        <Typography>Visitors</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails>
                        <Typography>
                            {this.props.renderApprovedUsers(visitors.approvedUsers, isMyPrivateEvent)}
                        </Typography>
                    </ExpansionPanelDetails>
                </ExpansionPanel> 
                {isMyPrivateEvent && 
                    <ExpansionPanel disabled={visitors.pendingUsers.length == 0}>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                        >
                        <Typography>Pending users</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails>
                        <Typography>
                            {this.props.renderPendingUsers(visitors.pendingUsers)}
                        </Typography>
                    </ExpansionPanelDetails>
                </ExpansionPanel> 
                }
                {isMyPrivateEvent &&
                    <ExpansionPanel disabled={visitors.deniedUsers.length == 0}>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                        >
                        <Typography>Denied users</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails>
                        <Typography>
                            {this.props.renderDeniedUsers(visitors.deniedUsers)}
                        </Typography>
                    </ExpansionPanelDetails>
                </ExpansionPanel> 
                }
            </div>
        )
    }
}

export default EventVisitors;