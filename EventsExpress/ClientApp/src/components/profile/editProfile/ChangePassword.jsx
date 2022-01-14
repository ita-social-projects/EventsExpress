import React from "react";
import { Field, reduxForm } from "redux-form";
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';
import { renderTextField } from '../../helpers/form-helpers';
import { fieldIsRequired } from '../../helpers/validators/required-fields-validator';
import {maxLength15, minLength6} from "../../helpers/validators/min-max-length-validators";

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

const validate = values => {
    const errors = {}
    const requiredFields = [
        'oldPassword',
        'newPassword',
        'repeatPassword',
    ]

    if (!values.newPassword){
        errors.newPassword = 'Password should be 6-15 characters long';
    }
    if (values.newPassword !== values.repeatPassword) {
        errors.repeatPassword = 'Passwords do not match';
    }
    return {
        ...fieldIsRequired(values, requiredFields),
        ...errors
    }
}

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
                <Typography className={classes.heading}>Change Password</Typography>

            </ExpansionPanelSummary>
            <ExpansionPanelDetails>

                <Typography>
                    <MuiThemeProvider>

                        <form onSubmit={handleSubmit}>
                            <div className="d-flex flex-column" >

                                <Field
                                    name="oldPassword"
                                    label="Input current password"
                                    component={renderTextField}
                                    type="password"
                                    className="mb-3"
                                />

                                <Field
                                    name="newPassword"
                                    label="Input new password"
                                    component={renderTextField}
                                    type="password"
                                    className="mb-3"
                                    validate={[maxLength15, minLength6]}
                                />

                                <Field
                                    name="repeatPassword"
                                    type="password"
                                    label="Repeat new password"
                                    component={renderTextField}
                                    className="mb-3"
                                />

                            </div>
                            {
                                props.error &&
                                <ErrorMessages error={props.error} className="text-center" />
                            }

                            <div>
                                <Button type="submit" color="primary" disabled={pristine || submitting}>
                                    Submit
                                </Button>
                                <Button type="button" disabled={pristine || submitting} onClick={reset}>
                                    Clear
                                </Button>
                            </div>
                        </form>

                    </MuiThemeProvider>
                </Typography>

            </ExpansionPanelDetails>

        </ExpansionPanel>
    );
};

export default reduxForm({
    form: "ChangePassword",
    validate

})(ChangePassword);