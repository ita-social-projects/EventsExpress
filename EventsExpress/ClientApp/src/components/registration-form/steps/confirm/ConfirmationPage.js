import React, { useContext } from 'react';
import moment from 'moment';
import { connect } from 'react-redux';
import { reduxForm, getFormValues } from 'redux-form';
import { Grid, List, ListItem, ListItemText, Button } from '@material-ui/core';
import reasonsForUsingTheSiteEnum from '../../../../constants/reasonsForUsingTheSiteEnum';
import eventTypeEnum from '../../../../constants/eventTypeEnum';
import genders from '../../../../constants/GenderConstants';
import SelectedActivitiesList from '../activities/SelectedActivitiesList';
import { RegisterStepContext } from '../../RegistrationForm';

// TODO: extract to reduce duplication in components
const reasonsMap = new Map([
    [reasonsForUsingTheSiteEnum.BeMoreActive, 'Be more active'],
    [reasonsForUsingTheSiteEnum.DevelopASkill, 'Develop a skill'],
    [reasonsForUsingTheSiteEnum.MeetPeopleLikeMe, 'Meet people like me'],
]);

const eventPreferencesMap = new Map([
    [eventTypeEnum.AnyDistance, 'Any distance'],
    [eventTypeEnum.Free, 'Free'],
    [eventTypeEnum.NearMe, 'Near me'],
    [eventTypeEnum.Offline, 'Offline'],
    [eventTypeEnum.Online, 'Online'],
    [eventTypeEnum.Paid, 'Paid'],
]);

// TODO: extract styles
const ConfirmationPage = ({
    formValues,
    pristine,
    submitting,
    categories,
    categoryGroups,
}) => {
    const { goBack, goToNext } = useContext(RegisterStepContext);

    const Items = ({ source, map }) => (
        <div className="d-flex flex-row">
            {source &&
                source.map(value => (
                    <ListItem key={map.get(value)}>
                        <ListItemText secondary={map.get(value)} />
                    </ListItem>
                ))}
        </div>
    );

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
        <div style={{ width: '97%', padding: '10px' }}>
            <form onSubmit={goToNext}>
                <Grid container spacing={3}>
                    <Grid item sm={7}>
                        <h1 style={{ fontSize: 20 }}>
                            Step 5: Confirm your user data
                        </h1>
                    </Grid>
                    <Grid item sm={5} />
                    <Grid item xs={4}>
                        <List>
                            <ListItem>
                                <ListItemText primary="Email" />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="First Name"
                                    secondary={formValues.firstName}
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="Last Name"
                                    secondary={formValues.lastName}
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    primary="Birth Date"
                                    secondary={
                                        formValues.birthDate
                                            ? moment(
                                                  formValues.birthDate
                                              ).format('DD-MM-YYYY')
                                            : 'Not entered.'
                                    }
                                />
                            </ListItem>
                        </List>
                    </Grid>
                    <Grid item xs={3}>
                        <List>
                            <ListItem>
                                <ListItemText
                                    primary="Gender"
                                    secondary={genders[formValues.gender]}
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
                                    primary="City"
                                    secondary={formValues.city}
                                />
                            </ListItem>
                        </List>
                    </Grid>
                    <Grid item xs={5}>
                        <List>
                            <ListItem>
                                <ListItemText primary="Parental status:" />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    secondary={
                                        formValues.parentstatus
                                            ? 'No kids'
                                            : 'With kids'
                                    }
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="Reasons for using the site:" />
                            </ListItem>
                            <Items
                                source={formValues.reasonsForUsingTheSite}
                                map={reasonsMap}
                            />
                            <ListItem>
                                <ListItemText primary="Event preferences:" />
                            </ListItem>
                            <Items
                                source={formValues.eventType}
                                map={eventPreferencesMap}
                            />
                            <ListItem>
                                <ListItemText primary="Relationship status:" />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    secondary={
                                        formValues.relationshipstatus
                                            ? 'In a relationship'
                                            : 'Single'
                                    }
                                />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="The type of leisure status:" />
                            </ListItem>
                            <ListItem>
                                <ListItemText
                                    secondary={
                                        formValues.thetypeofleisure
                                            ? 'Passive'
                                            : 'Active'
                                    }
                                />
                            </ListItem>
                        </List>
                    </Grid>
                    {formValues.categories && (
                        <SelectedActivitiesList
                            data={getSelectedCategories()}
                        />
                    )}
                    <Grid item sm={12}>
                        <Button
                            type="button"
                            className="previous"
                            onClick={goBack}
                            color="primary"
                            variant="text"
                            size="large"
                        >
                            Back
                        </Button>
                        <Button
                            type="submit"
                            className="next"
                            disabled={pristine || submitting}
                            color="primary"
                            variant="contained"
                            size="large"
                        >
                            Confirm
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </div>
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
