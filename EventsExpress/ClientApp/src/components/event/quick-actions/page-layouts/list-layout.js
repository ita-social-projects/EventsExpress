import ViewListIcon from '@material-ui/icons/ViewList';
import { QuickActionButton } from '../quick-action-button';
import React from 'react';

export const ListLayout = () => {
    return (
        <QuickActionButton
            title="List layout"
            icon={<ViewListIcon />}
        />
    );
}