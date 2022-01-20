import React, { Component } from 'react';
import ParticipantGroup from './participant-group';
import OrganizersActions from './organizers-actions';
import ApprovedUsersActions from './approved-users-action';
import PendingUsersActions from './pending-users-action';
import DeniedUsersActions from './denied-users-action';

class EventVisitors extends Component {

    render() {
        const { isMyPrivateEvent, visitors, admins, isMyEvent } = this.props;

        return (
            <div>
                <ParticipantGroup
                    disabled={false}
                    users={admins}
                    label="Admin"
                    renderUserActions={(user) => (
                        <OrganizersActions
                            user={user}
                            isMyEvent={isMyEvent}
                        />
                    )}
                />
                <ParticipantGroup
                    disabled={visitors.approvedUsers.length == 0}
                    users={visitors.approvedUsers}
                    label="Visitors"
                    renderUserActions={(user) => (
                        <ApprovedUsersActions
                            user={user}
                            isMyEvent={isMyEvent}
                            isMyPrivateEvent={isMyPrivateEvent}
                        />
                    )}
                />
                {isMyPrivateEvent &&
                    <ParticipantGroup
                        disabled={visitors.pendingUsers.length == 0}
                        users={visitors.pendingUsers}
                        label="Pending users"
                        renderUserActions={(user) => (
                            <PendingUsersActions
                                user={user}
                                isMyEvent={isMyEvent}
                            />
                        )}
                    />
                }
                {isMyPrivateEvent &&
                    <ParticipantGroup
                        disabled={visitors.deniedUsers.length == 0}
                        users={visitors.deniedUsers}
                        label="Denied users"
                        renderUserActions={(user) => (
                            <DeniedUsersActions
                                user={user}
                                isMyEvent={isMyEvent}
                            />
                        )}
                    />
                }
            </div>
        );
    }
}

export default EventVisitors;
