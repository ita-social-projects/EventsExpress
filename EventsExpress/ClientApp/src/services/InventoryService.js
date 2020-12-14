import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class InventoryService {

    getInventoriesByEventId = async (eventId) => {
        const res = await baseService.getResource(`inventory/${eventId}/GetInventar`);
        return res;
    }

    setItem = async (item, eventId) => {
        const data = {
            id: item.id,
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: item.unitOfMeasuring
        }
        const res = await baseService.setResource(`inventory/${eventId}/EditInventar`, data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setItemToInventory = async (item, eventId) => {
        const data = {
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: {id: item.unitOfMeasuring}
        }
        const res = await baseService.setResource(`inventory/${eventId}/AddInventar`, data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setItemDelete = async (itemId, eventId) => {
        const res = await baseService.setResource(`inventory/${eventId}/DeleteInventar/?itemId=${itemId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getUnitsOfMeasuring = async () => {
        return await baseService.getResource('unitofmeasuring/getall');
    }

    setWantToTake = async (data) => {
    const res = await baseService.setResource(`UserEventInventory/MarkItemAsTakenByUser`, data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getUsersInventories = async (eventId) => {
        const res = await baseService.getResource(`UserEventInventory/GetAllMarkItemsByEventId/?eventId=${eventId}`);
        return res;
    }

    setUsersInventoryDelete = async (data) => {
        const res = await baseService.setResource(`UserEventInventory/Delete`, data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUsersInventory = async (data) => {
        const res = await baseService.setResource(`UserEventInventory/Edit`, data);
        return !res.ok
            ? { error: await res.text() }
            : res; 
    }
}