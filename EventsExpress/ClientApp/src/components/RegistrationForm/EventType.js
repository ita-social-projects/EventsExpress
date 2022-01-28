import React from "react";
import eventTypeEnum from "../../constants/eventTypeEnum";

const EventType = (props) => {
    const project = () => {
        return props.data
            .map((i) => eventTypeEnum.find((el) => el.value === i))
            .join(", ");
    };

    return <div>{project()}</div>;
};

export default EventType;
