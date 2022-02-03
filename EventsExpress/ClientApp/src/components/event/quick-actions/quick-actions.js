import React from 'react';
import { connect } from 'react-redux';
import OrderEvents from './order/order-events';
import JoinedEventsFilter from './joined-events/joined-events-filter';

const QuickActions = ({ userId }) => {
    return (
        <div className="d-flex justify-content-end">
            <OrderEvents />
            {userId &&
                <JoinedEventsFilter />
            }
        </div>
    );
};

const mapStateToProps = state => ({
    userId: state.user.id,
});

export default connect(mapStateToProps)(QuickActions);
