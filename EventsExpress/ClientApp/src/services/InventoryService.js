import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class InventoryService {

    getInventoriesByEventId = eventId => baseService.getResourceNew(`inventory/${eventId}/GetInventar`);

    setItem = (item, eventId) => {
        const data = {
            id: item.id,
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: item.unitOfMeasuring
        }
        baseService.setResource(`inventory/${eventId}/EditInventar`, data);
    }

    setItemToInventory = (item, eventId) => {
        const data = {
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: item.unitOfMeasuring
        }
        baseService.setResource(`inventory/${eventId}/AddInventar`, data);
    }

    setItemDelete = (itemId, eventId) => {
        baseService.setResource(`inventory/${eventId}/DeleteInventar/?itemId=${itemId}`);
    }

    getUnitsOfMeasuring = () => baseService.getResource('unitofmeasuring/all');

    setWantToTake = data => baseService.setResource(`UserEventInventory/MarkItemAsTakenByUser`, data);

    getUsersInventories = eventId => baseService.getResource(`UserEventInventory/GetAllMarkItemsByEventId/?eventId=${eventId}`);


    setUsersInventoryDelete = data => {
        data.quantity = 1;
        baseService.setResource(`UserEventInventory/Delete`, data);
    }

    setUsersInventory = data => baseService.setResource(`UserEventInventory/Edit`, data);
}