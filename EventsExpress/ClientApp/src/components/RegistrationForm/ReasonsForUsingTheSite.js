import React from "react";
import reasonsForUsingTheSiteEnum from "../../constants/reasonsForUsingTheSiteEnum";

const ReasonsForUsingTheSite = (props) => {
    const project = () => {
        return props.data
            .map((i) => reasonsForUsingTheSiteEnum.find((el) => el.value === i))
            .join(", ");
    };

    return <div>{project()}</div>;
};

export default ReasonsForUsingTheSite;
