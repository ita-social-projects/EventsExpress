export const usersHaveAnyOfRoles = (userRoles, rolesList) =>
    userRoles.some((role) => rolesList.includes(role));
