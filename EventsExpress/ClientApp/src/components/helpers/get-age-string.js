import moment from "moment";

export const getAge = birthday => {
    let today = new Date();
    var date = moment(today);
    var birthDate = moment(birthday);
    let age = date.diff(birthDate, 'years');

    if (age >= 100) {
        age = "---";
    }

    return age;
}