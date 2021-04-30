import React, { Component } from 'react';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

export default class ParticipantGroup extends Component {


    render() {
        const { label,
                renderGroup,
                disabled,
        } = this.props;

        return (
            <ExpansionPanel disabled={disabled}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1a-content"
                    id="panel1a-header"
                >
                    <Typography>{ label }</Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography className="w-100">
                        {
                            renderGroup()
                        }
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
        )
    }
}
