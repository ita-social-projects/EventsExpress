import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class InventoryService {

    getInventoriesByEventId = eventId => baseService.getResourceNew(`inventory/${eventId}/GetInventar`);

    setItem = (item, eventId) =>
        baseService.setResource(`inventory/${eventId}/EditInventar`, {
            id: item.id,
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: item.unitOfMeasuring
        });

    setItemToInventory = (item, eventId) =>
        baseService.setResource(`inventory/${eventId}/AddInventar`, {
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: item.unitOfMeasuring
        });

    setItemDelete = (itemId, eventId) =>
        baseService.setResource(`inventory/${eventId}/DeleteInventar/?itemId=${itemId}`);

    getUnitsOfMeasuring = () => baseService.getResourceNew('unitofmeasuring/all');

    setWantToTake = data => baseService.setResource(`UserEventInventory/MarkItemAsTakenByUser`, data);

    getUsersInventories = eventId => baseService.getResourceNew(`UserEventInventory/GetAllMarkItemsByEventId/?eventId=${eventId}`);


    setUsersInventoryDelete = data => {
        data.quantity = 1;
        baseService.setResource(`UserEventInventory/Delete`, data);
    }

    setUsersInventory = data => baseService.setResource(`UserEventInventory/Edit`, data);
}