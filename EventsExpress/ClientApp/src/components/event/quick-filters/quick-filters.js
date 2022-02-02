import React from 'react';
import { connect } from 'react-redux';
import JoinedEventsFilter from './joined-events/joined-events-filter';

const QuickFilters = ({ userId }) => {
    return (
        <div className="d-flex justify-content-end">
            {userId &&
                <JoinedEventsFilter />
            }
        </div>
    );
};

const mapStateToProps = state => ({
    userId: state.user.id,
});

export default connect(mapStateToProps)(QuickFilters);
