import React from "react";
import { Grid, List, ListItem, ListItemText } from "@material-ui/core";

const SelectedActivitiesList = ({ data }) => {
  return (
    <>
      <Grid item sm={5}>
        <h5 align="left">Selected activities</h5>
      </Grid>
      <Grid item sm={7} />
      <Grid item xs={5}>
        <List>
          {data.map((el) => (
            <ListItem>
              <ListItemText
                primary={el.group}
                secondary={el.categories.join(", ")}
              />
            </ListItem>
          ))}
        </List>
      </Grid>
    </>
  );
};

export default SelectedActivitiesList;
