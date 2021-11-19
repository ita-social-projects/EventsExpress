import moment from "moment";

const parseEuDate = (value) => {
  if (!value) {
    return value;
  }

  return moment(value, "DD-MM-YYYY").toISOString();
};

export default parseEuDate;
