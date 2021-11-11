import React, { useEffect } from "react";
import { Grid, Button } from "@material-ui/core";
import { connect } from "react-redux";
import { reduxForm } from "redux-form";
import get_category_groups from "../../actions/categoryGroup/category-group-list-action";
import get_categories from "../../actions/category/category-list-action";
import TileGroup from "../../containers/TileGroup";

const ChooseActivities = (props) => {
  const { handleSubmit, previousPage } = props;

  useEffect(() => {
    props.getCategoryGroups();
    props.getCategories();
  }, []);

  const mapToCategories = () => {
    const groups = props.categoryGroups;
    const categories = props.categories;

    return groups.map((el) => ({
      group: el,
      categories: categories.filter((c) => c.categoryGroupId === el.id),
    }));
  };

  return (
    <>
      <div style={{ width: "97%", padding: "10px" }}>
        <h1 style={{ textAlign: "left", marginBottom: "20px" }}>
          What are your reasons for joining EventsExpress?
        </h1>
        <h4 style={{ textAlign: "left" }}>
          (you can skip this step and choose activities later in Profile
          Settings)
        </h4>
        <form onSubmit={handleSubmit}>
          <TileGroup data={mapToCategories()} />
          <Grid container spacing={3}>
            <Grid item sm={12} justify="center">
              <Button
                type="button"
                className="previous"
                onClick={previousPage}
                color="primary"
                variant="text"
                size="large"
              >
                Back
              </Button>
              <Button
                type="submit"
                className="next"
                color="primary"
                variant="contained"
                size="large"
              >
                Continue
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </>
  );
};

const mapStateToProps = (state) => ({
  categoryGroups: state.categoryGroups.data,
  categories: state.categories.data,
});

const mapDispatchToProps = (dispatch) => {
  return {
    getCategoryGroups: () => dispatch(get_category_groups()),
    getCategories: () => dispatch(get_categories()),
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(
  reduxForm({
    form: "registrationForm",
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
  })(ChooseActivities)
);
