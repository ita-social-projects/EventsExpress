import React from "react";
import { IconButton, Tooltip } from "@material-ui/core";

export const QuickActionButton = ({ title, icon, menuId, onClick }) => {
    return (
        <Tooltip title={title}>
            <IconButton
                aria-controls={menuId}
                aria-haspopup={Boolean(menuId)}
                onClick={onClick}
            >
                {icon}
            </IconButton>
        </Tooltip>
    );
};
