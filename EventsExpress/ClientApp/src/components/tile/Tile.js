import React from "react";
import { getFormValues, change } from "redux-form";
import { connect } from "react-redux";
import ThreeStateCheckbox from "../three-state-checkbox/ThreeStateCheckbox";
import "./Tile.css";
import "./covers/tile-covers.css";

const Tile = (props) => {
  const isFormInitialized = () =>
    props.formValues !== undefined && props.formValues.categories !== undefined;

  const handleTriStateChange = (state) => {
    const ids = props.categories.map((c) => c.id);
    let formValues = isFormInitialized()
      ? [...props.formValues.categories]
      : [];
    if (state) {
      let filteredIds = ids.filter((id) => !formValues.includes(id));
      filteredIds.forEach((el) => formValues.push(el));
    } else {
      formValues = formValues.filter((el) => !ids.includes(el));
    }

    props.handleTileToggleAction();
    props.updateFormValue(formValues);
  };

  const toggleTriStateCheckbox = () => {
    if (isFormInitialized()) {
      const opts = props.categories.map((c) => c.id);
      const values = [...props.formValues.categories].filter((item) =>
        opts.includes(item)
      );

      if (values.length === 0) return false;
      if (values.length < opts.length) return null;
      if (values.length === opts.length) return true;
    } else {
      return false;
    }
  };

  return (
    <div
      className={`tile tile-cover_${props.groupId}`}
      onClick={() => props.handleTileToggleAction()}
    >
      <ThreeStateCheckbox
        checked={toggleTriStateCheckbox()}
        onChange={handleTriStateChange}
      />
    </div>
  );
};

const mapStateToProps = (state) => {
  return {
    formValues: getFormValues("registrationForm")(state),
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    updateFormValue: (value) =>
      dispatch(change("registrationForm", "categories", value)),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Tile);
