const parseDate = (value) => {
  if (!value) {
    return value;
  }

  //if date selected with date pick
  if (value.toString().length > 12) {
    return value;
  }
  // keyboard input
  const onlyNums = value.toString().replace(/[^0-9]+/g, "");

  const day = onlyNums.slice(0, 2);
  const month = onlyNums.slice(2, 4);
  const year = onlyNums.slice(4, 8);
  return `${year}-${month}-${day}T00:00:00.000Z`;
};

export default parseDate;
