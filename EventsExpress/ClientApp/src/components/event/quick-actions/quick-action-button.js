import React from "react";
import { IconButton, Tooltip } from "@material-ui/core";

export const QuickActionButton = ({ title, icon, menuId, onClick, active = false }) => {
    return (
        <Tooltip title={title}>
            <IconButton
                color={active ? 'primary' : 'default'}
                aria-controls={menuId}
                aria-haspopup={Boolean(menuId)}
                onClick={onClick}
            >
                {icon}
            </IconButton>
        </Tooltip>
    );
};
