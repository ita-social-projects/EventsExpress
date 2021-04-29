import React, { Component } from 'react';
import ParticipantGroup from './participant-group';
import UserView from './users-view';
import OwnersActions from './owners-action';
import ApprovedUsersActions from './approved-users-action';
import PendingUsersActions from './pending-users-action';
import DeniedUsersActions from './denied-users-action';

class EventVisitors extends Component {

    renderOwners = (arr, isMyEvent) => {
        return arr.map((user, key) => (
            <div key={key}>
                <UserView user={user}>
                    <OwnersActions
                        user={user}
                        isMyEvent={isMyEvent}
                    />
                </UserView>
            </div>
        ));
    }

    renderApprovedUsers = (arr, isMyEvent, isMyPrivateEvent) => {
        return arr.map(user => (
            <UserView user={user}>
                <ApprovedUsersActions
                    user={user}
                    isMyEvent={isMyEvent}
                    isMyPrivateEvent={isMyPrivateEvent}
                />
            </UserView>
        ));
    }

    renderPendingUsers = (arr, isMyEvent) => {
        return arr.map(user => (
            <UserView user={user}>
                <PendingUsersActions
                    user={user}
                    isMyEvent={isMyEvent}
                />
            </UserView>
        ));
    }

    renderDeniedUsers = (arr, isMyEvent) => {
        return arr.map(user => (
            <UserView user={user}>
                <DeniedUsersActions
                    user={user}
                    isMyEvent={isMyEvent}
                />
            </UserView>
        ));
    }


    render() {
        const { isMyPrivateEvent, visitors, admins, isMyEvent } = this.props;

        return (
            <div>
                <ParticipantGroup
                    disabled={false}
                    renderGroup={() => this.renderOwners(admins, isMyEvent)}
                    label="Admin"
                />
                <ParticipantGroup
                    disabled={visitors.approvedUsers.length == 0}
                    renderGroup={() => this.renderApprovedUsers(visitors.approvedUsers, isMyEvent, isMyPrivateEvent)}
                    label="Visitors"
                />
                {isMyPrivateEvent &&
                    <ParticipantGroup
                        disabled={visitors.pendingUsers.length == 0}
                        renderGroup={() => this.renderPendingUsers(visitors.pendingUsers)}
                        label="Pending users"
                    />
                }
                {isMyPrivateEvent &&
                    <ParticipantGroup
                        disabled={visitors.deniedUsers.length == 0}
                        renderGroup={() => this.renderDeniedUsers(visitors.deniedUsers)}
                        label="Denied users"
                    />
                }
            </div>
        )
    }
}

export default EventVisitors;