import React from "react";
import EventType from "./EventType";
import ReasonsForUsingTheSite from "./ReasonsForUsingTheSite";
import parentStatusEnum from "../../constants/parentStatusEnum";
import relationShipStatusEnum from "../../constants/relationShipStatusEnum";
import theTypeOfLeisureEnum from "../../constants/theTypeOfLeisureEnum";
import { Grid, List, ListItem, ListItemText } from "@material-ui/core";

const MoreInfoList = (props) => {
    return (
        <>
            <Grid item xs={8}>
                <List>
                    <ListItem>
                        <ListItemText primary="Parent status:" />
                    </ListItem>
                    <ListItem>
                        <ListItemText
                            secondary={parentStatusEnum.find(
                                (i) => i.value === props.data.parentStatus
                            )}
                        />
                    </ListItem>

                    <ListItem>
                        <ListItemText primary="Reasons for using the site:" />
                    </ListItem>

                    <ListItem>
                        <ReasonsForUsingTheSite
                            data={props.data.reasonsForUsingTheSite}
                        />
                    </ListItem>

                    <ListItem>
                        <ListItemText primary="Event type:" />
                    </ListItem>

                    <ListItem>
                        <EventType data={props.data.eventType} />
                    </ListItem>

                    <ListItem>
                        <ListItemText primary="Relationship status:" />
                    </ListItem>

                    <ListItem>
                        <ListItemText
                            secondary={relationShipStatusEnum.find(
                                (i) => i.value === props.data.relationshipStatus
                            )}
                        />
                    </ListItem>

                    <ListItem>
                        <ListItemText primary="The type of leisure status:" />
                    </ListItem>

                    <ListItem>
                        <ListItemText
                            secondary={theTypeOfLeisureEnum.find(
                                (i) => i.value === props.data.typeOfLeisure
                            )}
                        />
                    </ListItem>
                </List>
            </Grid>
        </>
    );
};

export default MoreInfoList;
