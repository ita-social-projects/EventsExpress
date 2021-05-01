import React, { Component } from 'react';
import ParticipantGroup from './participant-group';
import OwnersActions from './owners-action';
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
                >
                    {
                        admins.map(user => (
                            < OwnersActions
                                user={user}
                                isMyEvent={isMyEvent}
                            />
                        ))
                    }
                </ParticipantGroup>
                <ParticipantGroup
                    disabled={visitors.approvedUsers.length == 0}
                    users={visitors.approvedUsers}
                    label="Visitors"
                >
                    {
                        visitors.approvedUsers.map(user => (
                            <ApprovedUsersActions
                                user={user}
                                isMyEvent={isMyEvent}
                                isMyPrivateEvent={isMyPrivateEvent}
                            />
                        ))
                    }
                </ParticipantGroup>
                {isMyPrivateEvent &&
                    <ParticipantGroup
                        disabled={visitors.pendingUsers.length == 0}
                        users={visitors.pendingUsers}
                        label="Pending users"
                    >
                        {
                            visitors.pendingUsers.map(user => (
                                <PendingUsersActions
                                    user={user}
                                    isMyEvent={isMyEvent}
                                />
                            ))
                        }
                    </ParticipantGroup>
                }
                {isMyPrivateEvent &&
                    <ParticipantGroup
                        disabled={visitors.deniedUsers.length == 0}
                        users={visitors.deniedUsers}
                        label="Denied users"
                    >
                        {
                            visitors.deniedUsers.map(user => (
                                <DeniedUsersActions
                                    user={user}
                                    isMyEvent={isMyEvent}
                                />
                            ))
                        }
                    </ParticipantGroup>
                }
            </div>
        )
    }
}

export default EventVisitors;