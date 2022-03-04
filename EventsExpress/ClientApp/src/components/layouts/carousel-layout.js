import React from 'react';
import { Icon, IconButton } from '@material-ui/core';
import './carousel-layout.css';
import Carousel from 'react-material-ui-carousel';

export const CarouselLayout = ({ children, onNext, onPrevious }) => {
    return (
        <section className="carousel-layout-section">
            <Carousel
                fullHeightHover={false}
                autoPlay={false}
                animation="slide"
                interval={1000}
                indicators={false}
                navButtonsAlwaysVisible={true}
                next={onNext}
                prev={onPrevious}
                NextIcon={<Icon className="fas fa-arrow-circle-right" />}
                PrevIcon={<Icon className="fas fa-arrow-circle-left" />}
            >
                <div className="carousel-layout-cards">
                    {children}
                </div>
            </Carousel>
        </section>
    );
};
