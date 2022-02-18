import React from 'react';
import { Icon, IconButton } from '@material-ui/core';
import './carousel-layout.css';

export const CarouselLayout = ({ children, onNext, onPrevious, hasPrevious = false, hasNext = false }) => {
    return (
        <section className="carousel-layout-section">
            <IconButton className="navigation-button" onClick={onPrevious} disabled={!hasPrevious}>
                <Icon className="fas fa-arrow-circle-left" />
            </IconButton>
            <div className="carousel-layout-cards">
                {children}
            </div>
            <IconButton className="navigation-button" onClick={onNext} disabled={!hasNext}>
                <Icon className="fas fa-arrow-circle-right" />
            </IconButton>
        </section>
    );
};
