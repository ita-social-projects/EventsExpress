import React from 'react';
import { IconButton } from '@material-ui/core';
import ArrowUpwardIcon from '@material-ui/icons/ArrowUpward';
import ArrowDownwardIcon from '@material-ui/icons/ArrowDownward';
import './list-layout.css';

export const ListLayout = ({ children, onNext, onPrevious, hasPrevious = false, hasNext = false }) => {
    return (
        <section className='list-layout-section'>
            <div className="list-layout-card">
                {children}
            </div>
            <div className='nav-buttons'>
                <div className='nav-button'>
                    <IconButton className='arrow-icon-up' onClick={onPrevious} disabled={!hasPrevious}>
                        <ArrowUpwardIcon fontSize='large' />
                    </IconButton>
                </div>
                <div className='nav-button'>
                    <IconButton className='arrow-icon-down' onClick={onNext} disabled={!hasNext}>
                        <ArrowDownwardIcon fontSize='large' color='info' />
                    </IconButton>
                </div>
            </div>            
        </section>
    )
}