import eventTypeEnum from '../../constants/eventTypeEnum';
import parentStatusEnum from '../../constants/parentStatusEnum';
import relationShipStatusEnum from '../../constants/relationShipStatusEnum';
import reasonsForUsingTheSiteEnum from '../../constants/reasonsForUsingTheSiteEnum';
import theTypeOfLeisureEnum from '../../constants/theTypeOfLeisureEnum';

const parenting = new Map([
    [parentStatusEnum.Kids, 'With children'],
    [parentStatusEnum.NoKids, 'No children'],
]);

const reasons = new Map([
    [reasonsForUsingTheSiteEnum.BeMoreActive, 'Be more active'],
    [reasonsForUsingTheSiteEnum.DevelopASkill, 'Develop a skill'],
    [reasonsForUsingTheSiteEnum.MeetPeopleLikeMe, 'Meet people like me'],
]);

const eventPreferences = new Map([
    [eventTypeEnum.AnyDistance, 'Any distance'],
    [eventTypeEnum.NearMe, 'Near me'],
    [eventTypeEnum.Offline, 'Offline'],
    [eventTypeEnum.Online, 'Online'],
    [eventTypeEnum.Free, 'Free'],
    [eventTypeEnum.Paid, 'Paid'],
]);

const relationshipStatuses = new Map([
    [relationShipStatusEnum.InARelationship, 'In a relationship'],
    [relationShipStatusEnum.Single, 'Single'],
]);

const leisureTypes = new Map([
    [theTypeOfLeisureEnum.Active, 'Active'],
    [theTypeOfLeisureEnum.Passive, 'Passive'],
]);

export default {
    parenting,
    reasons,
    eventPreferences,
    relationshipStatuses,
    leisureTypes,
};
