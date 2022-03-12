import moment from 'moment';
import eventStatusEnum from '../../../../constants/eventStatusEnum';

const isAdult = currentUser => {
    const today = moment().startOf('day');
    return moment.duration(today.diff(moment(currentUser.birthday))).asYears() >= 18;
};

const validateConditions = conditions => conditions.every(condition => condition === true);

export const iWillVisitIt = (event, currentUser) => event.members.find(x => x.id === currentUser.id) !== undefined;

export const isFutureEvent = event => new Date(event.dateFrom) >= new Date().setHours(0, 0, 0, 0);

export const isMyEvent = (event, currentUser) => event.organizers.find(x => x.id === currentUser.id) !== undefined;

export const isActive = event => event.eventStatus === eventStatusEnum.Active;

export const isFreePlace = event => {
    const approvedUsers = event.members.filter(x => x.userStatusEvent === 0);
    return approvedUsers.length < event.maxParticipants;
};

export const isDenied = (event, currentUser) => {
    const deniedUsers = event.members.filter(x => x.userStatusEvent === 1);
    return deniedUsers.find(x => x.id === currentUser.id) !== undefined;
};

export const isAppropriateAge = (event, currentUser) => !event.isOnlyForAdults || isAdult(currentUser);

export const isUserLoggedIn = user => validateConditions([
    user.id !== null
]);

export const eventCanBeJoined = event => validateConditions([
    isFutureEvent(event),
    isFreePlace(event),
    isActive(event)
]);

export const userCanAttend = (event, user) => validateConditions([
    !iWillVisitIt(event, user),
    !isMyEvent(event, user),
    isAppropriateAge(event, user)
]);

export const eventCanBeLeaved = event => validateConditions([
    isFutureEvent(event),
    isActive(event)
]);

export const userCanLeave = (event, user) => validateConditions([
    !isMyEvent(event, user),
    iWillVisitIt(event, user),
    !isDenied(event, user)
]);
