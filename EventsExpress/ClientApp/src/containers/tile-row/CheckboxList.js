import React from "react";
import Line from "./Line";
import { MultiCheckbox } from "../../components/helpers/form-helpers";
import { Field } from "redux-form";
import "./CheckboxList.css";
import "./CustomCheckbox.css";

function CheckboxList(props) {
  const mapToValues = (arr) => {
    console.log(arr);
    return arr.map((el) => ({ value: el.id, text: el.name }));
  };

  return (
    <div className="checkbox-group">
      <Line index={props.index} />
      <h2>Choose any hobbies from list (optional):</h2>
      <Field
        name="categories"
        component={MultiCheckbox}
        type="select-multiple"
        options={mapToValues(props.data)}
      />
    </div>
  );
}

export default CheckboxList;
