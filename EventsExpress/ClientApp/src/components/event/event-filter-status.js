import React, { Component } from 'react';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import { withStyles } from '@material-ui/core/styles';
import { purple } from '@material-ui/core/colors';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import StatusHistory from '../helpers/EventStatusEnum';

const PurpleSwitch = withStyles({
    switchBase: {
      color: purple[300],
      '&$checked': {
        color: purple[500],
      },
      '&$checked + $track': {
        backgroundColor: purple[500],
      },
    },
    checked: {},
    track: {},
  })(Switch);

class EventFilterStatus extends Component {
    
    render() {
        const { input: { value, onChange } } = this.props;
        const all = value[StatusHistory.Active] && value[StatusHistory.Blocked] && value[StatusHistory.Canceled];
        return (
            <div>
                <ExpansionPanel>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel-content"
                        id="panel-header"
                        >
                        <Typography>Details of event</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails>
                        <Typography className = "w-100">
                            <FormGroup>
                                <FormControlLabel
                                    control={<PurpleSwitch />}
                                    checked={this.props.status.all}
                                    onChange={onChange}
                                    label="All"
                                    name="all"
                                />
                                <FormControlLabel
                                    control={<PurpleSwitch />}
                                    checked={this.props.status.active}
                                    onChange={onChange}
                                    label="Active"
                                    name="active"
                                />
                                <FormControlLabel
                                    control={<PurpleSwitch />}
                                    checked={this.props.status.blocked}
                                    onChange={onChange}
                                    label="Blocked"
                                    name="blocked"
                                />
                                <FormControlLabel
                                    control={<PurpleSwitch />}
                                    checked={this.props.status.canceled}
                                    onChange={onChange}
                                    label="Canceled"
                                    name="canceled"
                                />
                            </FormGroup>
                        </Typography>
                    </ExpansionPanelDetails>
                </ExpansionPanel>
            </div>
        )
    }
}

export default EventFilterStatus;