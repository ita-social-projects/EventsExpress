import ViewCarouselIcon from '@material-ui/icons/ViewCarousel';
import { QuickActionButton } from '../quick-action-button';
import React from 'react';

export const CarouselLayout = () => {
    return (
        <QuickActionButton
            title="Carousel layout"
            icon={<ViewCarouselIcon />}
        />
    );
}