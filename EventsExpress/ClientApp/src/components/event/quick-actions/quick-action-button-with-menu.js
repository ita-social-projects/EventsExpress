import React, { useState } from 'react';
import { Menu } from '@material-ui/core';
import { QuickActionButton } from "./quick-action-button";

export const QuickActionButtonWithMenu = ({ title, icon, suppressMenu, renderMenuItems }) => {
    const [menuAnchor, setMenuAnchor] = useState(null);

    const handleButtonClick = event => {
        if (typeof(suppressMenu) !== 'function' || !suppressMenu()) {
            setMenuAnchor(event.currentTarget);
        }
    };

    return (
        <>
            <QuickActionButton
                title={title}
                icon={icon}
                menuId={title}
                onClick={handleButtonClick}
            />
            <Menu
                id={title}
                anchorEl={menuAnchor}
                getContentAnchorEl={null}
                open={Boolean(menuAnchor)}
                onClick={() => setMenuAnchor(null)}
                anchorOrigin={{ horizontal: 'left', vertical: 'bottom' }}
            >
                {renderMenuItems()}
            </Menu>
        </>
    );
};
