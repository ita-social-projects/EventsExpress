import React from 'react';
import { JoinedEventsFilter } from './joined-events/joined-events-filter';

export const QuickFilters = () => {
    return (
        <div className="d-flex justify-content-end">
            <JoinedEventsFilter />
        </div>
    );
};
