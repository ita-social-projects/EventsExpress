import ViewModuleIcon from '@material-ui/icons/ViewModule';
import { QuickActionButton } from '../quick-action-button';
import React from 'react';

export const MatrixLayout = () => {
    return (
        <QuickActionButton
            title="Matrix layout"
            icon={<ViewModuleIcon />}
        />
    );
}