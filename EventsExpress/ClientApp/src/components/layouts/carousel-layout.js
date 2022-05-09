import React from 'react';
import { Icon } from '@material-ui/core';
import './carousel-layout.css';
import Carousel from 'react-material-ui-carousel';
import IconButton from '@material-ui/core/IconButton';

export const CarouselLayout = ({ children, onNext, onPrevious, hasNext, hasPrevious }) => {
    const CustomNavButton = ({ onClick, prev, next }) => (
        <IconButton
            className="navigation-button"
            onClick={onClick}
            disabled={(prev && !hasPrevious) || (next && !hasNext)}
        >
            {prev && <Icon className="fas fa-arrow-circle-left" />}
            {next && <Icon className="fas fa-arrow-circle-right" />}
        </IconButton>
    );

    return (
        <Carousel
            className="carousel-layout-section"
            fullHeightHover={false}
            autoPlay={false}
            animation="slide"
            interval={1000}
            indicators={false}
            navButtonsAlwaysVisible={true}
            next={onNext}
            prev={onPrevious}
            NavButton={CustomNavButton}
            navButtonsWrapperProps={{
                className: 'navigation-button-wrappers'
            }}
            navButtonsProps={{
                className: 'navigation-button'
            }}
        >
            <div className="carousel-layout-cards">
                {children}
            </div>
        </Carousel>
    );
};
