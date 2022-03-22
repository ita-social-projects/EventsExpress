import React from 'react'
import { List, ListItem, ListItemText } from "@material-ui/core";
import maps from "../../maps";

const PreferencesList = ({ formValues }) => {
    const DisplayFormItems = ({ label, source, map }) => (
        <>
            <ListItem>
                <ListItemText primary={label} />
            </ListItem>
            <div className="d-flex flex-row">
                {source &&
                    source.map(value => (
                        <ListItem key={map.get(value)} className="py-0">
                            <ListItemText secondary={map.get(value)} />
                        </ListItem>
                    ))}
            </div>
        </>
    );

    const DisplaySelectedOption = ({ label, source, map }) => (
        <ListItem>
            <ListItemText
                primary={label}
                secondary={typeof source !== 'undefined' && map.get(source)}
            />
        </ListItem>
    );

    return (
        <List>
            <DisplaySelectedOption
                label="Parental status"
                source={formValues.parentStatus}
                map={maps.parenting}
            />
            <DisplayFormItems
                label="Reasons for using the site"
                source={formValues.reasonsForUsingTheSite}
                map={maps.reasons}
            />
            <DisplayFormItems
                label="Event preferences"
                source={formValues.eventTypes}
                map={maps.eventPreferences}
            />
            <DisplaySelectedOption
                label="Relationship status"
                source={formValues.relationshipStatus}
                map={maps.relationshipStatuses}
            />
            <DisplaySelectedOption
                label="Preferred type of leisure"
                source={formValues.leisureType}
                map={maps.leisureTypes}
            />
        </List>
    );
};

export default PreferencesList;
