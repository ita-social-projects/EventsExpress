import React, { useContext } from 'react';
import { connect } from 'react-redux';
import { reduxForm, getFormValues } from 'redux-form';
import { Grid, List, ListItem, ListItemText } from '@material-ui/core';
import moment from 'moment';
import genders from '../../../../constants/GenderConstants';
import SelectedActivitiesList from '../activities/SelectedActivitiesList';
import { RegisterStepContext } from '../../RegistrationForm';
import StepperNavigation from '../../StepperNavigation';
import PreferencesList from './PreferencesList';

const ConfirmationPage = ({ formValues, categories, categoryGroups }) => {
    const { goToNext } = useContext(RegisterStepContext);

    const getSelectedCategories = () => {
        const filteredCategories = categories.filter(el =>
            formValues.categories.includes(el.id)
        );

        return categoryGroups
            .map(g => ({
                group: g.title,
                categories: filteredCategories
                    .filter(c => c.categoryGroup.id === g.id)
                    .map(el => el.name),
            }))
            .filter(el => el.categories.length > 0);
    };

    return (
        <form onSubmit={goToNext}>
            <Grid container spacing={3}>
                <Grid item sm={12}>
                    <h1>Step 5: Confirm your user data</h1>
                </Grid>
                <Grid item container xs={6}>
                    <Grid item xs={6}>
                        <List>
                            <ListItem>
                                <ListItemText
                                    primary="First Name"
                                    secondary={formValues.firstName}
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="Country"
                                    secondary={formValues.country}
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="Birth Date"
                                    secondary={
                                        formValues.birthDate
                                            && moment(formValues.birthDate).format('DD-MM-YYYY')
                                    }
                                />
                            </ListItem>
                        </List>
                    </Grid>
                    <Grid item xs={6}>
                        <List>
                            <ListItem>
                                <ListItemText
                                    primary="Last Name"
                                    secondary={formValues.lastName}
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="City"
                                    secondary={formValues.city}
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="Gender"
                                    secondary={genders[formValues.gender]}
                                />
                            </ListItem>
                        </List>
                    </Grid>
                    <Grid item xs={12}>
                        <ListItem>
                            <ListItemText
                                primary="Additional information"
                                secondary={formValues.additionalInfo}
                            />
                        </ListItem>
                    </Grid>
                </Grid>
                <Grid item xs={6}>
                    <PreferencesList formValues={formValues} />
                </Grid>
                <Grid item xs={12} />
            </Grid>
            {formValues.categories && (
                <SelectedActivitiesList
                    data={getSelectedCategories()}
                />
            )}
            <div className="stepper-submit">
                <StepperNavigation
                    showSkip={false}
                    hasNext={false}
                />
            </div>
        </form>
    );
};

const mapStateToProps = state => ({
    formValues: getFormValues('registrationForm')(state),
    categoryGroups: state.categoryGroups.data,
    categories: state.categories.data,
});

export default connect(mapStateToProps)(
    reduxForm({
        form: 'registrationForm',
        destroyOnUnmount: false,
        forceUnregisterOnUnmount: true,
    })(ConfirmationPage)
);
