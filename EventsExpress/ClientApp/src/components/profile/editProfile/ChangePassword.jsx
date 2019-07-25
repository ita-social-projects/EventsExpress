import React from "react";
import { Field, reduxForm } from "redux-form";
import TextField from "material-ui/TextField";
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import { makeStyles } from "@material-ui/core/styles";

const renderTextField = ({
    input,
    label,
    meta: { touched, error },
    ...custom
}) => (
        <TextField
            hintText={label}
            floatingLabelText={label}
            errorText={touched && error}
            {...input}
            {...custom}
        />
    );

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
    },
    heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
    },
    secondaryHeading: {
        fontSize: theme.typography.pxToRem(15),
        color: theme.palette.text.secondary,
    },
}));

const ChangePassword = (props) => {
    const { handleSubmit, pristine, reset, submitting } = props;
    const classes = useStyles();

    const [expanded, setExpanded] = React.useState(false);

    const handleChange = panel => (event, isExpanded) => {
        setExpanded(isExpanded ? panel : false);
    };

    return (


        <ExpansionPanel expanded={expanded === 'panel5'} onChange={handleChange('panel5')}>
            <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1bh-content"
                id="panel1bh-header"
            >
                <Typography className={classes.heading}>ChangePassword</Typography>
                
            </ExpansionPanelSummary>
            <ExpansionPanelDetails>

                <Typography>
                    <MuiThemeProvider>

                        <form onSubmit={handleSubmit}>
                            <div>
                                <Field
                                    name="currentPassword"
                                    label="Input current password"
                                    component={renderTextField}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                />

                                <Field
                                    name="newPassword"
                                    label="Input new password"
                                    component={renderTextField}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                />

                                <Field
                                    name="repeatPassword"
                                    label="Repeat new password"
                                    component={renderTextField}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                />

                            </div>


                            <div>
                                <button type="submit" disabled={pristine || submitting}>
                                    Submit
                                </button>
                                <button type="button" disabled={pristine || submitting} onClick={reset}>
                                    Clear
                                </button>
                            </div>
                        </form>

                    </MuiThemeProvider>
                </Typography>

            </ExpansionPanelDetails>

        </ExpansionPanel>


        
    );
};

export default ChangePassword({
    form: "ChangePassword" // a unique identifier for this form
})(ChangePassword);