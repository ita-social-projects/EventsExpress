const validate = (values) => {
    let errors = {};
    const requiredFields = [
        "firstName",
        "lastName",
        "birthDate",
        "country",
        "city",
        "gender",
    ];

    if (values.firstName && values.firstName.length < 3)
        errors.firstName = "User name too short";
    else if (values.firstName && values.firstName.length > 50)
        errors.firstName = "User name too long";

    if (values.lastName && values.lastName.length < 3)
        errors.lastName = "User name too short";
    else if (values.lastName && values.lastName.length > 50)
        errors.lastName = "User name too long";

    if (values.gender && values.gender > 3) {
        errors.gender = "Invalid gender";
    }
};
